using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public float sprintSpeed;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public bool canMove = true;
    public float moveCoolTime = 0f;
    private bool isSprint = false;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    public Action inventory;
    private StatData _statData;
    private Rigidbody _rigidbody;
    private PlayerCondition _condition;

    private IEnumerator curCotoutine;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _condition = GetComponent<PlayerCondition>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _statData = CharacterManager.Instance.Player.statData;
        moveSpeed = _statData.moveSpeed;
        jumpPower = _statData.jumpForce;
    }

    private void FixedUpdate()
    {
        if(canMove)
            Move();
        else
        {
            moveCoolTime += Time.fixedDeltaTime;
            if (IsGrounded() && moveCoolTime > 1f)
            {
                canMove = true;
                moveCoolTime = 0;
            }
        }
    }

    private void LateUpdate()
    {
        if(canLook)
            CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        if (isSprint)
        {
            if (_condition.UseStamina(1f) && curMovementInput != Vector2.zero)
                dir *= sprintSpeed;
            else
            {
                isSprint = false;
            }
        }

        dir.y = _rigidbody.velocity.y;



        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower , ForceMode.Impulse);
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            isSprint = true;
        else if (context.phase == InputActionPhase.Canceled)
            isSprint = false;
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void BoostSpeedForDuration(float speed, float duration)
    {
        if(curCotoutine != null)
            StopCoroutine(curCotoutine);
        curCotoutine = SpeedUp(speed, duration);
        StartCoroutine(curCotoutine);
    }

    public void BoostJumpForceForDuration(float jumpForce, float duration)
    {
        if (curCotoutine != null)
            StopCoroutine(curCotoutine);
        curCotoutine = JumpForceUp(jumpForce, duration);
        StartCoroutine(curCotoutine);
    }

    IEnumerator SpeedUp(float speed, float duration)
    {
        moveSpeed = speed;
        yield return new WaitForSeconds(duration);
        moveSpeed = _statData.moveSpeed;
    }

    IEnumerator JumpForceUp(float jumpForce, float duration)
    {
        jumpPower = jumpForce;
        yield return new WaitForSeconds(duration);
        jumpPower = _statData.jumpForce;
    }
}

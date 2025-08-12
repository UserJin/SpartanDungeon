using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LunchPad : MonoBehaviour
{
    public float power;
    public Transform model;
    public LayerMask layerMask;

    [SerializeField] private float curTime;
    [SerializeField] private float maxTime = 2f;

    private BoxCollider col;

    private Rigidbody playerRigid;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        col.size = model.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(((1 << collision.gameObject.layer) & layerMask) > 1)
        {
            playerRigid = collision.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        curTime += Time.deltaTime;
        if(curTime >= maxTime)
        {
            playerRigid.velocity = Vector3.zero;
            CharacterManager.Instance.Player.controller.canMove = false;
            playerRigid.AddForce((transform.forward + Vector3.up).normalized * power, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) > 1)
        {
            playerRigid = null;
            curTime = 0;
        }
    }
}

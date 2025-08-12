using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LunchPad : MonoBehaviour
{
    public float power;
    public Transform model;
    public LayerMask layerMask;

    private BoxCollider col;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        col.size = model.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(((1 << collision.gameObject.layer) & layerMask) > 1)
        {
            if(collision.gameObject.TryGetComponent(out Rigidbody rigid))
            {
                rigid.velocity = Vector3.zero;
                CharacterManager.Instance.Player.controller.canMove = false;
                rigid.AddForce((transform.forward + Vector3.up).normalized * power, ForceMode.Impulse);
            }
        }
    }
}

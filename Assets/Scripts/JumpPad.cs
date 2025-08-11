using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float power;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Game.Common.Constants.Tags.PLAYER))
        {
            if(collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(Vector3.up * power, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("������ٵ� �������� �ʽ��ϴ�.");
            }
        }
    }
}

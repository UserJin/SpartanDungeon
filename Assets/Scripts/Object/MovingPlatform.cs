using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 destPos;
    public float speed;
    public LayerMask mask;

    private void Start()
    {
        startPos = transform.position;
        if(destPos == null)
            destPos = transform.position;
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPos, destPos, t);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.SetParent(null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public Transform modelTransform;
    public int damage;
    public LayerMask layerMask;

    private BoxCollider col;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        col.size = modelTransform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & layerMask) > 1)
        {
            CharacterManager.Instance.Player.condition.TakePhysicalDamage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "New Stat")]
public class StatData : ScriptableObject
{
    [Header("Condition")]
    public float hp;
    public float stamina;

    [Header("Controller")]
    public float moveSpeed;
    public float jumpForce;
}

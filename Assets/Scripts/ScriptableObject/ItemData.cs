using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
}

public enum ConsumableType
{
    Health,
    Stamina,
    Speed,
    JumpForce
}

public enum EquipType
{
    Tool,
    Armor
}

public enum StatusType
{
    Health,
    Stamina,
    Speed,
    JumpForce
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
    public float time;
}

[Serializable]
public class ItemDataEquip
{
    public StatusType statusType;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "Data/New Item")]
public class ItemData : ScriptableObject
{
    [Header("info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public EquipType equipType;
    public ItemDataEquip[] equips;
    public GameObject equipPrefab;
    
}

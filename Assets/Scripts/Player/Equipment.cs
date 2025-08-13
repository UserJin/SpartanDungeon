using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{
    public EquipTool curEquipTool;
    public EquipArmor curEquipArmor;
    public Transform equipParent;

    private PlayerController controller;
    private PlayerCondition condition;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    public void EquipNewTool(ItemData data)
    {
        //장착해제
        UnequipTool();
        curEquipTool = Instantiate(data.equipPrefab, equipParent).GetComponent<EquipTool>();
    }

    public void UnequipTool()
    {
        if(curEquipTool != null)
        {
            Destroy(curEquipTool.gameObject);
            curEquipTool = null;
        }
    }

    public void EquipNewArmor(ItemData data)
    {
        curEquipArmor = data.equipPrefab.GetComponent<EquipArmor>();
        ItemData equipArmorData = curEquipArmor.data;

        for (int i=0;i<equipArmorData.equips.Length;i++)
        {
            AdjustStatus(equipArmorData.equips[i].statusType, equipArmorData.equips[i].value);
        }
    }

    public void UnEquipArmor()
    {
        ItemData equipArmorData = curEquipArmor.data;

        for (int i = 0; i < equipArmorData.equips.Length; i++)
        {
            AdjustStatus(equipArmorData.equips[i].statusType, -1 * equipArmorData.equips[i].value);
        }

        curEquipArmor = null;
    }

    private void AdjustStatus(StatusType type, float amount)
    {
        switch (type)
        {
            // 체력과 스태미너는 일단 미구현
            case StatusType.Health:
                break;
            case StatusType.Stamina:
                break;
            case StatusType.Speed:
                controller.AddEquipMoveSpeed(amount);
                break;
            case StatusType.JumpForce:
                controller.AddEquipJumpPower(amount);
                break;
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && curEquipTool != null && controller.canLook)
        {
            curEquipTool.OnAttackInput();
        }
    }
}

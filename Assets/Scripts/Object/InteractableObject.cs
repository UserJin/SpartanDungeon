using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    protected string interactTxt;

    public string GetInteractPrompt()
    {
        return interactTxt;
    }
    public abstract void OnInteract();
}

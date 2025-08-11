using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    string interactTxt;

    public string GetInteractPrompt()
    {
        return null;
    }

    public void OnInteract()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
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

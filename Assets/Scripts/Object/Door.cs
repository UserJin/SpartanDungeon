using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    private bool isOpen = false;
    private Coroutine doorCoroutine;

    [SerializeField] private Vector3 openRotation = new Vector3(0f, 120f, 0f);
    [SerializeField] private Vector3 closeRotation = new Vector3(0f, 0f, 0f);
    [SerializeField] private float rotationSpeed = 2f;

    Door()
    {
        interactTxt = "E키를 눌러 열기";
    }

    public override void OnInteract()
    {
        ToggleDoor();
    }

    public void ToggleDoor()
    {
        if (doorCoroutine != null)
            StopCoroutine(doorCoroutine);

        if (isOpen)
            doorCoroutine = StartCoroutine(RotateDoor(closeRotation));
        else
            doorCoroutine = StartCoroutine(RotateDoor(openRotation));

        isOpen = !isOpen;
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        transform.rotation = endRotation;
    }
}

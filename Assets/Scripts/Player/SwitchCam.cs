using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CamMode
{
    first,
    third
}

public class SwitchCam : MonoBehaviour
{
    public Transform firstPersonCamTransform;
    public Transform thirdPersonCamTransform;

    public CamMode curCamMode;

    public LayerMask firstLayerMask;
    public LayerMask thirdLayerMask;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        curCamMode = CamMode.first;
    }

    public void OnSwitchCam(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            ToggleCam();
        }
    }

    private void ToggleCam()
    {
        switch(curCamMode)
        {
            case CamMode.first:
                transform.parent = thirdPersonCamTransform;
                transform.localPosition = Vector3.zero;
                cam.cullingMask = thirdLayerMask;
                curCamMode = CamMode.third;
                break;
            case CamMode.third:
                transform.parent = firstPersonCamTransform;
                transform.localPosition = Vector3.zero;
                cam.cullingMask = firstLayerMask;
                curCamMode = CamMode.first;
                break;
        }
    }
}

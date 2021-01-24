using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Powers_Camera : MonoBehaviour
{
    public float mouseSensitivity = 3f;
    public Transform target;
    public float distance;

    private Camera cam;
    private Vector3 camDirection = Vector3.zero;

    //This is used as a reference to the center point that camera is rotating around.
    private Vector3 focusPoint = Vector3.zero;

    private void Start()
    {
        //Get the camera 
        cam = GetComponent<Camera>();
        //Set the cam direction to the current rotation
        camDirection = transform.localEulerAngles;
        //Set the focus point to target
        focusPoint = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Wait for right click to rotate:
        if (Input.GetMouseButton(1))
        {
            //Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;

            //Get mouse movement
            camDirection.x -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            camDirection.y += Input.GetAxis("Mouse X") * mouseSensitivity;

            //Clamp X-axis to keep camera flipping upside-down.
            if (camDirection.x > 89) camDirection.x = 89;
            if (camDirection.x < -89) camDirection.x = -89;

            //Apply the rotation:
            transform.localEulerAngles = new Vector3(camDirection.x, camDirection.y, 0);
        }
        //Otherwise, ensure cursor is free.
        else Cursor.lockState = CursorLockMode.None;

        transform.position = Powers_AnimMath.Slide(transform.position, target.position - transform.forward * distance, .0000000000000001f);
    }

    // This is used by UI buttons to set the current target using Unity's Event System.
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // This is used by UI buttons to set the current distance from target using Unity's Event System.
    public void ChangeDistance(float newDistance)
    {
        distance = newDistance;
    }

    // This is used by the toggle buttons to set if the layer the axis are on is being culled or not.
    public void ChangeAxisVisible(Toggle toggle)
    {
        if (toggle.isOn) cam.cullingMask |= (1 << 2);
        else cam.cullingMask = ~(1 << 2);
    }
}

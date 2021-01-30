using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powers_Camera : MonoBehaviour
{
    /// <summary>
    /// This is the current target for the camera rig to focus on.
    /// </summary>
    public Transform target;

    /// <summary>
    /// This scales the horizontal mouse input
    /// </summary>
    public float mouseSensitivityX = 1;
    /// <summary>
    /// This scales the vertical mouse input
    /// </summary>
    public float mouseSensitivityY = 1;
    /// <summary>
    /// This scales the mouse scroll input
    /// </summary>
    public float mouseScrollMultiplier = 1;

    /// <summary>
    /// The tilt of the camera in degrees
    /// </summary>
    private float pitch;
    /// <summary>
    /// The tilt of the camera in degrees that we want
    /// </summary>
    private float targetPitch;
    /// <summary>
    /// The yaw angle of the camera rig in degrees
    /// </summary>
    private float yaw;
    /// <summary>
    /// The tilt of the camera in degrees that we want
    /// </summary>
    private float targetYaw;

    /// <summary>
    /// How far away the camera should be from its target, in meters
    /// </summary>
    public float distance = 10;
    private float lastDistance;
    /// <summary>
    /// How close the camera can be to its target, in meters
    /// </summary>
    public float minDistance = 10;
    /// <summary>
    /// How far away the camera can be from its target, in meters
    /// </summary>
    public float maxDistance = 10;
    /// <summary>
    /// How far away the camera actually is from its target, in meters, that we want
    /// </summary>
    private float actualDistance = 10;
    /// <summary>
    /// How far away the camera should be from its target, in meters, that we want
    /// </summary>
    private float targetDistance = 10;

    private Camera cam;
    private Vector3 camDirection = Vector3.zero;

    /// <summary>
    /// This is used as a reference to the center point that camera is rotating around.
    /// </summary>
    private Vector3 focusPoint = Vector3.zero;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();

        //Set the cam direction to the current rotation
        camDirection = transform.localEulerAngles;

        //Set the focus point to target
        focusPoint = target.position;

        lastDistance = distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            //changing the pitch:
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            targetYaw -= mouseX * mouseSensitivityX;
            targetPitch += mouseY * mouseSensitivityY;
        }

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        targetDistance += scroll * mouseScrollMultiplier;
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

        //If the distance changes, set the target distance to the default distance for the object
        if (lastDistance != distance) targetDistance = distance;

        actualDistance = Powers_AnimMath.Slide(actualDistance, targetDistance, 0.01f);

        cam.transform.localPosition = new Vector3(0, 0, -actualDistance); //EASE

        //changing the rotation to match the pitch variable:
        targetPitch = Mathf.Clamp(targetPitch, -89, 89);

        pitch = Powers_AnimMath.Slide(pitch, targetPitch, 0.05f); //EASE
        yaw = Powers_AnimMath.Slide(yaw, targetYaw, 0.05f); //EASE

        //Set rotation
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        //Slide object towards position
        transform.position = Powers_AnimMath.Slide(transform.position, target.position - transform.forward * distance, .0000000000000001f);

        lastDistance = distance;
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

    // This is used by UI buttons to set the current min distance from target using Unity's Event System.
    public void ChangeMinDistance(float newMinDistance)
    {
        minDistance = newMinDistance;
    }

    // This is used by UI buttons to set the current max distance from target using Unity's Event System.
    public void ChangeMaxDistance(float newMaxDistance)
    {
        maxDistance = newMaxDistance;
    }

    // This is used by UI buttons to set the current scrollwheel sensitivity from target using Unity's Event System.
    public void ChangeMouseScrollMultiplier(float newMouseScrollMultiplier)
    {
        mouseScrollMultiplier = newMouseScrollMultiplier;
    }

    // This is used by the toggle buttons to set if the layer the axis are on is being culled or not.
    public void ChangeAxisVisible(Toggle toggle)
    {
        if (toggle.isOn) cam.cullingMask |= (1 << 2);
        else cam.cullingMask = ~(1 << 2);
    }
}

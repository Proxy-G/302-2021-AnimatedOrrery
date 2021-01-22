using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_Camera : MonoBehaviour
{
    public float mouseSensitivity = 3f;
    public Transform target;
    public float distance;

    private Vector3 lookDirection;

    // Update is called once per frame
    void Update()
    {
        //set camera position:
        lookDirection = transform.forward;
        transform.position = Powers_AnimMath.Slide(transform.position, target.position - lookDirection * distance, 0.000001f);

    }
}
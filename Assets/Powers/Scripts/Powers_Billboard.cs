using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_Billboard : MonoBehaviour
{
    //Set this to camera.
    public Transform target;

    void Update()
    {
        //Simple billboard script used to look at the camera. Used for glow around the sun.
        transform.LookAt(target);
    }
}

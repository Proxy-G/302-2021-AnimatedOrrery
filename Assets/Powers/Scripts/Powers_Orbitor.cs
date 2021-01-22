using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_Orbitor : MonoBehaviour
{
    public Transform center;
    public float orbitRadius;
    public float orbitTilt;
    public float orbitTime;
    public float rotationTime;
    public bool debugEnabled = false;

    private float orbitAge;
    private float rotationAge;

    // Update is called once per frame
    void Update()
    {
        //checks if transform has been added. if not, script can act as a simple rotation script
        if(center != null)
        {
            //Getting the orbit position
            orbitAge += (Time.deltaTime / orbitTime);
            Vector3 offset = Powers_AnimMath.SpotOnCircleXYZ(orbitRadius, orbitAge, orbitTilt);
            transform.position = center.position + offset;
        }

        //Getting the orbit rotation
        rotationAge += ((Time.deltaTime / rotationTime) * 60);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationAge, transform.eulerAngles.z);
    }
}

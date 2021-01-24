using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_Orbitor : MonoBehaviour
{
    public Transform center;
    public Powers_TimeManager timeManager;
    public TrailRenderer trailRenderer;

    [Space(10)]

    public float axialTilt;
    public float orbitRadius;
    public float orbitTilt;
    public float orbitTime;
    public float rotationTime;
    public float startingOrbitAge;

    private float orbitAge;
    private float rotationAge;

    private float trailRenderTimer;
    private bool isTrailActivated = false;

    private void Start()
    {
        //Disable the trail renderer for the initial movement, as it will cause streak effects.
        if (trailRenderer != null) trailRenderer.enabled = false;
        isTrailActivated = false;

        //Set the orbit age to the desired starting orbit age.
        orbitAge = startingOrbitAge;

        //This is used to get all the planets into their proper positions and rotations.
        CalculateOrbit();
    }

    // Update is called once per frame
    void Update()
    {
        //Do the regular orbit calculations
        CalculateOrbit();

        //If trails aren't activated, add to the timer
        if (!isTrailActivated) trailRenderTimer += Time.deltaTime;
        //If trails aren't activated and it has been a second, then clear and enable trails and set trails activated to true
        if(!isTrailActivated && trailRenderTimer > 1)
        {
            if (trailRenderer != null) trailRenderer.Clear();
            if (trailRenderer != null) trailRenderer.enabled = true;
            isTrailActivated = true;
        }
    }

    void CalculateOrbit()
    {
        //checks if transform has been added. if not, script can act as a simple rotation script
        if(center != null)
        {
            //Getting the orbit position
            orbitAge += ((Time.deltaTime* timeManager.time) / orbitTime);
            Vector3 offset = Powers_AnimMath.SpotOnCircleXZ(orbitRadius, orbitAge);

            //Getting the orbital tilt, using the center's rotation as a base
            Quaternion orbitTiltFactor = Quaternion.Euler(0, 0, orbitTilt);
            offset = orbitTiltFactor* offset;

            transform.position = center.position + offset;
        }

        //Getting the orbit rotation
        rotationAge += ((Time.deltaTime * timeManager.time) / rotationTime) * 60;
        transform.eulerAngles = new Vector3(0, rotationAge, transform.rotation.z);

        //Apply the axial tilt using world space
        transform.Rotate(axialTilt, 0, 0, Space.World);

    }
}

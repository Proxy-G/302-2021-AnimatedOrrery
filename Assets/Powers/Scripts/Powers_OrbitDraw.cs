using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_OrbitDraw : MonoBehaviour
{
    public float radius = 0;
    public float lineWidth = 0;
    public float orbitTilt = 0;
    public Material debugMaterial;

    public void Start()
    {
        //creates a line renderer and sets material
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.material = debugMaterial;

        //sets use of world space and line's width
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;

        //segments is 360 is each degree of the circle
        int segments = 360;
        //create 361 positions for the line renderer. the final one reconnects to the start
        line.positionCount = segments + 1;
        int pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        Vector3[] points = new Vector3[pointCount];

        //create the  positions for each point in the array
        for (int i = 0; i < pointCount; i++)
        {
            float rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        //set the positions of each point in the line renderer using the array
        line.SetPositions(points);

        //apply the orbital tilt to the line renderer
        gameObject.transform.Rotate(0, 0, orbitTilt);
    }
}

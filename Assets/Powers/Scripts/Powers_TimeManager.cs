using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_TimeManager : MonoBehaviour
{
    [Range(-1,3)] 
    public float desiredTime;

    //This variable is used as a global time manipulator.

    [HideInInspector]
    public float time;

    private void Start()
    {
        //Set starting time to desired time:
        time = desiredTime;
    }

    private void Update()
    {
        //lerp time to make it smooth, but also set a high 't' value to ensure quick change
        time = Mathf.Lerp(time, desiredTime, Time.deltaTime);

        //Due to lerp's nature it will only ever get really close to the desired number. These act as a sort of clamp.
        if (time == desiredTime) return;
        else if (desiredTime == -1 && time > -1.01f && time < -0.99f) time = -1;
        else if (desiredTime == 0 && time > -.01f && time < 0.01f) time = 0;
        else if (desiredTime == 1 && time > 0.99f && time < 1.01f) time = 1;
        else if (desiredTime == 2 && time > 1.99f && time < 2.01f) time = 2;
        else if (desiredTime == 3 && time > 2.99f) time = 3;
    }

    // This is used by UI buttons to set the current desired time using Unity's Event System.
    public void SetTime(float setTime)
    {
        desiredTime = setTime;
    }    
}

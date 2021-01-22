using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_Billboard : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.LookAt(target);
    }
}

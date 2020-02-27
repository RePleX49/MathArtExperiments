using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateExperiment : MonoBehaviour
{
    public GameObject OrbitObject;
    public Transform pointer;
    public float speed;
    public float angle;
    public float radius;

    // Update is called once per frame
    void Update()
    {
        if(OrbitObject)
        {
            pointer.position = OrbitObject.transform.position + PointOnCircle(speed * Time.time, radius);
        }
        else
        {
            pointer.position = PointOnCircle(Time.time, radius);
        }      
    }

    /// <summary>
    /// Pass in an angle (called theta) in radians, get back the position on the circumference on the unity circle
    /// </summary>
    /// <param name="theta"></param>
    /// <returns></returns>
    Vector3 PointOnCircle(float theta, float radius)
    {
        return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * radius;
    }

    float OscillateVal(float InVal)
    {
        return InVal + (3f * Mathf.Sin(Time.time)) + (8f * Mathf.Cos(Time.time));
    }
}

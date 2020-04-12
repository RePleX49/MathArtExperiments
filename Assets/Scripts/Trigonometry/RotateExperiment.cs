using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateExperiment : MonoBehaviour
{
    public GameObject OrbitObject;
    public GameObject pointerPrefab;
    public Color[] TrailColors;

    Transform pointer;
    GameObject[] pointers;
    Transform[] orbits;

    public float speed;
    public float angle;
    public float radius;
    public float outerRadius;
    public int pointerCount;

    float increment;
    int colorIndex = 0;

    void Start()
    {
        GameObject point = Instantiate(pointerPrefab, PointOnCircle(speed * Time.time, radius), Quaternion.identity);
        pointer = point.transform;

        increment = Mathf.PI * 2f / (float)pointerCount;

        pointers = new GameObject[pointerCount];
        orbits = new Transform[pointerCount];

        for (int i = 0; i < pointerCount; i++)
        {
            GameObject pointerTest = Instantiate(pointerPrefab, PointOnCircle(increment * (float)i, radius), Quaternion.identity);
            pointers[i] = pointerTest;

            GameObject OrbitTest = Instantiate(pointerPrefab, PointOnCircle(increment * (float)i, radius), Quaternion.identity);
            orbits[i] = OrbitTest.transform;
        }
    }

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

        for (int i = 0; i < pointerCount; i++)
        {
            pointers[i].transform.position = orbits[i].position + PointOnCircle(speed * Time.time, outerRadius);
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
}

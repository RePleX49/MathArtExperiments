using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotator : MonoBehaviour
{
    public GameObject Point;
    public float circleRadius;

    public float speed = 1.0f;

    public float AmplitudeY = 1.0f;
    public float PeriodY = 1.0f;
    public float TimeMultiplierY = 1.0f;

    public float AmplitudeX = 1.0f;
    public float PeriodX = 1.0f;
    public float TimeMultiplierX = 1.0f;

    public float LineLifeTime = 5.0f;

    // Update is called once per frame
    void Update()
    {
        Point.transform.position = PointOnCircle(speed * Time.time, circleRadius);
        Debug.DrawLine(this.transform.position, Point.transform.position, Color.yellow, LineLifeTime);

        float InitialX = this.transform.position.x;
        float InitialZ = this.transform.position.z;

        Vector3 NewPos = new Vector3(SideFunction(), HeightFunction(), InitialZ);

        this.transform.position = NewPos;
    }

    float HeightFunction()
    {
        return AmplitudeY * Mathf.Sin(PeriodY * (TimeMultiplierY * Time.time));
    }

    float SideFunction()
    {
        return AmplitudeX * Mathf.Cos(PeriodX * (TimeMultiplierX * Time.time));
    }

    Vector3 PointOnCircle(float theta, float radius)
    {
        return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * radius;
    }
}

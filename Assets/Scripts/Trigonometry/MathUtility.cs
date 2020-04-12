using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtility
{
    public static Vector3 PointOnCircle(float theta, float radius)
    {
        return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * radius;
    }

    public static Vector3 PointOnSphere(float radius, float XTheta, float YTheta)
    {
        Vector3 returnPoint = new Vector3(0.0f, 0.0f, 0.0f);

        returnPoint.x = Mathf.Sin(XTheta) * Mathf.Cos(YTheta);
        returnPoint.y = Mathf.Sin(YTheta);
        returnPoint.z = Mathf.Cos(XTheta) * Mathf.Cos(XTheta);

        return returnPoint;
    }
}

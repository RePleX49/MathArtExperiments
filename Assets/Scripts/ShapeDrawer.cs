using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDrawer : MonoBehaviour
{
    LineRenderer lineRend;
    public int numofpoints;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = numofpoints;

        for (int i = 0; i < numofpoints; i++)
        {
            float increment = Mathf.PI * 2f / (float)numofpoints;

            lineRend.SetPosition(i, PointOnCircle(increment * (float)i, radius));
        }
    }

    Vector3 PointOnCircle(float theta, float radius)
    {
        return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * radius;
    }
}

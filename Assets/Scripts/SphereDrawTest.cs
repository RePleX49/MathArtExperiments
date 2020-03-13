using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDrawTest : MonoBehaviour
{
    public float radius, verticalTheta, horizontalTheta;
    public Transform drawingElement;
    LineRenderer linerend;

    // Start is called before the first frame update
    void Start()
    {
        linerend = GetComponent<LineRenderer>();
        linerend.positionCount = 1000;
        for(float h = 0; h < 2f * Mathf.PI; h += (2f * Mathf.PI / 100f))
        {
            for(float v = 0; v < 2f * Mathf.PI; v += (2f * Mathf.PI / 100f))
            {
                linerend.SetPosition((int)(100f * h * v), MathUtility.PointOnSphere(1f, h, v));
            }
        }
    }
}

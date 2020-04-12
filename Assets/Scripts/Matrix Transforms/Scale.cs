using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : TransformationClass
{
    public Vector3 scale = Vector3.one;

    public Vector3 period, amplitude;

    void Update()
    {
        // Oscillate scale over time
        scale.x = amplitude.x * Mathf.Sin(period.x * Time.time);
        scale.y = amplitude.y * Mathf.Sin(period.y * Time.time);
        scale.z = amplitude.z * Mathf.Sin(period.z * Time.time);
    }

    public override Matrix4x4 Matrix
    {
        get
        {
            // create new scale matrix based off formula
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(scale.x, 0f, 0f, 0f));
            matrix.SetRow(1, new Vector4(0f, scale.y, 0f, 0f));
            matrix.SetRow(2, new Vector4(0f, 0f, scale.z, 0f));
            matrix.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
            return matrix;
        }
    }
}

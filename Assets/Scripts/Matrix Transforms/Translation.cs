using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : TransformationClass
{
    public Vector3 position;

    public Vector3 period, amplitude;

    void Update()
    {
        // Oscillate position over time
        position.x = amplitude.x * Mathf.Sin(period.x * Time.time);
        position.y = amplitude.y * Mathf.Cos(period.y * Time.time);
        position.z = amplitude.z * Mathf.Sin(period.z * Time.time);
    }

    public override Matrix4x4 Matrix
    {
        get
        {
            // create new translation matrix based on formula
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(1f, 0f, 0f, position.x));
            matrix.SetRow(1, new Vector4(0f, 1f, 0f, position.y));
            matrix.SetRow(2, new Vector4(0f, 0f, 1f, position.z));
            matrix.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
            return matrix;
        }
    }
}

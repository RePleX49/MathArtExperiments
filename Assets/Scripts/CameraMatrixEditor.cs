using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMatrixEditor : MonoBehaviour
{
    Matrix4x4 MatRotate = Matrix4x4.identity;
    Matrix4x4 MatScale = Matrix4x4.identity;
    Matrix4x4 MatTranslation = Matrix4x4.identity;
    Matrix4x4 MatYShear = Matrix4x4.identity;
    Matrix4x4 MatXShear = Matrix4x4.identity;

    Matrix4x4 OriginalProjection;

    Camera cam;

    public float rotation_angle;
    public Vector2 scale = Vector2.one;
    public Vector2 shearing = Vector2.zero;
    public Vector2 translation = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        OriginalProjection = cam.projectionMatrix;
    }

    // Update is called once per frame
    void Update()
    {
        MatScale[0, 0] = scale.x;
        MatScale[1, 1] = scale.y;

        MatXShear[0, 1] = Mathf.Tan(shearing.x);
        MatYShear[1, 0] = Mathf.Tan(shearing.y);

        MatTranslation[0, 2] = translation.x;
        MatTranslation[1, 2] = translation.y;

        MatRotate[0, 0] = Mathf.Cos(rotation_angle);
        MatRotate[0, 1] = Mathf.Sin(rotation_angle);
        MatRotate[1, 0] = -Mathf.Sin(rotation_angle);
        MatRotate[1, 1] = MatRotate[0, 0];
        

        cam.projectionMatrix = OriginalProjection * MatScale * MatXShear * MatYShear * MatTranslation * MatRotate;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    LineRenderer lineRend;

    public void Init(Vector3 LineStart, Vector3 LineEnd)
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.SetPosition(0, LineStart);
        lineRend.SetPosition(1, LineEnd);
    }
}

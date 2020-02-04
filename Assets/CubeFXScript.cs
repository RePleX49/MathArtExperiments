using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFXScript : MonoBehaviour
{
    Color LineColor;
    GameObject NearestObject;
    Vector3 LineEnd;

    // Start is called before the first frame update
    void Start()
    {
        int RandNum = Random.Range(0, 3);

        switch (RandNum)
        {
            case 0:
                LineColor = Color.yellow;
                break;

            case 1:
                LineColor = Color.red;
                break;

            case 2:
                LineColor = Color.blue;
                break;

            default:
                LineColor = Color.green;
                break;
        };

        //DrawLines();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawLines()
    {
        Vector3 LineStart = this.transform.position;
        float closestDistance = 1000000.0f;

        Collider[] colliders = Physics.OverlapSphere(LineStart, 20.0f);

        foreach (Collider testCollider in colliders)
        {
            GameObject testObject = testCollider.gameObject;

            if (testObject.tag == "DataPoint" && testObject != this)
            {
                float DistanceTo = Vector3.Distance(testObject.transform.position, LineStart);

                if (DistanceTo < closestDistance)
                {
                    closestDistance = DistanceTo;
                    NearestObject = testObject;
                }
            }
        }

        if (NearestObject != null)
        {
            LineEnd = NearestObject.transform.position;
            Debug.DrawLine(LineStart, LineEnd, LineColor, 100f, false);
            Debug.Log("Drew Debug");
        }
    }
}

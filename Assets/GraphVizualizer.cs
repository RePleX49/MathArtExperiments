using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphVizualizer : MonoBehaviour
{
    public GameObject dataPrefab;

    [Range(10, 100)]
    public int dataPointCount;

    public float TimeMultiplierA = 1.0f;
    public float TimeMultiplierB = 1.0f;

    public float AmplitudeA = 1.0f;
    public float AmplitudeB = 1.0f;

    public float PeriodA = 1.0f;
    public float PeriodB = 1.0f;

    GameObject[,] dataPoints;

    // Start is called before the first frame update
    void Start()
    {
        dataPoints = new GameObject[dataPointCount, dataPointCount];

        for (int i = 0; i < dataPointCount; i++)
        {
            for (int j = 0; j < dataPointCount; j++)
            {
                Vector3 pos = new Vector3(i, 0, j);
                GameObject newpoint = Instantiate(dataPrefab, pos, Quaternion.identity);
                //newpoint.transform.parent = this.transform;
                dataPoints[i, j] = newpoint;            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dataPointCount; i++)
        {
            for (int j = 0; j < dataPointCount; j++)
            {
                GameObject testObject = dataPoints[i, j];
                Vector3 newPos = new Vector3(testObject.transform.position.x, HeightFunction(i, j), testObject.transform.position.z);
                dataPoints[i, j].transform.position = newPos;
            }
        }
    }

    float HeightFunction(float x, float y)
    {
        float FunctionA = AmplitudeA * Mathf.Cos(PeriodA * (x + (TimeMultiplierA * Time.time)));
        float FunctionB = AmplitudeB * Mathf.Sin(PeriodB * (y + (TimeMultiplierB * Time.time)));

        return FunctionA + FunctionB;
    }
}

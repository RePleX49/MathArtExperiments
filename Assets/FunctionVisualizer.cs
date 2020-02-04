using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionVisualizer : MonoBehaviour
{
    //List<GameObject[,]> dataPoints = new List<GameObject[,]>();

    GameObject[,] dataPoints;

    public GameObject dataPrefab;
    public float slopeMultiplier;
    public float timeMultiplier;
    public float scaleTimeMultiplier;
    public float period = 5f;

    [Range(10, 250)]
    public int dataPointCount = 100;

    // Start is called before the first frame update
    void Start()
    {
        dataPoints = new GameObject[dataPointCount,dataPointCount];
        Vector3 pos = new Vector3(0, 0, 0);

        for (int i = 0; i < dataPointCount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject newpoint = Instantiate(dataPrefab, pos, Quaternion.identity);
                dataPoints[i, j] = newpoint;
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        DrawFunction();
    }

    void DrawFunction()
    {
        for (int i = 0; i < dataPointCount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                /*
                 * single array code
                 * 
                //int Index = i + (-dataPointCount / 2);
                //int IndexJ = j + (-dataPointCount / 2);
                //dataPoints[i, j].transform.position = new Vector3(Index / 10f, MyFunction(Index / 10f, slopeMultiplier), 0);

                */

                dataPoints[i, j].transform.position = new Vector3(i / 10f, MyFunction(i / 10f, slopeMultiplier), j / 10f);

                float YPos = dataPoints[i, j].transform.position.y;
                float Scale = ScaleFunction(YPos, slopeMultiplier);

                dataPoints[i, j].transform.localScale = new Vector3(Scale, Scale, Scale);
            }          
        }
    }

    //TODO
    /*
     * color objects
     * change color according to some other function
     * draw negative values
     * add multiple functions together
     * draw different functions (sin)
     * graph in 3 dimensions
     * 
     */

    void spawnPoint(Vector3 pos)
    {
        GameObject newpoint = Instantiate(dataPrefab, pos, Quaternion.identity);
        newpoint.transform.parent = this.transform;
    }

    // function for drawing, y = 2x
    float MyFunction(float x, float multiplier)
    {
        return multiplier * Mathf.Sin(x * period + (Time.time * timeMultiplier));
    }

    float ScaleFunction(float x, float multiplier)
    {
        return Mathf.Clamp(Mathf.Sin(x * period + (Time.time * scaleTimeMultiplier)), 0.2f, 0.6f);
    }
} 

    
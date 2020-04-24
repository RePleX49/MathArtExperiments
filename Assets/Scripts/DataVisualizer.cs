using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    [Range(5, 100)]
    public int dataPointCount = 50;

    public float maxHeight = 75f;
    public float minHeight = -75f;
    public float maxScale = 350f;
    public float minScale = -350f;
    public float[] dataPoints;
    public GameObject[] visualPoints;
    public GameObject visualPrefab;

    private bool ObjectIsMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        dataPoints = new float[dataPointCount];
        visualPoints = new GameObject[dataPointCount];

        for (int i = 0; i < dataPointCount; i++)
        {
            dataPoints[i] = Random.Range(minHeight, maxHeight);
            visualPoints[i] = Instantiate(visualPrefab, this.transform);

            Vector3 newPos = new Vector3(i * MathUtility.GetStdDev(dataPoints), dataPoints[i]);

            visualPoints[i].transform.localPosition = newPos;
            visualPoints[i].GetComponent<SpriteRenderer>().color = new Color(-newPos.y / 20.0f, 0, newPos.y / 20.0f);          
        }

        DrawDataLines();     
    }

    // Update is called once per frame
    void Update()
    {
        if(!ObjectIsMoving)
        {
            GenerateDataSet();
        }
    }

    void GenerateDataSet()
    {
        for (int i = 0; i < dataPointCount; i++)
        {
            // generate random data and calculate the StdDev for XCoord multiplier
            dataPoints[i] = Random.Range(minHeight, maxHeight);
            float XCoord = i * MathUtility.GetStdDev(dataPoints);

            // clamp XCoord so that the graph doesn't go off frame
            XCoord = Mathf.Clamp(XCoord, 0.0f, 2200.0f);

            Vector3 newPos = new Vector3(XCoord, dataPoints[i]);
            //Debug.Log(XCoord);

            // get scale from datapoint value
            float scale = Mathf.Clamp(dataPoints[i] * 2.85f, minScale, maxScale);
            Vector3 newScale = new Vector3(scale, scale);

            StartCoroutine(SmoothTranslation(visualPoints[i], visualPoints[i].transform.localPosition, newPos, 1f));
            StartCoroutine(SmoothScale(visualPoints[i], visualPoints[i].transform.localScale, newScale, 1f));
        }
    }

    IEnumerator SmoothTranslation(GameObject MoveObject, Vector3 InitialPos, Vector3 TargetPos, float MoveDuration)
    {
        float InitialTime = Time.time;

        ObjectIsMoving = true;

        while (Time.time < InitialTime + MoveDuration && MoveObject != null)
        {
            MoveObject.transform.localPosition = Vector3.Lerp(InitialPos, TargetPos, (Time.time - InitialTime) / MoveDuration);
            MoveObject.GetComponent<SpriteRenderer>().color = 
                new Color(-MoveObject.transform.position.y / 20.0f, 0, MoveObject.transform.position.y / 20.0f);
            DrawDataLines();
            yield return null;
        }

        ObjectIsMoving = false;

        yield return null;
    }

    IEnumerator SmoothScale(GameObject MoveObject, Vector3 InitialScale, Vector3 TargetScale, float MoveDuration)
    {
        float InitialTime = Time.time;

        ObjectIsMoving = true;

        while (Time.time < InitialTime + MoveDuration && MoveObject != null)
        {
            MoveObject.transform.localScale = Vector3.Lerp(InitialScale, TargetScale, (Time.time - InitialTime) / MoveDuration);
            yield return null;
        }

        ObjectIsMoving = false;

        yield return null;
    }

    void DrawDataLines()
    {
        for (int i = 0; i < dataPointCount; i++)
        {
            if (i < dataPointCount - 1)
            {
                Vector3[] LinePositions = { visualPoints[i].transform.position, visualPoints[i + 1].transform.position };
                visualPoints[i].GetComponent<LineRenderer>().SetPositions(LinePositions);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public GameObject linePrefab;
    public int GridSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                DrawLineLeftOrRight(i, j, 1, 1);
            }
        }
    }

    void DrawLineLeftOrRight(int x, int y, int width, int height)
    {
        GameObject newLine = Instantiate(linePrefab, new Vector3(x, y, 0), Quaternion.identity, this.transform);

        Vector3 LineStart, LineEnd;

        if(Random.value < 0.5f)
        {
            LineStart = new Vector3(x, y, 0);
            LineEnd = new Vector3(x + width, y + height, 0);
        }
        else
        {
            LineStart = new Vector3(x + width, y, 0);
            LineEnd = new Vector3(x, y + height, 0);
        }

        newLine.GetComponent<LineDrawer>().Init(LineStart, LineEnd);
    }
}

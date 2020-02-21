using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public Color PrimaryColor;
    public Color SecondaryColor;
    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;
    HexMesh hexMesh;

    HexCell[] cells;

    void Awake()
    {
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void Start()
    {
        hexMesh.Triangulate(cells, PrimaryColor, SecondaryColor);

        //Repeat RenderHex to have colors changing constantly on each cell
        InvokeRepeating("RenderHex", 0.0f, 0.6f);
    }

    void RenderHex()
    {
        hexMesh.Triangulate(cells, PrimaryColor, SecondaryColor);
    }

    void CreateCell(int x, int z, int i)
    {
        //use GridMetrics static class for grid distances, so that we can allow for padding between hexes
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (GridMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (GridMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;     
    }
}

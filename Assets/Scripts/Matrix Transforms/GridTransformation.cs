using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTransformation : MonoBehaviour
{
    public Transform prefab;

    public int gridSize = 10;

    Transform[] grid;

    List<TransformationClass> transformations;

    Matrix4x4 transformation;

    void Awake()
    {
        // make a new grid array in 3D
        grid = new Transform[gridSize * gridSize * gridSize];
        for (int i = 0, z = 0; z < gridSize; z++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++, i++)
                {
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }

        transformations = new List<TransformationClass>();
    }
    void Update()
    {
        UpdateTransformation();

        for (int i = 0, z = 0; z < gridSize; z++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++, i++)
                {
                    // set this grid index new transform
                    grid[i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
    }

    void UpdateTransformation()
    {
        GetComponents<TransformationClass>(transformations);
        if (transformations.Count > 0)
        {
            // set initial transform matrix
            transformation = transformations[0].Matrix;

            for (int i = 1; i < transformations.Count; i++)
            {
                // multiply transforms together to combine
                transformation = transformations[i].Matrix * transformation;
            }
        }
    }

    Transform CreateGridPoint(int x, int y, int z)
    {
        Transform point = Instantiate<Transform>(prefab);

        // set position by grid array Index
        point.localPosition = GetCoordinates(x, y, z);

        // assign color by world space
        point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / gridSize,
            (float)y / gridSize,
            (float)z / gridSize
        );

        return point;
    }

    Vector3 GetCoordinates(int x, int y, int z)
    {
        // adjust location to be centered
        return new Vector3(
            x - (gridSize - 1) * 0.5f,
            y - (gridSize - 1) * 0.5f,
            z - (gridSize - 1) * 0.5f
        );
    }

    Vector3 TransformPoint(int x, int y, int z)
    {
        Vector3 coordinates = GetCoordinates(x, y, z);

        for (int i = 0; i < transformations.Count; i++)
        {
            coordinates = transformations[i].Apply(coordinates);
        }

        return coordinates;
    }


}

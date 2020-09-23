using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoard : MonoBehaviour
{
    public CellNode[][] grid;

    public GameObject cellPrefab;

    public int gridWidth = 16;
    public int gridHeight = 9;
    private Vector3 location; 

    void Start()
    {
        float xInitialOffset = 0.455f;
        float yInitialOffset = -0.307f;

        float xOffset = 0.8895f;
        float yOffset = -0.578f;


        grid = new CellNode[gridHeight][];
        for(int i = 0; i < gridHeight; i++)
        {
            grid[i] = new CellNode[gridWidth];
            for(int j = 0; j < gridWidth; j++)
            {
                grid[i][j] = (Instantiate(cellPrefab, new Vector3(transform.position.x + xInitialOffset + xOffset*j , transform.position.y + yInitialOffset + yOffset* i, transform.position.z-0.00001f), transform.rotation, transform) as GameObject).GetComponent<CellNode>();
            }
        }
    }

    void Update()
    {
        
    }

    public Vector3 GetLocation()
    {
        return location;
    }

    public void SetLocation(Vector3 location)
    {
        this.location = location;
    }

}

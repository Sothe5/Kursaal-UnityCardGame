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
        float xOffset = 0.47f;
        float yOffset = -0.307f;

        grid = new CellNode[gridHeight][];
        for(int i = 0; i < gridHeight; i++)
        {
            grid[i] = new CellNode[gridWidth];
            for(int j = 0; j < gridWidth; j++)
            {
                grid[i][j] = (Instantiate(cellPrefab, new Vector3(transform.position.x + xOffset*i+1, transform.position.y + yOffset*j+1, transform.position.z), transform.rotation, transform) as GameObject).GetComponent<CellNode>();
                grid[i][j].transform.position = new Vector3(transform.position.x + xOffset + 0.88f*j , transform.position.y + yOffset , transform.position.z);
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

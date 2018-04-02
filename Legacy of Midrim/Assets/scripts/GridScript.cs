using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

    public Vector2 gridSize;
    public GameObject hexModel;
    public int hexSize;

	void Start () {
        Node[,] grid = new Node[Mathf.RoundToInt(gridSize.x), Mathf.RoundToInt(gridSize.y)];

        GenerateGrid(grid);
        SpawnHexGrid(grid);
	}
	
    void GenerateGrid(Node[,] grid)
    {
        float preCalculatedValueX = Mathf.Sqrt(3) / 2 * hexSize;
        float preCalculatedValueY = hexSize * 0.75f;
        for (int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                grid[x, y] = new Node(new Vector2(x, y),
                    new Vector3(
                        x * preCalculatedValueX + y * preCalculatedValueX/2,
                        0.0f,
                        y * preCalculatedValueY
                        ));
            }
        }
    }

    void SpawnHexGrid(Node[,] grid)
    {
        foreach(Node n in grid)
        {
            //Debug.Log("Drawing plane");
            GameObject hexagon = Instantiate(hexModel, n.position, Quaternion.identity) as GameObject;
            hexagon.transform.localScale = new Vector3(hexagon.transform.localScale.x * hexSize, hexagon.transform.localScale.y * hexSize, hexagon.transform.localScale.z);
        }
    }

}

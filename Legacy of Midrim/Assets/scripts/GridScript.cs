using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

    public Vector2 gridSize;
    public GameObject hexModel;
    public float hexSize;

	void Start () {
        Node[,] grid = new Node[Mathf.RoundToInt(gridSize.x), Mathf.RoundToInt(gridSize.y)];

        GenerateGrid(grid);
        SpawnHexGrid(grid);
	}
	
    void GenerateGrid(Node[,] grid)
    {
        float preCalculatedValueX = Mathf.Sqrt(3) / 2 * hexSize;
        float preCalculatedValueY = hexSize * 0.75f;
        int firstX = -1;
        for (int y = 0; y < gridSize.y; y++)
        {
            firstX = -Mathf.FloorToInt(y/2);
            for(int x = 0; x < gridSize.x; x++)
            {
                //Debug.Log(string.Format("{0}, {1} floor gives: {2}. -floor gives: {3}", x, y, Mathf.FloorToInt(x / 2), xCoordinate));
                //Debug.Log(string.Format("The storageCor's are: {0}, {1}. The real coordinates are: {2}, {3}", x, y, firstX + x, y));
                //Debug.Log(string.Format("x position is: {0} + {1} * {2} + {3}", firstX, x, preCalculatedValueX, y * preCalculatedValueX / 2));
                grid[x, y] = new Node(new Vector2(firstX + x, y),
                    new Vector3(
                        ((firstX + x) * preCalculatedValueX + y * preCalculatedValueX/2),
                        0.0f,
                        (y * preCalculatedValueY)
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
            hexagon.name = string.Format("{0}, {1}", n.coordinate.x, n.coordinate.y);
            hexagon.transform.localScale = new Vector3(hexagon.transform.localScale.x * hexSize, hexagon.transform.localScale.y * hexSize, hexagon.transform.localScale.z * hexSize);
        }
    }

}

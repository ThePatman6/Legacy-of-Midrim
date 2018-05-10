using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class GridScript : MonoBehaviour {

    public Vector2 gridSize;
    public GameObject hexModel;
    public float hexSize;
    Node[,] grid;
    Path_finding pathFinding;

    public int destinationX;
    public int destinationY;

    private Vector2[] neighbourDirections;
    private List<Node> testPath;
    private Stopwatch sw;

    private void Awake()
    {
        neighbourDirections = new Vector2[] { new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, -1),
            new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(0, 1)
        };
        pathFinding = GetComponent<Path_finding>();
    }

    void Start () {
        sw = new Stopwatch();
        grid = new Node[Mathf.RoundToInt(gridSize.x), Mathf.RoundToInt(gridSize.y)];

        GenerateGrid(grid);
        //SpawnHexGrid(grid);
        sw.Start();
        testPath = pathFinding.FindPath(grid[0, 0], grid[destinationX, destinationY]);
        sw.Stop();
        print("Elapsed time: " + sw.ElapsedMilliseconds + " ms");
        SpawnPath(testPath);
	}

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            sw.Reset();
            sw.Start();
            testPath = pathFinding.FindPath(grid[0, 0], grid[destinationX, destinationY]);
            sw.Stop();
            print("Elapsed time: " + sw.ElapsedMilliseconds + " ms");
        }
    }

    public int MaxGridSize
    {
        get
        {
            return Mathf.RoundToInt(gridSize.x * gridSize.y);
        }
    }

    void GenerateGrid(Node[,] grid)
    {
        float preCalculatedValueX = Mathf.Sqrt(3) / 2 * hexSize;
        float preCalculatedValueY = hexSize * 0.75f;
        int firstX = -1;
        Vector3 nodePosition;
        RaycastHit hitInfo;
        int terrainMask = LayerMask.GetMask("terrain");
        terrainMask = ~terrainMask;
        for (int y = 0; y < gridSize.y; y++)
        {
            firstX = -Mathf.FloorToInt(y/2);
            for(int x = 0; x < gridSize.x; x++)
            {
                nodePosition = new Vector3((firstX + x) * preCalculatedValueX + y * preCalculatedValueX / 2, 110, y * preCalculatedValueY);
                Physics.Raycast(transform.TransformPoint(nodePosition), -transform.up, out hitInfo, Mathf.Infinity, terrainMask);
                nodePosition.y = hitInfo.point.y;
                grid[x, y] = new Node(new Vector2(firstX + x, y), nodePosition, true);
            }
        }
    }

    public int GetFirstX(int y)
    {
        return -Mathf.FloorToInt(y / 2);
    }

    public int XCoordinateToStoreIndex(int x, int y)
    {
        return Mathf.FloorToInt(x + y / 2);
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        foreach (Vector2 direction in neighbourDirections){
            int checkY = Mathf.RoundToInt(node.coordinate.y + direction.y);
            int checkX = XCoordinateToStoreIndex(Mathf.RoundToInt(node.coordinate.x + direction.x), checkY);

            if(checkX < 0 || checkX >= gridSize.x || checkY < 0 || checkY >= gridSize.y)
            {
                continue;
            }
            neighbours.Add(grid[checkX, checkY]);
        }

        return neighbours;
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

    void SpawnPath(List<Node> path)
    {
        foreach(Node n in path)
        {
            GameObject hexagon = Instantiate(hexModel, n.position, Quaternion.identity) as GameObject;
            hexagon.name = string.Format("{0}, {1}", n.coordinate.x, n.coordinate.y);
            hexagon.transform.localScale = new Vector3(hexagon.transform.localScale.x * hexSize, hexagon.transform.localScale.y * hexSize, hexagon.transform.localScale.z * hexSize);
        }
    }

}

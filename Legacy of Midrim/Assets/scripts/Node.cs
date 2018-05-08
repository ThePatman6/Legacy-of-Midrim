using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : IHeapItem<Node>{
    public Vector2 coordinate;
    public Vector3 position;

    // variables below are used for pathfinding
    public int gCost; // distance from startingNode
    public int hCost; // distance from destinationNode
    public bool traversable;
    public Node parent;

    private int heapIndex;

    public Node(Vector2 _coordinate, Vector3 _position, bool _traversable)
    {
        coordinate = _coordinate;
        position = _position;
        traversable = _traversable;
    }

    public float z
    {
        get
        {
            return -coordinate.x - coordinate.y;
        }
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_finding : MonoBehaviour {

    GridScript gridScript;
    private void Awake()
    {
        gridScript = GetComponent<GridScript>();
    }

    public List<Node> FindPath(Node startNode, Node destinationNode)
    {
        Heap<Node> openSet = new Heap<Node>(gridScript.MaxGridSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        int defaultMoveCost = Mathf.RoundToInt(gridScript.hexSize);

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();

            if(currentNode == destinationNode)
            {
                return RetracePath(startNode, destinationNode);
            }
            closedSet.Add(currentNode);

            foreach(Node neighbour in gridScript.GetNeighbours(currentNode))
            {
                if(neighbour.traversable == false || closedSet.Contains(neighbour)){
                    continue;
                }

                int newGCost = currentNode.gCost + defaultMoveCost;
                if(newGCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newGCost;
                    //neighbour.hCost = GetHeuristicDistance(neighbour, destinationNode);
                    neighbour.hCost = Mathf.RoundToInt(Vector3.Distance(neighbour.position, destinationNode.position));
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }

        }
        Debug.Log("incompleet path given");
        return new List<Node>();
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        //Debug.Log(string.Format("({0},{1})", endNode.coordinate.x, endNode.coordinate.y));
        while(currentNode != startNode)
        {
            path.Add(currentNode);
            //Debug.Log(string.Format("currentNode: {0}, {1}", currentNode.coordinate.x, currentNode.coordinate.y));
            currentNode = currentNode.parent;
            if(currentNode == null)
            {
                Debug.Log("CurrentNode was NULL");
            }
            //Debug.Log(string.Format("({0},{1})", currentNode.coordinate.x, currentNode.coordinate.y));
        }
        path.Reverse();

        return path;
    }

    int GetHeuristicDistance(Node nodeA, Node nodeB)
    {
        return Mathf.RoundToInt(Mathf.Max(Mathf.Abs(nodeA.coordinate.x - nodeB.coordinate.y), Mathf.Abs(nodeA.coordinate.x - nodeB.coordinate.y), Mathf.Abs(nodeA.z - nodeB.z)));
    }

}

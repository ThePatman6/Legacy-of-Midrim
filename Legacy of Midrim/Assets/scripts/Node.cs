using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node{
    public Vector2 coordinate;
    public Vector3 position;

    public Node(Vector2 _coordinate, Vector3 _position)
    {
        coordinate = _coordinate;
        position = _position;
    }
}

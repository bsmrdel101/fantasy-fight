using UnityEngine;

public enum NodeType
{
    Floor,
    Wall
}

public class Node
{
    public int x;
    public int y;
    public float g = Mathf.Infinity;
    public float h = Mathf.Infinity;
    public float f => g + h;
    public Node parent;
    public NodeType type;
    public GameObject tile;

    public Node(int x, int y, NodeType type, GameObject tile)
    {
        this.x = x;
        this.y = y;
        this.type = type;
        this.tile = tile;
    }
}

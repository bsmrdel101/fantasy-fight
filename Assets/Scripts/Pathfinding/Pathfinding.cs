using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [Header("Pathfinding")]
    private List<List<Node>> nodes;
    private int gridWidth;
    private int gridHeight;


    public void Init(List<List<Node>> nodeList, int width, int height)
    {
        nodes = nodeList;
        gridWidth = width;
        gridHeight = height;
    }
    
    private float Heuristic(Vector2 start, Vector2 end) => Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);

    internal Node[] FindPath(Vector2 startPos, Vector2 endPos)
    {
        Node start = nodes[(int)startPos.y][(int)startPos.x];
        Node end = nodes[(int)endPos.y][(int)endPos.x];
        
        foreach (List<Node> row in nodes)
        {
            foreach (Node node in row)
            {
                node.g = Mathf.Infinity;
                node.h = Mathf.Infinity;
                node.parent = null;
            }
        }
        
        List<Node> openList = new() { start };
        List<Node> closedList = new();
        start.g = 0;
        start.h = Heuristic(startPos, endPos);

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => (int)(a.f - b.f));
            Node current = openList[0];
            openList.RemoveAt(0);
            if (current == end) return ReconstructPath(current);
            
            closedList.Add(current);
            foreach (Node neighbor in GetNeighbors(current))
            {
                if (closedList.Contains(neighbor)) continue;
                float dx = Mathf.Abs(neighbor.x - current.x);
                float dy = Mathf.Abs(neighbor.y - current.y);
                float stepCost = (dx == 1f && dy == 1f) ? 1.41f : 1f;
                float tentativeG = current.g + stepCost;

                if (tentativeG < neighbor.g)
                {
                    neighbor.parent = current;
                    neighbor.g = current.g;
                    neighbor.h = Heuristic(new Vector2(neighbor.x, neighbor.y), endPos);
                    if (!openList.Contains(neighbor)) openList.Add(neighbor);
                }
            }
        }
        return Array.Empty<Node>();
    }

    private Node[] GetNeighbors(Node node)
    {
        Vector2[] dirs = {
            new Vector2(0, -1),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(-1, 0),
            new Vector2(-1, -1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(1, 1)
        };
        List<Node> neighbors = new();

        foreach (Vector2 d in dirs) {
            int nx = (int)(node.x + d.x);
            int ny = (int)(node.y + d.y);
            if (!IsValidNode(nx, ny)) continue;

            if (Math.Abs(d.x) == 1 && Math.Abs(d.y) == 1) {
                Node neighborHorizontal = nodes[node.y][(int)(node.x + d.x)];
                Node neighborVertical = nodes[(int)(node.y + d.y)][node.x];
                if (neighborHorizontal?.type == NodeType.Wall && neighborVertical?.type == NodeType.Wall) continue;
            }
            neighbors.Add(nodes[ny][nx]);
        }
        return neighbors.ToArray();
    }

    private bool IsValidNode(int x, int y)
    {
        return x >= 0 && y >= 0 && x < gridWidth && y < gridHeight && nodes[y][x].type != NodeType.Wall;
    }
    
    private Node[] ReconstructPath(Node endNode)
    {
        List<Node> path = new();
        Node current = endNode;
        while (current != null)
        {
            path.Insert(0, current);
            current = current.parent;
        }
        return path.ToArray();
    }
}

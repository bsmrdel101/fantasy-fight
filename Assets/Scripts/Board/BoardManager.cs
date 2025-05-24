using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinding))]
public class BoardManager : MonoBehaviour
{
    [Header("Board Properties")]
    [SerializeField] private int gridWidth = 24;
    [SerializeField] private int gridHeight = 15;
    [SerializeField] private float gridGap = 1.05f;
    
    [Header("Tiles")]
    [SerializeField] private GameObject floorTileObj;
    [SerializeField] private GameObject wallTileObj;
    private readonly List<List<Node>> nodes = new();

    [Header("References")]
    [SerializeField] private Transform tileContainer;
    [SerializeField] private Pathfinding pathfinding;
    
    
    private void Start()
    {
        GenerateBoard(gridWidth, gridHeight);
    }
    
    internal void GenerateBoard(int width, int height)
    {
        for (int y = 0; y < height; y++)
        {
            List<Node> row = new();
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = new Vector3(x * gridGap, y * gridGap, 0f);
                GameObject tileObj = Instantiate(floorTileObj, pos, Quaternion.identity);
                tileObj.transform.SetParent(tileContainer, false);
                row.Add(new Node(x, y, NodeType.Floor, tileObj));
            }
            nodes.Add(row);
        }
        pathfinding.Init(nodes, width, height);
    }
}

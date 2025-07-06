using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Pathfinding))]
public class BoardManager : MonoBehaviour
{
    [Header("Board Properties")]
    [SerializeField] private int gridWidth = 24;
    [SerializeField] private int gridHeight = 15;
    [SerializeField] private float gridGap = 1.05f;
    
    [Header("Map Generation")]
    [SerializeField] private float seed = 0f;
    [SerializeField] private float noiseIntensity = 6f;
    [SerializeField] private int mapPadding = 2;
    private float noiseOffsetX;
    private float noiseOffsetY;
    
    [Header("Tiles")]
    [SerializeField] private GameObject floorTileObj;
    [SerializeField] private GameObject wallTileObj;
    private readonly List<List<Node>> nodes = new();

    [Header("References")]
    [SerializeField] private Transform tileContainer;
    [SerializeField] private Pathfinding pathfinding;
    
    
    public Vector2 GetSize() => new Vector2(gridWidth, gridHeight);
    public float GetGridGap() => gridGap;
    public float GetSeed() => seed;
    
    private void Awake()
    {
        mapPadding = Math.Max(mapPadding - 1, 0);
        noiseOffsetX = Random.Range(0f, 9999f);
        noiseOffsetY = Random.Range(0f, 9999f);
    }
    
    internal void GenerateBoard(int width, int height, float seed = 0f)
    {
        for (int y = 0; y < height; y++)
        {
            List<Node> row = new();
            for (int x = 0; x < width; x++)
            {
                NodeType type = GetNodeTypeFromNoiseAndPos(GenerateNoise(x, y, seed), x, y);
                Vector3 pos = new Vector3(x * gridGap, y * gridGap, 0f);
                GameObject tileObj = Instantiate(GetTileFromType(type), pos, Quaternion.identity);
                tileObj.transform.SetParent(tileContainer, false);
                row.Add(new Node(x, y, type, tileObj));
            }
            nodes.Add(row);
        }
        pathfinding.Init(nodes, width, height);
    }
    
    private float GenerateNoise(int x, int y, float seed) 
    {
        float xCoord = (float)x / gridWidth * noiseIntensity + (seed > 0f ? seed : noiseOffsetX);
        float yCoord = (float)y / gridHeight * noiseIntensity + (seed > 0f ? seed : noiseOffsetY);
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    private NodeType GetNodeTypeFromNoiseAndPos(float noise, int x, int y)
    {
        if (noise > 0.6f && x > mapPadding && x < gridWidth - mapPadding - 1 && y > mapPadding && y < gridHeight - mapPadding - 1)
            return NodeType.Wall;
        else
            return NodeType.Floor;
    }

    private GameObject GetTileFromType(NodeType type)
    {
        return type switch
        {
            NodeType.Floor => floorTileObj,
            NodeType.Wall => wallTileObj,
            _ => null
        };
    }
}

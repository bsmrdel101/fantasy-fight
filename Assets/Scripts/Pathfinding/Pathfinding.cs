using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [Header("Pathfinding")]
    private List<List<Node>> nodes;


    public void Init(List<List<Node>> nodeList)
    {
        nodes = nodeList;
    }
}

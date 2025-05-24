using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PathfindingTests
{
    private Pathfinding pathfinding;
    private BoardManager boardManager;

    [UnityOneTimeSetUp]
    public IEnumerator OneTimeSetup()
    {
        Debug.unityLogger.logEnabled = false;
        yield return SceneManager.LoadSceneAsync("Testing");
    }

    [UnitySetUp]
    public IEnumerator SetupEachTest()
    {
        yield return new WaitUntil(() => GameObject.Find("BoardManager") != null);
        boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        boardManager.GenerateBoard(24, 15);
        pathfinding = boardManager.GetComponent<Pathfinding>();
        Assert.IsNotNull(pathfinding, "Pathfinding script not found in scene.");
    }

    private Vector2[] ConvertNodesToVectors(Node[] nodePath)
    {
        Vector2[] path = new Vector2[nodePath.Length];
        for (int i = 0; i < nodePath.Length; i++)
        {
            path[i] = new Vector2(nodePath[i].x, nodePath[i].y);
        }
        return path;
    }
    
    [UnityTest]
    public IEnumerator FindPath_ReturnExpectedPath()
    {
        yield return null;
        Vector2[] expectedPath = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 1),
            new Vector2(2, 2),
            new Vector2(3, 3)
        };
        Node[] nodePath = pathfinding.FindPath(new Vector2(0, 0), new Vector2(3, 3));
        Vector2[] path = ConvertNodesToVectors(nodePath);
        
        Assert.AreEqual(expectedPath.Length, path.Length, "Path lengths do not match.");
        for (int i = 0; i < expectedPath.Length; i++)
        {
            Assert.AreEqual(expectedPath[i], path[i], $"Mismatch at index {i}");
        }
    }
}

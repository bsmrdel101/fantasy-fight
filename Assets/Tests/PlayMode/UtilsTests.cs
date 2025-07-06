using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class UtilsTests
{
    private Utils utils;
    
    [UnityOneTimeSetUp]
    public IEnumerator OneTimeSetup()
    {
        Debug.unityLogger.logEnabled = false;
        yield return SceneManager.LoadSceneAsync("Testing");
    }

    [UnitySetUp]
    public IEnumerator SetupEachTest()
    {
        yield return new WaitUntil(() => GameObject.Find("Utils") != null);
        utils = GameObject.Find("Utils").GetComponent<Utils>();
        Assert.IsNotNull(utils, "Utils script not found in scene.");
    }
    
    [UnityTest]
    public IEnumerator ParseGridCoords_GridToVector()
    {
        yield return null;
        Assert.AreEqual(new Vector2(1.05f, 1.05f), utils.ParseGridCoords(1, 1), "Should convert grid coords into correct vector coords");
        Assert.AreEqual(new Vector2(5.25f, 1.05f), utils.ParseGridCoords(5, 1), "Should convert grid coords into correct vector coords");
        Assert.AreEqual(new Vector2(525f, 1.05f), utils.ParseGridCoords(500, 1), "Should convert grid coords into correct vector coords");
        Assert.AreEqual(new Vector2(-10.50f, 5.25f), utils.ParseGridCoords(-10, 5), "Should convert grid coords into correct vector coords");
    }
}
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BoardTests
{
    [UnityOneTimeSetUp]
    public IEnumerator OneTimeSetup()
    {
        Debug.unityLogger.logEnabled = false;
        yield return SceneManager.LoadSceneAsync("Testing");
    }
}

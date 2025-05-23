using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BoardTests
{
    [OneTimeSetUp]
    public void Setup()
    {
        Debug.unityLogger.logEnabled = false;
        SceneManager.LoadScene("Testing");
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerInteractionTests
{
    GameObject _playerObject;

    [SetUp]
    public void SetUpTest()
    {
        // load the test scene with various scenarios
        SceneManager.LoadScene("Test Scene");

        _playerObject = GameObject.FindGameObjectWithTag("Player");

        if (_playerObject == null)
        {
            throw new System.Exception("No player object found in the scene");
        }
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerInteractionTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}

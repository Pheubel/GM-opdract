using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Testing;
using UnityEditor;
using UnityEngine.InputSystem;

public class PlayerInteractionTests
{
    TestScenario _scenarioPrefab;
    TestScenario _scenario;

    Keyboard _keyboard;

    [OneTimeSetUp]
    public void SetUpOnce()
    {
        _keyboard = InputSystem.AddDevice<Keyboard>();

        //var actions = InputSystem.ListEnabledActions();
        //InputSystem.
        //actions[0]

        // load the test prefab with various scenarios
        _scenarioPrefab = AssetDatabase.LoadAssetAtPath<TestScenario>("Assets/Testing/Prefabs/Movement Test Scenario.prefab");

        if (_scenarioPrefab == null)
            throw new System.Exception("Could not load in test scenario.");
    }

    [SetUp]
    public void SetUpTest()
    {
        _scenario = Object.Instantiate(_scenarioPrefab);
    }

    [TearDown]
    public void TestTeardown()
    {
        Object.Destroy(_scenario);
    }

    private GameObject PreparePlayer(Transform location)
    {
        var playerObject = Object.Instantiate(_scenario.PlayerPrefab, location.position, Quaternion.identity);


        return playerObject;
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerInteractionTestsWithEnumeratorPasses()
    {
        var player = PreparePlayer(_scenario.LeftFromBlock);
        
        yield return null;
    }
}

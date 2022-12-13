using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Testing;
using UnityEditor;

public class PlayerInteractionTests
{
    TestScenario _scenarioPrefab;
    TestScenario _scenario;

    [OneTimeSetUp]
    public void SetUpOnce()
    {
        // load the test scene with various scenarios
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

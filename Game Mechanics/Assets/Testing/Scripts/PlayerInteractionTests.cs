using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Testing;
using UnityEditor;
using UnityEngine.InputSystem;

public class PlayerInteractionTests : InputTestFixture
{
    TestScenario _scenarioPrefab;
    TestScenario _scenario;

    [OneTimeSetUp]
    public void SetUpOnce()
    {
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
        Debug.Log("setup called");

        _scenario = Object.Instantiate(_scenarioPrefab);
    }

    [TearDown]
    public void TestTeardown()
    {
        Debug.Log("teardown called");

        Object.Destroy(_scenario.gameObject);
    }

    private GameObject PreparePlayer(Transform location)
    {
        var playerObject = Object.Instantiate(_scenario.PlayerPrefab, location.position, Quaternion.identity);


        return playerObject;
    }


    [UnityTest]
    public IEnumerator PlayerMovesToLeft()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.EmptyBox);

        yield return new WaitForSeconds(0.1f);

        Press(keyboard.aKey, 1);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerLeftFromEmpty.IsPlayerInTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerMovesToRight()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.EmptyBox);

        yield return new WaitForSeconds(0.1f);

        Press(keyboard.dKey, 1);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerRightFromEmpty.IsPlayerInTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerMovesUpSlope()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.SlopeBottom);

        yield return new WaitForSeconds(0.1f);

        Press(keyboard.dKey, 1);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerRightFromSlope.IsPlayerInTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerMovesDownSlope()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.SlopeTop);

        yield return new WaitForSeconds(0.1f);

        Press(keyboard.aKey, 1);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerLeftFromSlope.IsPlayerInTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerDoesNotSlipDownSlope()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.SlopeDrop);

        var begin = player.transform.position;

        yield return new WaitForSeconds(1f);

        Assert.AreEqual(begin.x, player.transform.position.x, 0.1);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerWithoutDoubleJumpDoesNotBreakBlocks()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.DestructableBlocks);

        yield return new WaitForSeconds(0.5f);

        Press(keyboard.spaceKey, 0.1f);
        yield return new WaitForSeconds(0.2f);
        Release(keyboard.spaceKey, 0.1f);
        yield return new WaitForSeconds(0.2f);
        Press(keyboard.spaceKey, 0.1f);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerOnDestructableBlock.IsPlayerInTrigger);
        Assert.IsTrue(_scenario.TriggerAboveDestructableBlock.HasPlayerEnteredTrigger);
        Assert.IsFalse(_scenario.TriggerBelowDestructableBlock.HasPlayerEnteredTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerWithDoubleJumpBreaksBlocks()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.DestructableBlocks);
        player.GetComponent<PlayerMovement>().SetMaxDoubleJumps(1);

        yield return new WaitForSeconds(0.5f);

        Press(keyboard.spaceKey, 0.1f);
        yield return new WaitForSeconds(0.2f);
        Release(keyboard.spaceKey, 0.1f);
        yield return new WaitForSeconds(0.2f);
        Press(keyboard.spaceKey, 0.1f);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerAboveDestructableBlock.HasPlayerEnteredTrigger);
        Assert.IsTrue(_scenario.TriggerBelowDestructableBlock.HasPlayerEnteredTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerCannotSingleJumpBlockMovingLeft()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.RightFromBlock);

        yield return new WaitForSeconds(0.5f);

        Press(keyboard.aKey);
        yield return new WaitForSeconds(0.2f);
        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(0.2f);
        Release(keyboard.spaceKey);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerRightFromBlock.IsPlayerInTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerCannotSingleJumpBlockMovingRight()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.LeftFromBlock);

        yield return new WaitForSeconds(0.5f);

        Press(keyboard.dKey, 1);
        yield return new WaitForSeconds(0.2f);
        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(0.2f);
        Release(keyboard.spaceKey);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerLeftFromBlock.IsPlayerInTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerCanDoubleJumpBlockMovingLeft()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.RightFromBlock);
        player.GetComponent<PlayerMovement>().SetMaxDoubleJumps(1);

        yield return new WaitForSeconds(0.5f);

        Press(keyboard.aKey);
        yield return new WaitForSeconds(0.2f);
        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(0.2f);
        Release(keyboard.spaceKey);
        yield return new WaitForSeconds(0.2f);
        Press(keyboard.spaceKey);

        yield return new WaitForSeconds(1f);

        Assert.IsFalse(_scenario.TriggerRightFromBlock.IsPlayerInTrigger);
        Assert.IsTrue(_scenario.TriggerOnTopOfBlock.HasPlayerEnteredTrigger);
        Assert.IsTrue(_scenario.TriggerLeftFromBlock.IsPlayerInTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerCanDoubleJumpBlockMovingRight()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.LeftFromBlock);
        player.GetComponent<PlayerMovement>().SetMaxDoubleJumps(1);

        yield return new WaitForSeconds(0.5f);

        Press(keyboard.dKey, 1);
        yield return new WaitForSeconds(0.2f);
        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(0.2f);
        Release(keyboard.spaceKey);
        yield return new WaitForSeconds(0.2f);
        Press(keyboard.spaceKey);

        yield return new WaitForSeconds(1f);

        Assert.IsFalse(_scenario.TriggerLeftFromBlock.IsPlayerInTrigger);
        Assert.IsTrue(_scenario.TriggerOnTopOfBlock.HasPlayerEnteredTrigger);
        Assert.IsTrue(_scenario.TriggerRightFromBlock.IsPlayerInTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerFallsDownWalkingLeft()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.OnBlock);

        yield return new WaitForSeconds(0.5f);

        Press(keyboard.aKey);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerLeftFromBlock.IsPlayerInTrigger);
        Assert.IsFalse(_scenario.TriggerRightFromBlock.IsPlayerInTrigger);

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerFallsDownWalkingRight()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        var player = PreparePlayer(_scenario.OnBlock);

        yield return new WaitForSeconds(0.5f);

        Press(keyboard.dKey);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_scenario.TriggerRightFromBlock.IsPlayerInTrigger);
        Assert.IsFalse(_scenario.TriggerLeftFromBlock.IsPlayerInTrigger);

        Object.Destroy(player);
    }
}

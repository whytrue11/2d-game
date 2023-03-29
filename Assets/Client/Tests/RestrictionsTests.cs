using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class RestrictionsTests
{
    private GameObject utils;

    [SetUp]
    public void Setup()
    {
        utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);  
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(utils);
    }

  
    [UnityTest]
    public IEnumerator PlayerAlmostReachMaxSpeed()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<PlayerMovement>().SetRunSpeed(29.0f);
        SpeedBuff speedBuff = new SpeedBuff();
        Debug.Log(speedBuff.GetDescription());
        speedBuff.Apply(player);
        yield return new WaitForSeconds(0.1f);

        Assert.AreNotEqual(speedBuff.GetMultiplier() * player.GetComponent<PlayerMovement>().GetRunSpeed(), 30.0f);

        Object.Destroy(player);
    }

    //TODOOOOOOOOOOOOO
    [UnityTest]
    public IEnumerator PlayerReachMaxSpeed()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<PlayerMovement>().SetRunSpeed(45.0f);
        SpeedBuff speedBuff = new SpeedBuff();
        Debug.Log(speedBuff.GetMultiplier());
        speedBuff.Apply(player);
        yield return new WaitForSeconds(0.1f);

        Assert.Less(speedBuff.GetMultiplier() * player.GetComponent<PlayerMovement>().GetRunSpeed(), 45.0f);

        Object.Destroy(player);
    }

}

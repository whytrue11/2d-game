using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class CoinPickerTests
{
    private GameObject utils;
    private GameObject coinPicker;

    [SetUp]
    public void Setup()
    {
        utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        coinPicker = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/CoinPicker.prefab");
        coinPicker = GameObject.Instantiate(coinPicker);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(utils);
        Object.Destroy(coinPicker);
    }

    [UnityTest]
    public IEnumerator CoinPickerCollisionWithCoin()
    {
        DataHolder.localCoins = new Coin(0);
        int expectedLocalCoins = 1;
        GameObject coin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Coin.prefab");
        coin = GameObject.Instantiate(coin);

        coinPicker.transform.position = coin.transform.position;
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());
        UnityEngine.Assertions.Assert.IsNull(coin);
        Object.Destroy(coin);
    }
    
    [UnityTest]
    public IEnumerator CoinPickerCollisionWithNoCoin()
    {
        DataHolder.localCoins = new Coin(0);
        int expectedLocalCoins = 0;
        GameObject otherObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        otherObj = GameObject.Instantiate(otherObj);

        coinPicker.transform.position = otherObj.transform.position;
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());
        
        Object.Destroy(otherObj);
    }
}

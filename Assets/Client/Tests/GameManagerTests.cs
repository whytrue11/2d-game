using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class GameManagerTests
{
    private GameObject utils;
    private GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        gameManager = utils.GetComponent<GameManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(utils);
    }

    [UnityTest]
    public IEnumerator GetCoins()
    {
        DataHolder.coins = new Coin(24);
        DataHolder.localCoins = new Coin(120);
        int expectedCoins = DataHolder.coins.GetCoins() + DataHolder.localCoins.GetCoins();
        
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins, gameManager.GetCoins());
    }
    
    [UnityTest]
    public IEnumerator AddCoins()
    {
        DataHolder.coins = new Coin(24);
        DataHolder.localCoins = new Coin(120);
        int addCoins = 31;
        int expectedCoins = DataHolder.coins.GetCoins();
        int expectedLocalCoins = DataHolder.localCoins.GetCoins() + addCoins;

        gameManager.AddCoins(addCoins);
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());
    }
    
    [UnityTest]
    public IEnumerator RemoveCoins()
    {
        int startCoins = 10;
        int startLocalCoins = 3;
        DataHolder.coins = new Coin(startCoins);
        DataHolder.localCoins = new Coin(startLocalCoins);
        int removeCoins = startLocalCoins + 1;
        int expectedCoins = 9;
        int expectedLocalCoins = 0;

        gameManager.RemoveCoins(removeCoins);
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());
    }
    
    [UnityTest]
    public IEnumerator RemoveCoinsMoreThenHave()
    {
        int startCoins = 10;
        int startLocalCoins = 3;
        DataHolder.coins = new Coin(startCoins);
        DataHolder.localCoins = new Coin(startLocalCoins);
        int removeCoins = startCoins + startLocalCoins + 1;
        int expectedCoins = startCoins;
        int expectedLocalCoins = startLocalCoins;

        gameManager.RemoveCoins(removeCoins);
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());
    }
    
    [UnityTest]
    public IEnumerator RemoveOnlyLocalCoins()
    {
        int startCoins = 10;
        int startLocalCoins = 3;
        DataHolder.coins = new Coin(startCoins);
        DataHolder.localCoins = new Coin(startLocalCoins);
        int removeCoins = startLocalCoins;
        int expectedCoins = startCoins;
        int expectedLocalCoins = startLocalCoins - removeCoins;

        gameManager.RemoveCoins(removeCoins);
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());
    }
    
    [UnityTest]
    public IEnumerator EndWithDeathCheckCoins()
    {
        double coinPercent = (double) 2 / 3;
        int startCoins = 10;
        int startLocalCoins = 3;
        DataHolder.coins = new Coin(startCoins);
        DataHolder.localCoins = new Coin(startLocalCoins);
        int expectedCoins = startCoins + (int) Math.Round(startLocalCoins * coinPercent);
        int expectedLocalCoins = 0;

        gameManager.End(true);
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());
    }
    
    [UnityTest]
    public IEnumerator EndWithDeathCheckPlayerParameters()
    {
        DataHolder.SetDefault();
        int expectedPlayerMaxHealth = DataHolder.playerMaxHealth;
        int expectedPlayerCurrentHealth = DataHolder.playerCurrentHealth;
        int expectedPlayerDamage = DataHolder.playerDamage;
        bool expectedPlayerDoubleJumpBuff = DataHolder.playerDoubleJumpBuff;
        float expectedPlayerRunSpeed = DataHolder.playerRunSpeed;
        DataHolder.playerMaxHealth = 192;
        DataHolder.playerCurrentHealth = 9804;
        DataHolder.playerDamage = 343;
        DataHolder.playerDoubleJumpBuff = true;
        DataHolder.playerRunSpeed = 43;

        yield return new WaitForSeconds(0.1f);
        gameManager.End(true);
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedPlayerMaxHealth, DataHolder.playerMaxHealth);
        Assert.AreEqual(expectedPlayerCurrentHealth, DataHolder.playerCurrentHealth);
        Assert.AreEqual(expectedPlayerDamage, DataHolder.playerDamage);
        Assert.AreEqual(expectedPlayerDoubleJumpBuff, DataHolder.playerDoubleJumpBuff);
        Assert.AreEqual(expectedPlayerRunSpeed, DataHolder.playerRunSpeed);
    }
    
    [UnityTest]
    public IEnumerator EndWithNoDeath()
    {
        int expectedScoresCount = 1;
        DataHolder.scores = new List<Timer.Score>();

        gameManager.End(false);
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedScoresCount, DataHolder.scores.Count);
    }
}

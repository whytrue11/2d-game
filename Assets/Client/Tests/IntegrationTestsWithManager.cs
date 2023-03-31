using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class IntegrationTestsWithManager
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

    //Продолжение использования приложения без файла сохранения при входе
    [UnityTest]
    public IEnumerator LoadWithNoDataInFile()
    {
        String path = Application.persistentDataPath + "/save.json";
        Saves saver = utils.GetComponent<Saves>();
        File.Delete(path);
        Coin expectedCoins = new Coin(1);
        List<Timer.Score> expectedScores = new List<Timer.Score>();
        float expectedMusicVolume = 0.3f;

        saver.Load();
        DataHolder.coins.AddCoins(expectedCoins.GetCoins());
        saver.Save();
        DataHolder.coins.SetCoins(0);
        DataHolder.musicVolume = 0;
        DataHolder.scores = null;
        saver.Load();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins.GetCoins(), DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedScores, DataHolder.scores);
        Assert.AreEqual(expectedMusicVolume, DataHolder.musicVolume);
        Assert.IsTrue(File.Exists(path));
        Object.Destroy(saver);
    }
    
    //Изменение громкости музыки и сохранение
    [UnityTest]
    public IEnumerator VolumeOptionSave()
    {
        Saves saver = utils.GetComponent<Saves>();
        saver.Load();
        float beforeMusicVolume = DataHolder.musicVolume;
        float expectedMusicVolume = beforeMusicVolume.Equals(0.3f) ? 0f : 0.3f;

        DataHolder.musicVolume = expectedMusicVolume;
        saver.Save();
        DataHolder.musicVolume = beforeMusicVolume;
        saver.Load();
        yield return new WaitForSeconds(0.1f);
        
        Assert.AreEqual(expectedMusicVolume, DataHolder.musicVolume);
        Object.Destroy(saver);
    }
    
    //Перезагрузка уровня после смерти персонажа
    [UnityTest]
    public IEnumerator ReloadLevelAfterPlayerDeath()
    {
        const double coinPercent = (double) 2 / 3;
        DataHolder.localCoins.SetCoins(0);
        DataHolder.coins.SetCoins(0);
        DataHolder.SetDefault();
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        yield return new WaitForSeconds(0.001f);
        int expectedPlayerMaxHealth = player.GetComponent<Health>().GetMaxHealth();
        int expectedPlayerCurrentHealth = player.GetComponent<Health>().GetHealth();
        int expectedPlayerDamage = player.GetComponent<Attack>().GetDamage();
        int expectedCoins = (int) Math.Round(15 * coinPercent);
        int expectedLocalCoins = 0;

        player.GetComponent<Health>().SetMaxHealth(expectedPlayerMaxHealth - 1);
        player.GetComponent<Health>().SetHealth(expectedPlayerCurrentHealth - 2);
        player.GetComponent<Attack>().SetDamage(expectedPlayerDamage - 3);
        utils.GetComponent<GameManager>().AddCoins(15);
        player.GetComponent<PlayerController>().PlayerDmg(player.GetComponent<Health>().GetHealth());
        player.GetComponent<PlayerController>().Start();
        yield return new WaitForSeconds(0.1f);
        
        Assert.AreEqual(expectedPlayerMaxHealth, DataHolder.playerMaxHealth);
        Assert.AreEqual(expectedPlayerCurrentHealth, DataHolder.playerCurrentHealth);
        Assert.AreEqual(expectedPlayerDamage, DataHolder.playerDamage);
        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());

        Assert.AreEqual(expectedPlayerMaxHealth, player.GetComponent<Health>().GetMaxHealth());
        Assert.AreEqual(expectedPlayerCurrentHealth, player.GetComponent<Health>().GetHealth());
        Assert.AreEqual(expectedPlayerDamage, player.GetComponent<Attack>().GetDamage());
        
        Object.Destroy(player);
    }
    
    //Получение монет за убийство врага
    [UnityTest]
    public IEnumerator GettingCoinsForKillingEnemy()
    {
        DataHolder.coins.SetCoins(0);
        DataHolder.localCoins.SetCoins(0);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        yield return new WaitForSeconds(0.001f);
        int expectedCoins = 0;
        int expectedLocalCoins = 1;

        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position + new Vector3(0.01f, 0.0f, 0.0f), Quaternion.identity);
        platform = GameObject.Instantiate(platform, player.transform.position - new Vector3(0.0f, 1.0f, 0.0f),  Quaternion.identity);
        player.GetComponent<PlayerController>().AttackEnemy();
        yield return new WaitForSeconds(1f);
        
        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());

        Object.Destroy(player);
        Object.Destroy(platform);
        UnityEngine.Assertions.Assert.IsNull(enemyPatrol.GetComponent<EnemyPatrolController>());
        Object.Destroy(enemyPatrol);
    }
    
    //Переход между уровнями (сохранение характеристик персонажа и монет)
    [UnityTest]
    public IEnumerator LevelChangeSavePlayerParameters()
    {
        DataHolder.localCoins.SetCoins(0);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        yield return new WaitForSeconds(0.001f);
        int expectedPlayerMaxHealth = player.GetComponent<Health>().GetMaxHealth() + 12;
        int expectedPlayerCurrentHealth = player.GetComponent<Health>().GetHealth() + 11;
        int expectedPlayerDamage = player.GetComponent<Attack>().GetDamage() + 13;
        bool expectedPlayerDoubleJumpBuff = !player.GetComponent<PlayerController>().GetDoubleJump();
        int expectedCoins = DataHolder.coins.GetCoins();
        int expectedLocalCoins = 15;

        player.GetComponent<Health>().SetMaxHealth(expectedPlayerMaxHealth);
        player.GetComponent<Health>().SetHealth(expectedPlayerCurrentHealth);
        player.GetComponent<Attack>().SetDamage(expectedPlayerDamage);
        player.GetComponent<PlayerController>().SetDoubleJump(expectedPlayerDoubleJumpBuff);
        utils.GetComponent<GameManager>().AddCoins(expectedLocalCoins);
        
        player.GetComponent<PlayerController>().ChangeLevel();
        player.GetComponent<PlayerController>().Start();
        yield return new WaitForSeconds(0.1f);
        
        Assert.AreEqual(expectedPlayerMaxHealth, DataHolder.playerMaxHealth);
        Assert.AreEqual(expectedPlayerCurrentHealth, DataHolder.playerCurrentHealth);
        Assert.AreEqual(expectedPlayerDamage, DataHolder.playerDamage);
        Assert.AreEqual(expectedPlayerDoubleJumpBuff, DataHolder.playerDoubleJumpBuff);
        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());

        Assert.AreEqual(expectedPlayerMaxHealth, player.GetComponent<Health>().GetMaxHealth());
        Assert.AreEqual(expectedPlayerCurrentHealth, player.GetComponent<Health>().GetHealth());
        Assert.AreEqual(expectedPlayerDamage, player.GetComponent<Attack>().GetDamage());
        Assert.AreEqual(expectedPlayerDoubleJumpBuff, player.GetComponent<PlayerController>().GetDoubleJump());
        
        Object.Destroy(player);
    }
    
    //Подбор монет игроком
    [UnityTest]
    public IEnumerator PlayerPickUpCoins()
    {
        DataHolder.localCoins.SetCoins(0);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        GameObject coin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Coin.prefab");
        player = GameObject.Instantiate(player);
        yield return new WaitForSeconds(0.001f);
        int expectedCoins = DataHolder.coins.GetCoins();
        int expectedLocalCoins = 15;

        List<GameObject> coins = new List<GameObject>();
        for (int i = 0; i < expectedLocalCoins; i++)
        {
            coins.Add(GameObject.Instantiate(coin, player.transform.position, Quaternion.identity));
        }
        yield return new WaitForSeconds(0.1f);
        

        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());

        Object.Destroy(player);
        foreach (GameObject coinEx in coins)
        {
            Object.Destroy(coinEx);
        }
    }
    
    //При выходе с уровня сохраняется только определённая часть собранных монет
    [UnityTest]
    public IEnumerator ExitFromLevel()
    {
        const double coinPercent = (double) 2 / 3;
        DataHolder.coins.SetCoins(0);
        DataHolder.localCoins.SetCoins(0);
        int expectedCoins = (int) Math.Round(15 * coinPercent);
        int expectedLocalCoins = 0;

        utils.GetComponent<GameManager>().AddCoins(15);
        utils.GetComponent<GameManager>().End(true);
        yield return new WaitForSeconds(0.1f);
        
        Assert.AreEqual(expectedCoins, DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedLocalCoins, DataHolder.localCoins.GetCoins());
    }
    
    //На паузе игрок не может умереть
    [UnityTest]
    public IEnumerator PlayerCanNotDieOnPause()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        Menu menu = player.GetComponentInChildren<Menu>();
        player = GameObject.Instantiate(player);
        yield return new WaitForSeconds(0.001f);
        int expectedPlayerCurrentHealth = player.GetComponent<Health>().GetHealth();

        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position, Quaternion.identity);
        platform = GameObject.Instantiate(platform, player.transform.position - new Vector3(0.0f, 1.0f, 0.0f),  Quaternion.identity);
        menu.Pause();
        yield return new WaitForSecondsRealtime(1f);
        menu.Unpause();
        
        Assert.AreEqual(expectedPlayerCurrentHealth, player.GetComponent<Health>().GetHealth());
        
        Object.Destroy(player);
        Object.Destroy(platform);
        UnityEngine.Assertions.Assert.IsNull(enemyPatrol.GetComponent<EnemyPatrolController>());
        Object.Destroy(enemyPatrol);
    }
}
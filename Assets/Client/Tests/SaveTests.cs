using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class SaveTests
{
    private static String path = Application.persistentDataPath + "/save.json";
    
    [UnityTest]
    public IEnumerator SaveWithNotExistFile()
    {
        GameObject saver = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Save.prefab");
        saver = GameObject.Instantiate(saver);
        File.Delete(path);

        saver.GetComponent<Saves>().Save();
        yield return new WaitForSeconds(0.1f);

        Assert.True(File.Exists(path));
        Object.Destroy(saver);
    }

    [UnityTest]
    public IEnumerator Load()
    {
        GameObject saver = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Save.prefab");
        saver = GameObject.Instantiate(saver);

        saver.GetComponent<Saves>().Load();
        yield return new WaitForSeconds(0.1f);

        Assert.NotNull(DataHolder.coins.GetCoins());
        Assert.NotNull(DataHolder.localCoins.GetCoins());
        Assert.NotNull(DataHolder.scores);
        Object.Destroy(saver);
    }
    
    [UnityTest]
    public IEnumerator SaveAndLoadData()
    {
        Coin expectedCoins = new Coin(222);
        List<Timer.Score> expectedScores = new List<Timer.Score>();
        float expectedMusicVolume = 0.99f;
        GameObject saver = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Save.prefab");
        saver = GameObject.Instantiate(saver);

        DataHolder.coins = expectedCoins;
        DataHolder.scores = expectedScores;
        DataHolder.musicVolume = expectedMusicVolume;
        yield return new WaitForSeconds(0.1f);
        saver.GetComponent<Saves>().Save();
        DataHolder.coins = null;
        DataHolder.localCoins = null;
        DataHolder.scores = null;
        DataHolder.musicVolume = 0f;
        yield return new WaitForSeconds(0.1f);
        saver.GetComponent<Saves>().Load();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins.GetCoins(), DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedScores, DataHolder.scores);
        Assert.AreEqual(expectedMusicVolume, DataHolder.musicVolume);
        Object.Destroy(saver);
    }

    [UnityTest]
    public IEnumerator LoadWithNoExistFile()
    {
        Coin expectedCoins = new Coin(0);
        List<Timer.Score> expectedScores = new List<Timer.Score>();
        float expectedMusicVolume = 0.3f;
        GameObject saver = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Save.prefab");
        saver = GameObject.Instantiate(saver);
        File.Delete(path);
        
        saver.GetComponent<Saves>().Load();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins.GetCoins(), DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedScores, DataHolder.scores);
        Assert.AreEqual(expectedMusicVolume, DataHolder.musicVolume);
        Object.Destroy(saver);
    }
    
    [UnityTest]
    public IEnumerator LoadWithNoDataInFile()
    {
        Coin expectedCoins = new Coin(0);
        List<Timer.Score> expectedScores = new List<Timer.Score>();
        float expectedMusicVolume = 0.3f;
        GameObject saver = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/Save.prefab");
        saver = GameObject.Instantiate(saver);
        File.Delete(path);
        File.WriteAllText(path, "");
        
        saver.GetComponent<Saves>().Load();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(expectedCoins.GetCoins(), DataHolder.coins.GetCoins());
        Assert.AreEqual(expectedScores, DataHolder.scores);
        Assert.AreEqual(expectedMusicVolume, DataHolder.musicVolume);
        Object.Destroy(saver);
    }
}

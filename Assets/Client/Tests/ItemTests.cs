using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class ItemTests
{
    private GameObject utils;
    private GameObject shop;

    [SetUp]
    public void Setup()
    {
        utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(utils);
        Object.Destroy(shop);
    }


    [UnityTest]
    public IEnumerator ShopCollisionWithEnemyPatrol()
    {
        GameObject enemy =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        enemy = GameObject.Instantiate(enemy);

        enemy.transform.position = shop.transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(shop.GetComponent<Shop>().getEnemiesNearby(), true);

        Object.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator Shop()
    {

        Debug.Log(shop.GetComponent<Shop>().GetCoins());

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(shop.GetComponent<Shop>().getEnemiesNearby(), false);
    
    }

    [UnityTest]
    public IEnumerator ShopCollisionWithEnemyPatrolZone()
    {
        GameObject enemy =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrolZone.prefab");
        enemy = GameObject.Instantiate(enemy);

        enemy.transform.position = shop.transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(shop.GetComponent<Shop>().getEnemiesNearby(), true);

        Object.Destroy(enemy);
    }

}

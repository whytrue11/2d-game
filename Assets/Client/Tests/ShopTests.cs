using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class ShopTests
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

    [Test]
    public void ShopGetBuff()
    {
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);
        Assert.AreNotEqual(string.IsNullOrEmpty(""), string.IsNullOrEmpty(shop.GetComponent<Shop>().GetBuff().GetBuffDescription()));
        Object.Destroy(shop);
    }

    [Test]
    public void ShopGetWeapon()
    {
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);
        Assert.AreNotEqual(string.IsNullOrEmpty(""), string.IsNullOrEmpty(shop.GetComponent<Shop>().GetWeapon().GetWeaponDescription()));
        Object.Destroy(shop);
    }


    [UnityTest]
    public IEnumerator ShopCollisionWithEnemyPatrol()
    {
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);
        GameObject enemy =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        enemy = GameObject.Instantiate(enemy);

        enemy.transform.position = shop.transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(shop.GetComponent<Shop>().GetEnemiesNearby(), true);

        Object.Destroy(enemy);
        Object.Destroy(shop);
    }

    [UnityTest]
    public IEnumerator ShopCollisionWithEnemyPathFinder()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);
        GameObject enemy =
           AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPathFinder.prefab");
        enemy = GameObject.Instantiate(enemy);

        enemy.transform.position = shop.transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(shop.GetComponent<Shop>().GetEnemiesNearby(), true);

        Object.Destroy(enemy);
        Object.Destroy(shop);
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator ShopCollisionWithEnemyPatrolZone()
    {
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);
        GameObject enemy =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrolZone.prefab");
        enemy = GameObject.Instantiate(enemy);

        enemy.transform.position = shop.transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(shop.GetComponent<Shop>().GetEnemiesNearby(), true);

        Object.Destroy(enemy);
        Object.Destroy(shop);
    }

}

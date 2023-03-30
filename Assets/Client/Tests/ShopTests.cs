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

        Assert.AreEqual(shop.GetComponent<Shop>().getEnemiesNearby(), true);

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

        Assert.AreEqual(shop.GetComponent<Shop>().getEnemiesNearby(), true);

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

        Assert.AreEqual(shop.GetComponent<Shop>().getEnemiesNearby(), true);

        Object.Destroy(enemy);
        Object.Destroy(shop);
    }

    [UnityTest]
    public IEnumerator PlayerBuyInShop()
    {
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestShop.prefab");
        shop = GameObject.Instantiate(shop);

        DataHolder.localCoins = new Coin(1000);
        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(1.0f);
        shop.transform.position = player.transform.position;
        shop.transform.position += new Vector3(0.667f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().PickUpItem();
        yield return new WaitForSeconds(0.1f);
        shop.transform.position -= new Vector3(0.667f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().PickUpItem();
        yield return new WaitForSeconds(0.1f);
        shop.transform.position -= new Vector3(1.431f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().PickUpItem();
        yield return new WaitForSeconds(0.1f);
        shop.transform.position -= new Vector3(1.09f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().PickUpItem();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(shop.transform.childCount, 0);
     
        DataHolder.localCoins = new Coin(0);
        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(shop);
    }

    [UnityTest]
    public IEnumerator PlayerCantBuyInShop()
    {
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);

        DataHolder.localCoins = new Coin(1000);
        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(1.0f);
        GameObject enemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestEnemyPatrolZone.prefab");
        enemy = GameObject.Instantiate(enemy);
        enemy.transform.position = player.transform.position;
        shop.transform.position = player.transform.position;
        shop.transform.position += new Vector3(0.667f, 0.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().PickUpItem();
        shop.transform.position -= new Vector3(0.667f, 0.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().PickUpItem();
        shop.transform.position -= new Vector3(1.431f, 0.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().PickUpItem();
        shop.transform.position -= new Vector3(1.09f, 0.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().PickUpItem();

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(shop.transform.childCount, 4);

        DataHolder.localCoins = new Coin(0);
        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(shop);
        Object.Destroy(enemy);
    }
}

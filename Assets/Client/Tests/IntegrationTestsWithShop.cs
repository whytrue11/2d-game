using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class IntegrationTestsWithShop
{

    [UnityTest]
    public IEnumerator PlayerBuyInShop()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestShop.prefab");
        shop = GameObject.Instantiate(shop);

        yield return new WaitForSeconds(0.1f);

        player.GetComponent<PlayerController>().SetDoubleJump(false);
        player.GetComponent<PlayerMovement>().SetRunSpeed(15.0f);
        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(100);
        player.GetComponent<Attack>().SetDamage(0);
        player.GetComponent<Attack>().SetWeaponAnimation(0);
        player.GetComponent<Attack>().SetAttackCooldown(0);

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
        Assert.AreEqual(player.GetComponent<PlayerController>().GetDoubleJump(), true);
        Assert.AreEqual(player.GetComponent<PlayerMovement>().GetRunSpeed(), 15.0f * 1.2f, 0.0001f);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 120);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 120);
        Assert.AreEqual(player.GetComponent<Attack>().GetDamage(), 25);
        Assert.AreEqual(player.GetComponent<Attack>().GetWeaponAnimation(), 1);
        Assert.AreEqual(player.GetComponent<Attack>().GetAttackCooldown(), 0.5f, 0.0001f);
        Assert.AreEqual(DataHolder.localCoins.GetCoins(), 1000 - (1+4+4+10));

        DataHolder.localCoins = new Coin(0);
        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(shop);
        Object.Destroy(utils);
    }

    [UnityTest]
    public IEnumerator PlayerCantBuyInShopWithEnemyNearby()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);

        yield return new WaitForSeconds(0.1f);

        player.GetComponent<PlayerController>().SetDoubleJump(false);
        player.GetComponent<PlayerMovement>().SetRunSpeed(15.0f);
        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(100);
        player.GetComponent<Attack>().SetDamage(0);
        player.GetComponent<Attack>().SetWeaponAnimation(0);
        player.GetComponent<Attack>().SetAttackCooldown(0);

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
        Assert.AreEqual(player.GetComponent<PlayerController>().GetDoubleJump(), false);
        Assert.AreEqual(player.GetComponent<PlayerMovement>().GetRunSpeed(), 15.0f, 0.0001f);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 100);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 100);
        Assert.AreEqual(player.GetComponent<Attack>().GetDamage(), 0);
        Assert.AreEqual(player.GetComponent<Attack>().GetWeaponAnimation(), 0);
        Assert.AreEqual(player.GetComponent<Attack>().GetAttackCooldown(), 0);
        Assert.AreEqual(DataHolder.localCoins.GetCoins(), 1000);

        DataHolder.localCoins = new Coin(0);
        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(shop);
        Object.Destroy(enemy);
        Object.Destroy(utils);
    }

    [UnityTest]
    public IEnumerator PlayerCantBuyInShopWithoutMoney()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);

        yield return new WaitForSeconds(0.1f);

        player.GetComponent<PlayerController>().SetDoubleJump(false);
        player.GetComponent<PlayerMovement>().SetRunSpeed(15.0f);
        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(100);
        player.GetComponent<Attack>().SetDamage(0);
        player.GetComponent<Attack>().SetWeaponAnimation(0);
        player.GetComponent<Attack>().SetAttackCooldown(0);

        int currentCoins = DataHolder.coins.GetCoins();

        DataHolder.localCoins = new Coin(0);
        DataHolder.coins = new Coin(0);

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(1.0f);
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
        Assert.AreEqual(player.GetComponent<PlayerController>().GetDoubleJump(), false);
        Assert.AreEqual(player.GetComponent<PlayerMovement>().GetRunSpeed(), 15.0f, 0.0001f);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 100);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 100);
        Assert.AreEqual(player.GetComponent<Attack>().GetDamage(), 0);
        Assert.AreEqual(player.GetComponent<Attack>().GetWeaponAnimation(), 0);
        Assert.AreEqual(player.GetComponent<Attack>().GetAttackCooldown(), 0);
        Assert.AreEqual(DataHolder.localCoins.GetCoins(), 0);


        DataHolder.coins = new Coin(currentCoins);

        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(shop);
        Object.Destroy(utils);
    }

    [UnityTest]
    public IEnumerator PlayerCantBuyInShopWithMaxStats()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);

        yield return new WaitForSeconds(0.1f);

        player.GetComponent<PlayerController>().SetDoubleJump(true);
        player.GetComponent<PlayerMovement>().SetRunSpeed(30.0f);
        player.GetComponent<Health>().SetHealth(250);
        player.GetComponent<Health>().SetMaxHealth(250);
   
        int currentCoins = DataHolder.coins.GetCoins();

        DataHolder.localCoins = new Coin(1000);

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(1.0f);
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

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(shop.transform.childCount, 4);
        Assert.AreEqual(player.GetComponent<PlayerController>().GetDoubleJump(), true);
        Assert.AreEqual(player.GetComponent<PlayerMovement>().GetRunSpeed(), 30.0f, 0.0001f);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 250);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 250);
        Assert.AreEqual(DataHolder.localCoins.GetCoins(), 1000);

        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(shop);
        Object.Destroy(utils);
    }


}

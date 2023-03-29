using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
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
    public void PlayerCrouchAnimationChange()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<PlayerMovement>().SetRunSpeed(1.0f);
        player.GetComponent<PlayerController>().Move(0.0f, true, false, false);

        Assert.IsTrue(player.GetComponent<Animator>().GetBool("Crouch"));
        Object.Destroy(player);
    }

    [Test]
    public void PlayerDashAnimationChange()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<PlayerMovement>().SetRunSpeed(1.0f);
        player.GetComponent<PlayerController>().Move(1.0f, false, false, true);

        Assert.IsTrue(player.GetComponent<Animator>().GetBool("Dash"));
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerCantJump()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        yield return new WaitForSeconds(0.1f);

        player.GetComponent<PlayerController>().Move(0.0f, false, true, false);

        Assert.IsFalse(player.GetComponent<Animator>().GetBool("Jump"));
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerJumpAnimationChange()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        platform.transform.position = player.transform.position;
        platform.transform.position -= new Vector3(0.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);

        player.GetComponent<PlayerController>().Move(0.0f, false, true, false);

        Assert.IsTrue(player.GetComponent<Animator>().GetBool("Jump"));
        Object.Destroy(platform);
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerHealAnimation()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);

        yield return new WaitForSeconds(0.1f);

        player.GetComponent<PlayerController>().HealAnimation();

        Assert.IsTrue(player.GetComponent<Animator>().GetBool("Heal"));
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerMoveForward()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        float startPosX = player.transform.position.x;
        player.GetComponent<PlayerMovement>().SetRunSpeed(1.0f);
        player.GetComponent<PlayerController>().Move(1.0f, false, false, false);
    
        yield return new WaitForSeconds(0.1f);

        Assert.Greater(player.transform.position.x, startPosX);
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerMoveBack()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        float startPosX = player.transform.position.x;
        player.GetComponent<PlayerMovement>().SetRunSpeed(1.0f);
        player.GetComponent<PlayerController>().Move(-1.0f, false, false, false);

        yield return new WaitForSeconds(1.0f);

        Assert.Less(player.transform.position.x, startPosX);
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerNextToBuff()
    { 
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        platform.transform.position = player.transform.position;
        platform.transform.position -= new Vector3(0.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);

        shop.transform.position = player.transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(player.GetComponent<PlayerController>().GetNextToTheBuff());

        yield return new WaitForSeconds(0.1f);

        Object.Destroy(shop);

        yield return new WaitForSeconds(0.1f);

        Assert.IsFalse(player.GetComponent<PlayerController>().GetNextToTheBuff());
        Object.Destroy(player);
        Object.Destroy(shop);
        Object.Destroy(platform);
    }

    [UnityTest]
    public IEnumerator PlayerNextToWeapon()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        GameObject shop = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Shop.prefab");
        shop = GameObject.Instantiate(shop);
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        platform.transform.position = player.transform.position;
        platform.transform.position -= new Vector3(0.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);

        shop.transform.position = player.transform.position;
        shop.transform.position -= new Vector3(2.6f, 0.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(player.GetComponent<PlayerController>().GetNextToTheWeapon());

        yield return new WaitForSeconds(0.1f);

        Object.Destroy(shop);

        yield return new WaitForSeconds(0.1f);

        Assert.IsFalse(player.GetComponent<PlayerController>().GetNextToTheWeapon());
        Object.Destroy(player);
        Object.Destroy(shop);
        Object.Destroy(platform);
    }

    [UnityTest]
    public IEnumerator PlayerDmg()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        Health health = player.GetComponent<PlayerController>().GetPlayerHealth();
        player.GetComponent<PlayerController>().PlayerDmg(10);

        yield return new WaitForSeconds(0.1f);
        health.DmgUnit(10);

        Assert.AreEqual(health.GetHealth(), player.GetComponent<PlayerController>().GetPlayerHealth().GetHealth());
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerDeath()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        Health health = player.GetComponent<PlayerController>().GetPlayerHealth();
        player.GetComponent<PlayerController>().PlayerDmg(health.GetHealth());

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(player.GetComponent<Animator>().GetBool("Death"));

        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerAttackPatrolZone()
    {
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);
        GameObject enemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestEnemyPatrolZone.prefab");
        enemy = GameObject.Instantiate(enemy);
        enemy.transform.position = player.transform.position;

        yield return new WaitForSeconds(0.1f);
        int dmg = 20;
        int health = enemy.transform.GetChild(0).GetComponent<Health>().GetHealth();
        player.GetComponent<Attack>().SetDamage(dmg);
        player.GetComponent<PlayerController>().AttackEnemy();

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(enemy.transform.GetChild(0).GetComponent<Health>().GetHealth(), health - dmg);

        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator PlayerAttackPatrol()
    {
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);
        GameObject enemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestEnemyPatrol.prefab");
        enemy = GameObject.Instantiate(enemy);
        enemy.transform.position = player.transform.position;

        yield return new WaitForSeconds(0.1f);
        int dmg = 20;
        int health = enemy.transform.GetChild(0).GetComponent<Health>().GetHealth();
        player.GetComponent<Attack>().SetDamage(dmg);
        player.GetComponent<PlayerController>().AttackEnemy();

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(enemy.transform.GetChild(0).GetComponent<Health>().GetHealth(), health - dmg);

        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator PlayerAttackPathFinder()
    {
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);
        GameObject enemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestEnemyPathFinder.prefab");
        enemy = GameObject.Instantiate(enemy);
        enemy.transform.position = player.transform.position;

        yield return new WaitForSeconds(0.1f);
        int dmg = 20;
        int health = enemy.GetComponent<Health>().GetHealth();
        player.GetComponent<Attack>().SetDamage(dmg);
        player.GetComponent<PlayerController>().AttackEnemy();

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(enemy.transform.GetComponent<Health>().GetHealth(), health - dmg);

        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(enemy);
    }

}

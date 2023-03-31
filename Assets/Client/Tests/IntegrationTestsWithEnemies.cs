using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class IntegrationTestsWithEnemies
{
    [UnityTest]
    public IEnumerator BossCloseAttackWithDamageTest()
    {
        GameObject platform =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);

        yield return new WaitForSeconds(0.01f);

        int startHealth = player.GetComponent<Health>().GetHealth();
        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(1.0f);

        GameObject enemyPatrol =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestBoss.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position - new Vector3(1.2f, -0.5f, 0.0f),
            Quaternion.identity);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();

        bossController.isDebugOrTest = true;
        bossController.SetCloseAttack();


        yield return new WaitForSeconds(3.0f);

        Assert.Less(player.GetComponent<Health>().GetHealth(), startHealth);
        // Assert.IsTrue(bossController.GetComponent<Animator>().GetBool("AttackMelee"));
        Object.Destroy(enemyPatrol);
        Object.Destroy(player);
        Object.Destroy(utils);
        Object.Destroy(platform);
    }


    [UnityTest]
    public IEnumerator BossDistanceAttackWithDamageTest()
    {
        GameObject platform =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);

        yield return new WaitForSeconds(0.01f);

        int startHealth = player.GetComponent<Health>().GetHealth();
        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(1.0f);

        GameObject enemyPatrol =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestBossFiring.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position - new Vector3(1.2f, -0.5f, 0.0f),
            Quaternion.identity);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();

        bossController.isDebugOrTest = true;
        bossController.SetDistanceAttack();


        yield return new WaitForSeconds(3.0f);

        Assert.Less(player.GetComponent<Health>().GetHealth(), startHealth);
        // Assert.IsTrue(bossController.GetComponent<Animator>().GetBool("AttackRange"));
        Object.Destroy(enemyPatrol);
        Object.Destroy(player);
        Object.Destroy(utils);
        Object.Destroy(platform);
    }

    [UnityTest]
    public IEnumerator BossDistanceAttackWithDodgePlayerTest()
    {
        GameObject platform =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject platformOverPlayer =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platformOverPlayer = GameObject.Instantiate(platformOverPlayer);
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);

        yield return new WaitForSeconds(0.01f);
        int startHealth = player.GetComponent<Health>().GetHealth();

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(1.0f);
        platformOverPlayer.transform.position = player.transform.position - new Vector3(-2f, -0.60f, 0.0f);
        GameObject enemyPatrol =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestBossFiring.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position - new Vector3(1.2f, -0.5f, 0.0f),
            Quaternion.identity);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();

        bossController.isDebugOrTest = true;
        bossController.SetDistanceAttack();

        yield return new WaitForSeconds(0.1f);

        player.GetComponent<PlayerMovement>().SetRunSpeed(1.0f);
        player.GetComponent<PlayerController>().Move(1.0f, true, false, false);

        yield return new WaitForSeconds(0.01f);

        yield return new WaitForSeconds(5.0f);

        Assert.AreEqual(startHealth, player.GetComponent<Health>().GetHealth());
        // Assert.IsTrue(bossController.GetComponent<Animator>().GetBool("AttackRange"));
        Object.Destroy(enemyPatrol);
        Object.Destroy(player);
        Object.Destroy(utils);
        Object.Destroy(platform);
        Object.Destroy(platformOverPlayer);
    }

    [UnityTest]
    public IEnumerator BossDistanceAttackWithDashPlayerTest()
    {
        GameObject platform =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);

        yield return new WaitForSeconds(0.01f);
        int startHealth = player.GetComponent<Health>().GetHealth();

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(1.0f);
        GameObject enemyPatrol =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestBossFiring.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position - new Vector3(1.2f, -0.5f, 0.0f),
            Quaternion.identity);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();

        bossController.isDebugOrTest = true;
        bossController.SetCloseAttack();

        yield return new WaitForSeconds(0.1f);

        player.GetComponent<PlayerMovement>().SetRunSpeed(1.0f);
        player.GetComponent<PlayerController>().Move(1.0f, false, false, true);

        yield return new WaitForSeconds(0.01f);

        yield return new WaitForSeconds(5.0f);

        Assert.AreEqual(startHealth, player.GetComponent<Health>().GetHealth());
        // Assert.IsTrue(bossController.GetComponent<Animator>().GetBool("AttackRange"));
        Object.Destroy(enemyPatrol);
        Object.Destroy(player);
        Object.Destroy(utils);
        Object.Destroy(platform);
    }
}
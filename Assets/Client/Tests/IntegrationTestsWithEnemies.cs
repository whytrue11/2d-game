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
    [UnityTest]
    public IEnumerator MeetWithPlayerTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        yield return new WaitForSeconds(0.1f);
        GameObject platform =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platform = GameObject.Instantiate(platform);
        player.GetComponent<Health>().SetHealth(250);
        player.GetComponent<Health>().SetMaxHealth(250);
        platform.transform.position = player.transform.position;
        platform.transform.position -= new Vector3(-1.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);

        GameObject enemyPatrol =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrolZone.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position - new Vector3(3.8f, 0.0f, 0.0f),
            Quaternion.identity);
        EnemyPatrolZoneController enemyPatrolZoneController =
            enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolZoneController>();


        yield return new WaitForSeconds(10.0f);

        Assert.IsTrue(enemyPatrolZoneController.atacked);
        Assert.IsTrue(enemyPatrolZoneController.attackInsideLimits);
        Object.Destroy(player);
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
        Object.Destroy(platform);
    }

    [UnityTest]
    public IEnumerator GoodEnding()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestUtils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject platform =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);

        yield return new WaitForSeconds(0.1f);

        player.GetComponent<Health>().SetHealth(250);
        player.GetComponent<Health>().SetMaxHealth(250);
        player.GetComponent<Attack>().SetDamage(100);

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);
        GameObject enemyBoss = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestBoss.prefab");
        enemyBoss = GameObject.Instantiate(enemyBoss, player.transform.position + new Vector3(0.8f, 0.1f, 0.0f),
            Quaternion.identity);
        enemyBoss.GetComponent<Health>().SetHealth(1);

        yield return new WaitForSeconds(2.0f);


        player.GetComponent<PlayerController>().AttackEnemy();

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(enemyBoss);
        Assert.IsTrue(utils.transform.GetChild(0).transform.GetChild(5).gameObject.activeSelf);
        yield return new WaitForSeconds(2.5f);
        Object.Destroy(player);
        Object.Destroy(platform);
        Object.Destroy(utils);
    }

    [UnityTest]
    public IEnumerator PathFinderReachPlayerAndAttack()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject platform =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);

        yield return new WaitForSeconds(0.1f);

        player.GetComponent<Health>().SetHealth(250);
        player.GetComponent<Health>().SetMaxHealth(250);

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(0.1f);
        GameObject enemyPathFinder =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPathFinder.prefab");
        enemyPathFinder = GameObject.Instantiate(enemyPathFinder,
            player.transform.position + new Vector3(1.7f, 0.1f, 0.0f), Quaternion.identity);

        yield return new WaitForSeconds(5.0f);

        Assert.Less(player.GetComponent<Health>().GetHealth(), 250);
        Object.Destroy(player);
        Object.Destroy(enemyPathFinder);
        Object.Destroy(utils);
    }

    [UnityTest]
    public IEnumerator EnemyPatrolMeetWithPlayerAndAttack()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(100);
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);

        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position - new Vector3(0.5f, 0.0f, 0.0f),
            Quaternion.identity);
        EnemyPatrolController enemyPatrolController =
            enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolController>();
        yield return new WaitForSeconds(5.0f);

        Assert.Less(player.GetComponent<Health>().GetHealth(), 100);
        Object.Destroy(player);
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
        Object.Destroy(platform);
    }
}
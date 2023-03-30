using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class EnemyPatrolZoneControllerTest
{
    [UnityTest]
    public IEnumerator TakeDamageTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrolZone.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPatrolZoneController enemyPatrolZoneController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolZoneController>();
        
        int damage = 10;
        enemyPatrolZoneController.GetComponent<Health>().SetHealth(20);
        int currentHealth = enemyPatrolZoneController.GetComponent<Health>().GetHealth();
        
        enemyPatrolZoneController.TakeDamage(damage);
        yield return new WaitForSeconds(0.001f);
        Assert.AreEqual(currentHealth - damage, enemyPatrolZoneController.GetComponent<Health>().GetHealth());
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
    }  
    
    [UnityTest]
    public IEnumerator TakeCriticalDamage()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrolZone.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPatrolZoneController enemyPatrolZoneController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolZoneController>();
        
        int damage = 100;
        enemyPatrolZoneController.GetComponent<Health>().SetHealth(20);

        enemyPatrolZoneController.TakeDamage(damage);
        yield return new WaitForSeconds(0.001f);
        Object.Destroy(utils);
        UnityEngine.Assertions.Assert.IsNull(enemyPatrolZoneController);
        Object.Destroy(enemyPatrol);
        
    }
    
    
    [UnityTest]
    public IEnumerator MeetWithPlayerTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Platform1.prefab");
        platform = GameObject.Instantiate(platform);
        platform.transform.position = player.transform.position;
        platform.transform.position -= new Vector3(0.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);

        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrolZone.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position + new Vector3(1.5f, 0.0f, 0.0f), Quaternion.identity);
        EnemyPatrolZoneController enemyPatrolZoneController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolZoneController>();

        yield return new WaitForSeconds(10.0f);

        Assert.IsTrue(enemyPatrolZoneController.atacked);
        Object.Destroy(player);
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
        Object.Destroy(platform);
    }
    
    [UnityTest]
    public IEnumerator AttackPlayerWithCoolDown()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrolZone.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPatrolZoneController enemyPatrolZoneController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolZoneController>();

        
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        
        Vector3 enemyPosition = enemyPatrolZoneController.transform.position;
        Vector3 nearByPosition = new Vector3(enemyPosition.x - 0.25f, enemyPosition.y, enemyPosition.z);
        player.transform.position = nearByPosition;
        Boolean startCooldownStatus = true;
        yield return new WaitForSeconds(enemyPatrolZoneController.timer);

        Assert.AreNotEqual(startCooldownStatus, enemyPatrolZoneController.cooling);
        Object.Destroy(player);
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
    }
    
    [UnityTest]
    public IEnumerator EscapeFromRangeOfEnemyTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrolZone.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPatrolZoneController enemyPatrolZoneController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolZoneController>();

        
        Vector3 enemyPosition = enemyPatrolZoneController.transform.position;
        Vector3 insideLimitsPosition = new Vector3(enemyPosition.x - 5f, enemyPosition.y, enemyPosition.z);
        enemyPatrolZoneController.transform.position = insideLimitsPosition;

        yield return new WaitForSeconds(0.001f);

        
        Assert.AreEqual(true, !enemyPatrolZoneController.inRange);
        Assert.AreEqual(true, !enemyPatrolZoneController.InsideOfLimits());
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
    }
}
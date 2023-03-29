using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object; 
public class EnemyPathFinderTest
{
    [UnityTest]
    public IEnumerator TakeDamageTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPathFinder.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPathFinderController enemyPathFinder = enemyPatrol.GetComponentInChildren<EnemyPathFinderController>();
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        int damage = 10;
        enemyPathFinder.GetComponent<Health>().SetHealth(20);
        int currentHealth = enemyPathFinder.GetComponent<Health>().GetHealth();
        
        enemyPathFinder.TakeDamage(damage);
        yield return new WaitForSeconds(0.001f);
        Assert.AreEqual(currentHealth - damage, enemyPathFinder.GetComponent<Health>().GetHealth());
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
        Object.Destroy(player);
    }  
    
    [UnityTest]
    public IEnumerator TakeCriticalDamageTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPathFinder.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPathFinderController enemyPathFinder = enemyPatrol.GetComponentInChildren<EnemyPathFinderController>();
        
        int damage = 100;
        enemyPathFinder.GetComponent<Health>().SetHealth(20);

        enemyPathFinder.TakeDamage(damage);
        yield return new WaitForSeconds(0.001f);
        Object.Destroy(utils);
        UnityEngine.Assertions.Assert.IsNull(enemyPathFinder);
        Object.Destroy(enemyPatrol);
        
    }
    
    
    [UnityTest]
    public IEnumerator AttackPlayerTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPathFinder.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPathFinderController enemyPathFinder = enemyPatrol.GetComponentInChildren<EnemyPathFinderController>();
        
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        
        Vector3 enemyPosition = enemyPathFinder.transform.position;
        Vector3 nearByPosition = new Vector3(enemyPosition.x - 0.25f, enemyPosition.y, enemyPosition.z);
        player.transform.position = nearByPosition;
        
        yield return new WaitForSeconds(0.001f);
        

        
        Assert.IsTrue(enemyPathFinder.GetComponent<Animator>().GetBool("Attack"));
        Object.Destroy(player);
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
    }
    
    /*[UnityTest]
    public IEnumerator FollowPlayerTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPathFinder.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPathFinderController enemyPathFinder = enemyPatrol.GetComponentInChildren<EnemyPathFinderController>();
        
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        
        Vector3 enemyPosition = enemyPathFinder.transform.position;
        Vector3 nearByPosition = new Vector3(enemyPosition.x - 1f, enemyPosition.y, enemyPosition.z);
        player.transform.position = nearByPosition;
        
        yield return new WaitForSeconds(0.001f);
        

        
        Assert.AreEqual(enemyPathFinder.pathFound, true);
        Object.Destroy(player);
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
    }*/
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyPatrolControllerTest
{

    [UnityTest]
    public IEnumerator TakeDamageTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPatrolController enemyPatrolController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolController>();
        
        int damage = 10;
        enemyPatrolController.GetComponent<Health>().SetHealth(20);
        int currentHealth = enemyPatrolController.GetComponent<Health>().GetHealth();
        
        enemyPatrolController.TakeDamage(damage);
        yield return new WaitForSeconds(0.001f);
        Assert.AreEqual(currentHealth - damage, enemyPatrolController.GetComponent<Health>().GetHealth());
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
    }
    
    [UnityTest]
    public IEnumerator TakeCriticalDamage()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPatrolController enemyPatrolController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolController>();
        
        int damage = 100;
        enemyPatrolController.GetComponent<Health>().SetHealth(20);

        enemyPatrolController.TakeDamage(damage);
        yield return new WaitForSeconds(0.001f);
        Object.Destroy(utils);
        UnityEngine.Assertions.Assert.IsNull(enemyPatrolController);
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
        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f); 
        yield return new WaitForSeconds(1.0f);
 
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position - new Vector3(0.5f, 0.0f, 0.0f), Quaternion.identity);
        EnemyPatrolController enemyPatrolController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolController>();
        yield return new WaitForSeconds(5.0f);

        Assert.IsTrue(enemyPatrolController.attacked);
        Object.Destroy(player);
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
        Object.Destroy(platform);
    }
    
    [UnityTest]
    public IEnumerator RunBetweenGoalPoints()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPatrolController enemyPatrolController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolController>();
        
        Vector3 enemyPosition = enemyPatrolController.transform.position;
        Vector3 goalPoint1 = new Vector3(enemyPosition.x - 0.05f, enemyPosition.y, enemyPosition.z);
        Vector3 goalPoint2 = new Vector3(enemyPosition.x + 0.05f, enemyPosition.y, enemyPosition.z);

        List<Transform> newPoints = enemyPatrolController.getPoints();
        newPoints[0].position = goalPoint1;
        newPoints[1].position = goalPoint2;
        enemyPatrolController.setPoints(newPoints);
        
        yield return new WaitForSeconds(0.1f);
        
        Assert.IsTrue(enemyPatrolController.walked);
        Object.Destroy(enemyPatrol);
        Object.Destroy(utils);
    }
    
    [UnityTest]
    public IEnumerator HeadDeathHitTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        EnemyPatrolController enemyPatrolController = enemyPatrol.transform.GetChild(0).GetComponent<EnemyPatrolController>();
        
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        
        Vector3 enemyPosition = enemyPatrolController.transform.position;
        Vector3 nearByPosition = new Vector3(enemyPosition.x, enemyPosition.y + 0.25f, enemyPosition.z);
        player.transform.position = nearByPosition;
        
        yield return new WaitForSeconds(0.1f);
        
        Object.Destroy(player);
        Object.Destroy(utils);
        UnityEngine.Assertions.Assert.IsNull(enemyPatrolController);
        Object.Destroy(enemyPatrol);
    }
}
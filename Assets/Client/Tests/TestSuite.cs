using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    [Test]
    public void TestDmgUnit()
    {
        int healthInt = 100;
        int damage = 20;
        int expectedHP = healthInt - damage;
        Health health = new Health();
        health.SetHealth(100);

        health.DmgUnit(damage);

        Assert.AreEqual(expectedHP, health.GetHealth());
    }
    
    [UnityTest]
    public IEnumerator TestEnemyPatrolMove()
    {
        GameObject enemyGameObject = 
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/EnemyPatrol.prefab");
        enemyGameObject = GameObject.Instantiate(enemyGameObject);
        float startPosX = enemyGameObject.transform.GetChild(0).transform.position.x;

        yield return new WaitForSeconds(1.1f);
        
        Assert.AreNotEqual(startPosX, enemyGameObject.transform.GetChild(0).transform.position.x);
    }
}

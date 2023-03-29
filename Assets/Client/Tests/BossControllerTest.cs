using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class BossControllerTest
{
    [UnityTest]
    public IEnumerator TakeDamageTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Boss.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        
        int damage = 10;
        bossController.GetComponent<Health>().SetHealth(20);
        int currentHealth = bossController.GetComponent<Health>().GetHealth();
        
        bossController.TakeDamage(damage);
        yield return new WaitForSeconds(0.001f);
        Assert.AreEqual(currentHealth - damage, bossController.GetComponent<Health>().GetHealth());
        Object.Destroy(enemyPatrol);
        Object.Destroy(player);
        Object.Destroy(utils);
    }
    
    [UnityTest]
    public IEnumerator TakeCriticalDamageTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Boss.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        
        int damage = 100;
        bossController.GetComponent<Health>().SetHealth(20);
        int currentHealth = bossController.GetComponent<Health>().GetHealth();
        
        bossController.TakeDamage(damage);
        yield return new WaitForSeconds(0.001f);
        Object.Destroy(player);
        Object.Destroy(utils);
        UnityEngine.Assertions.Assert.IsNull(enemyPatrol);
        Object.Destroy(enemyPatrol);
        
    }
    
    [UnityTest]
    public IEnumerator AttackTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Boss.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        
        Vector3 enemyPosition = bossController.transform.position;
        Vector3 nearByPosition = new Vector3(enemyPosition.x + 0.5f, enemyPosition.y, enemyPosition.z);
        player.transform.position = nearByPosition;
        int currentHealth = bossController.GetComponent<Health>().GetHealth();
        
        
        yield return new WaitForSeconds(0.01f);
       // string attackAnimationName = bossController.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;
        Assert.IsTrue(bossController.attacked);
        Object.Destroy(enemyPatrol);
        Object.Destroy(player);
        Object.Destroy(utils);
    }
    
    [UnityTest]
    public IEnumerator CloseAttackWithDamageTest()
    {
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Boss.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();
        
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        int startHealth = player.GetComponent<Health>().GetHealth();
        
        Vector3 enemyPosition = bossController.transform.position;
        Vector3 nearByPosition = new Vector3(enemyPosition.x, enemyPosition.y, enemyPosition.z);
        player.transform.position = nearByPosition;
        bossController.isDebugOrTest = true;
        bossController.attacks = Enumerable
            .Repeat(0, 10)
            .Select(i => 0)
            .ToArray();


        yield return new WaitForSeconds(0.001f);
        // string attackAnimationName = bossController.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;
        Assert.Less(player.GetComponent<Health>().GetHealth(), startHealth);
        Object.Destroy(enemyPatrol);
        Object.Destroy(player);
        Object.Destroy(utils);
    }
}
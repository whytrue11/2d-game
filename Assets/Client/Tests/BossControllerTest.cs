using System.Collections;
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
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestBoss.prefab");
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        enemyPatrol = GameObject.Instantiate(enemyPatrol);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();

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
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestBoss.prefab");
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
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
      
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);

        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(1.0f);
        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestBoss.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position - new Vector3(1.25f, -0.5f, 0.0f), Quaternion.identity);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();
        bossController.transform.position = player.transform.position;

        yield return new WaitForSeconds(0.5f);
        Assert.IsTrue(bossController.attacked);
        Object.Destroy(enemyPatrol);
        Object.Destroy(player);
        Object.Destroy(utils);
        Object.Destroy(platform);
    }
    
    [UnityTest]
    public IEnumerator CloseAttackWithDamageTest()
    {
        GameObject platform = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlatform.prefab");
        platform = GameObject.Instantiate(platform);
        GameObject utils = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Utils.prefab");
        utils = GameObject.Instantiate(utils);
        GameObject player =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<Attack>().SetDamage(0);
        
        int startHealth = player.GetComponent<Health>().GetHealth();
        platform.transform.position = player.transform.position - new Vector3(0.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);

        GameObject enemyPatrol = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestBoss.prefab");
        enemyPatrol = GameObject.Instantiate(enemyPatrol, player.transform.position - new Vector3(1.2f, -0.5f, 0.0f), Quaternion.identity);
        BossController bossController = enemyPatrol.GetComponentInChildren<BossController>();

        bossController.isDebugOrTest = true;
        bossController.attacks = Enumerable
            .Repeat(0, 10)
            .Select(i => 0)
            .ToArray();


        yield return new WaitForSeconds(3.0f);
        Assert.Less(player.GetComponent<Health>().GetHealth(), startHealth);
        Object.Destroy(enemyPatrol);
        Object.Destroy(player);
        Object.Destroy(utils);
        Object.Destroy(platform);
    }
}

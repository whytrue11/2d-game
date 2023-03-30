using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class ItemTests
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
    public void PlayerGetSpeedBuff()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        SpeedBuff speedBuff = new SpeedBuff();
        speedBuff.SetMultiplier(2.0f);

        player.GetComponent<PlayerMovement>().SetRunSpeed(15.0f);
        speedBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<PlayerMovement>().GetRunSpeed(), 30.0f);

        player.GetComponent<PlayerMovement>().SetRunSpeed(29.0f);
        speedBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<PlayerMovement>().GetRunSpeed(), 30.0f);

        player.GetComponent<PlayerMovement>().SetRunSpeed(30.0f);
        speedBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<PlayerMovement>().GetRunSpeed(), 30.0f);

        Object.Destroy(player);
    }

    [Test]
    public void PlayerGetHealBuff()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        HealBuff healBuff = new HealBuff();
        healBuff.SetHealth(50);

        player.GetComponent<Health>().SetHealth(50);
        player.GetComponent<Health>().SetMaxHealth(100);
        healBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 100);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 100);

        player.GetComponent<Health>().SetHealth(90);
        player.GetComponent<Health>().SetMaxHealth(100);
        healBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 100);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 100);

        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(100);
        healBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 100);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 100);
        Object.Destroy(player);
    }

    [Test]
    public void PlayerGetHealthBuff()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        HealthBuff healthBuff = new HealthBuff();
        healthBuff.SetMultiplier(2.0f);

        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(100);
        healthBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 200);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 200);

        player.GetComponent<Health>().SetHealth(250);
        player.GetComponent<Health>().SetMaxHealth(250);
        healthBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 250);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 250);

        player.GetComponent<Health>().SetHealth(80);
        player.GetComponent<Health>().SetMaxHealth(100);
        healthBuff.Apply(player);

        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 160);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 200);

        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(200);
        healthBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 125);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 250);

        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(250);
        healthBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<Health>().GetHealth(), 100);
        Assert.AreEqual(player.GetComponent<Health>().GetMaxHealth(), 250);

        Object.Destroy(player);
    }

    [Test]
    public void PlayerGetDoubleJumpBuff()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<PlayerController>().SetDoubleJump(false);
        JumpBuff jumpBuff = new JumpBuff();
        jumpBuff.Apply(player);
        Assert.IsTrue(player.GetComponent<PlayerController>().GetDoubleJump());

        player.GetComponent<PlayerController>().SetDoubleJump(true);
        jumpBuff.Apply(player);
        Assert.IsTrue(player.GetComponent<PlayerController>().GetDoubleJump());
        Object.Destroy(player);
    }

    [Test]
    public void PlayerGetWeapon()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        PlayerWeapon weapon = new PlayerWeapon();
        weapon.SetWeaponDamage(25);
        weapon.SetWeaponAnimationLayerPos(2);
        weapon.SetPlayerWeaponAttackCooldown(1.0f);

        player.GetComponent<Attack>().SetDamage(0);
        player.GetComponent<Attack>().SetWeaponAnimation(0);
        player.GetComponent<Attack>().SetAttackCooldown(0);

        weapon.Apply(player);

        Assert.AreEqual(player.GetComponent<Attack>().GetDamage(), 25);
        Assert.AreEqual(player.GetComponent<Attack>().GetWeaponAnimation(), 2);
        Assert.AreEqual(player.GetComponent<Attack>().GetAttackCooldown(), 1.0f, 0.0001f);

        Object.Destroy(player);
    }

    [Test]
    public void PlayerGetDashBuff()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        DashBuff dashBuff = new DashBuff();
        dashBuff.SetCooldownReduction(0.5f);

        player.GetComponent<PlayerController>().SetDashCooldown(1.5f);
        dashBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<PlayerController>().GetDashCooldown(), 1.0f, 0.0001f);

        player.GetComponent<PlayerController>().SetDashCooldown(1.2f);
        dashBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<PlayerController>().GetDashCooldown(), 0.7f, 0.0001f);

        player.GetComponent<PlayerController>().SetDashCooldown(0.6f);
        dashBuff.Apply(player);
        Assert.AreEqual(player.GetComponent<PlayerController>().GetDashCooldown(), 0.5f, 0.0001f);

        Object.Destroy(player);
    }

}

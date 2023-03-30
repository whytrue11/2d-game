using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class RestrictionsTests
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
    public void PlayerNotReachMaxSpeed()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        SpeedBuff speedBuff = new SpeedBuff();
        speedBuff.SetMultiplier(2.0f);

        player.GetComponent<PlayerMovement>().SetRunSpeed(15.0f);
        Assert.IsTrue(speedBuff.CanGetEffect(player));

        player.GetComponent<PlayerMovement>().SetRunSpeed(29.0f);
        Assert.IsTrue(speedBuff.CanGetEffect(player));

        Object.Destroy(player);
    }

    [Test]
    public void PlayerReachMaxSpeed()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<PlayerMovement>().SetRunSpeed(30.0f);
        SpeedBuff speedBuff = new SpeedBuff();
        speedBuff.SetMultiplier(2.0f);

        Assert.IsFalse(speedBuff.CanGetEffect(player));
        Object.Destroy(player);
    }

    [Test]
    public void PlayerNotFullHealth()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        HealBuff healBuff = new HealBuff();
        healBuff.SetHealth(50);

        player.GetComponent<Health>().SetHealth(50);
        player.GetComponent<Health>().SetMaxHealth(100);
        Assert.IsTrue(healBuff.CanGetEffect(player));


        player.GetComponent<Health>().SetHealth(90);
        player.GetComponent<Health>().SetMaxHealth(100);
        Assert.IsTrue(healBuff.CanGetEffect(player));

        Object.Destroy(player);
    }

    [Test]
    public void PlayerFullHealth()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        HealBuff healBuff = new HealBuff();
        healBuff.SetHealth(50);

        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(100);
        Assert.IsFalse(healBuff.CanGetEffect(player));

        Object.Destroy(player);
    }

    [Test]
    public void PlayerNotReachMaxHealth()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        HealthBuff healthBuff = new HealthBuff();
        healthBuff.SetMultiplier(2.0f);

        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(100);
        Assert.IsTrue(healthBuff.CanGetEffect(player));

        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(249);
        Assert.IsTrue(healthBuff.CanGetEffect(player));

        Object.Destroy(player);
    }

    [Test]
    public void PlayerReachMaxHealth()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        HealthBuff healthBuff = new HealthBuff();
        healthBuff.SetMultiplier(2.0f);

        player.GetComponent<Health>().SetHealth(100);
        player.GetComponent<Health>().SetMaxHealth(250);
        Assert.IsFalse(healthBuff.CanGetEffect(player));

        Object.Destroy(player);
    }

    [Test]
    public void PlayerDontHaveDoubleJump()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<PlayerController>().SetDoubleJump(false);
        JumpBuff jumpBuff = new JumpBuff();
 
        Assert.IsTrue(jumpBuff.CanGetEffect(player));
        Object.Destroy(player);
    }

    [Test]
    public void PlayerHaveDoubleJump()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        player.GetComponent<PlayerController>().SetDoubleJump(true);
        JumpBuff jumpBuff = new JumpBuff();

        Assert.IsFalse(jumpBuff.CanGetEffect(player));
        Object.Destroy(player);
    }

    [Test]
    public void PlayerNotReachMinDashCooldown()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        DashBuff dashBuff = new DashBuff();
        dashBuff.SetCooldownReduction(0.5f);

        player.GetComponent<PlayerController>().SetDashCooldown(1.5f);
        Assert.IsTrue(dashBuff.CanGetEffect(player));

        player.GetComponent<PlayerController>().SetDashCooldown(0.6f);
        Assert.IsTrue(dashBuff.CanGetEffect(player));

        Object.Destroy(player);
    }

    [Test]
    public void PlayerReachMinDashCooldown()
    {
        GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Client/Prefabs/Tests/TestPlayer.prefab");
        player = GameObject.Instantiate(player);
        DashBuff dashBuff = new DashBuff();
        dashBuff.SetCooldownReduction(0.5f);

        player.GetComponent<PlayerController>().SetDashCooldown(0.5f);
        Assert.IsFalse(dashBuff.CanGetEffect(player));

        Object.Destroy(player);
    }

    [Test]
    public void WrongParametersForItems()
    {
        GameObject player = new GameObject();
        player = GameObject.Instantiate(player);
        SpeedBuff speedBuff = new SpeedBuff();
        HealBuff healBuff = new HealBuff();
        HealthBuff healthBuff = new HealthBuff();
        JumpBuff jumpBuff = new JumpBuff();
        PlayerWeapon weapon = new PlayerWeapon();
        DashBuff dashBuff = new DashBuff();

        Assert.IsFalse(speedBuff.CanGetEffect(player));
        Assert.IsFalse(healBuff.CanGetEffect(player));
        Assert.IsFalse(healthBuff.CanGetEffect(player));
        Assert.IsFalse(jumpBuff.CanGetEffect(player));
        Assert.IsFalse(weapon.CanGetEffect(player));
        Assert.IsFalse(dashBuff.CanGetEffect(player));

        Object.Destroy(player);
    }

}

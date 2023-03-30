using System;
using System.Collections.Generic;

public static class DataHolder
{
    public static Coin coins;
    public static List<Timer.Score> scores;

    public static DateTime startTime;
    public static Timer.Score currentScore;

    public static Coin localCoins;

    public static float musicVolume;

    public static bool playerDoubleJumpBuff;
    public static int playerDamage;
    public static int playerWeaponAnimation;
    public static float playerWeaponAttackCooldown;
    public static float playerRunSpeed;
    public static int playerCurrentHealth;
    public static int playerMaxHealth;

    static DataHolder()
    {
        SetDefault();
    }

    public static void SetDefault()
    {
        playerDoubleJumpBuff = false;
        playerDamage = 50;
        playerWeaponAnimation = 0;
        playerWeaponAttackCooldown = 0.5f;
        playerRunSpeed = 15;
        playerCurrentHealth = 100;
        playerMaxHealth = 100;
    }
}
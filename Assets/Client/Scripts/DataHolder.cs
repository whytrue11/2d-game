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

    public static bool playerDoubleJumpBuff = true;
    public static int playerDamage = 100;
    public static int playerWeaponAnimation = 0;
    public static float playerWeaponAttackCooldown = 0.5f;
    public static float playerRunSpeed = 15;
    public static int playerCurrentHealth = 100;
    public static int playerMaxHealth = 100;
}

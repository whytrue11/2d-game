using UnityEngine;

public class Weapon
{
    private PowerUpEffect weapon;
    private Sprite weaponSprite;
    public Weapon(PowerUpEffect weapon, Sprite weaponSprite)
    {
        this.weapon = weapon;
        this.weaponSprite = weaponSprite;
    }
    public PowerUpEffect GetWeapon()
    {
        return weapon;
    }
    public Sprite GetWeaponSprite()
    {
        return weaponSprite;
    }
    public string GetWeaponDescription()
    {
        return weapon.GetDescription();
    }
}
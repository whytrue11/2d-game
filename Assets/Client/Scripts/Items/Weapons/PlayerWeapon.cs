using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Weapon")]
public class PlayerWeapon : PowerUpEffect
{
    [SerializeField] private int weaponAnimationLayerPos;
    [SerializeField] private int price;
    [SerializeField] private int weaponDamage;
    [SerializeField] private float playerWeaponAttackCooldown;
    [SerializeField] private string description;
 
    public override void Apply(GameObject player)
    {
        Attack playerAttack = player.GetComponentInParent(typeof(Attack)) as Attack;
        if (CanGetEffect(player))
        {
            playerAttack.SetDamage(playerAttack.GetDamage() + weaponDamage);
            playerAttack.SetWeaponAnimation(weaponAnimationLayerPos);
            playerAttack.SetAttackCooldown(playerWeaponAttackCooldown);
        }
    }
    public override bool CanGetEffect(GameObject player)
    {
        Attack playerAttack = player.GetComponentInParent(typeof(Attack)) as Attack;
        if (playerAttack != null)
        {
            return true;
        }
        return false;
    }
    public override int GetPrice()
    {
        return price;
    }

    public override string GetDescription()
    {
        return description;
    }

    public void SetWeaponDamage(int value)
    {
        weaponDamage = value;
    }

    public void SetPlayerWeaponAttackCooldown(float value)
    {
        playerWeaponAttackCooldown = value;
    }

    public void SetWeaponAnimationLayerPos(int value)
    {
        weaponAnimationLayerPos = value;
    }

}

using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HealBuff")]
public class HealBuff : PowerUpEffect
{
    [SerializeField] private int health;
    [SerializeField] private int price;
    [SerializeField] private string description;
    public override void Apply(GameObject player)
    {
        Health playerHealth = player.GetComponentInParent(typeof(Health)) as Health;
        PlayerController playerController = player.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        if (CanGetEffect(player))
        {
            playerController.HealAnimation();
            playerHealth.HealUnit(health);
        }
    }
    public override bool CanGetEffect(GameObject player)
    {
        Health playerHealth = player.GetComponentInParent(typeof(Health)) as Health;
        PlayerController playerController = player.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        if (playerHealth != null && playerController != null)
        {
            return (playerHealth.GetHealth() < playerHealth.GetMaxHealth());
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

    public void SetHealth(int value)
    {
        health = value;
    }

}

using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HealthBuff")]
public class HealthBuff : PowerUpEffect
{
    [SerializeField] private float multiplier;
    [SerializeField] private int price;
    [SerializeField] private string description;

    public override void Apply(GameObject player)
    {
        Health playerHealth = player.GetComponentInParent(typeof(Health)) as Health;
        playerHealth.SetMaxHealth((int)(playerHealth.GetMaxHealth() * multiplier));
        playerHealth.SetHealth((int)(playerHealth.GetHealth() * multiplier));
        PlayerController playerController = player.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        playerController.HealAnimation();
    }
    public override bool CanGetEffect(GameObject player)
    {
        Health playerHealth = player.GetComponentInParent(typeof(Health)) as Health;
        if (playerHealth != null)
        {
            return (playerHealth.GetMaxHealth() * multiplier <= 250);
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

}

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
      
        playerHealth.SetHealth(playerHealth.GetHealth() + health);
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

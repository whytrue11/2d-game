using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DashBuff")]
public class DashBuff : PowerUpEffect
{
    [SerializeField] private float cooldownReduction;
    [SerializeField] private int price;
    [SerializeField] private string description;

    public override void Apply(GameObject player)
    {
        PlayerController playerController = player.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        if (CanGetEffect(player))
        {
            if(playerController.GetDashCooldown() - cooldownReduction >= 0.5f)
            {
                playerController.SetDashCooldown(playerController.GetDashCooldown() - cooldownReduction);
            }
            else
            {
                playerController.SetDashCooldown(0.5f);
            }
        }
    }

    public override bool CanGetEffect(GameObject player)
    {
        PlayerController playerController = player.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        if (playerController != null)
        {
            return (playerController.GetDashCooldown() > 0.5f);
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
    public void SetCooldownReduction(float value)
    {
        cooldownReduction = value;
    }
}

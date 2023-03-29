using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBuff")]
public class SpeedBuff : PowerUpEffect
{
    [SerializeField] private float multiplier;
    [SerializeField] private int price;
    [SerializeField] private string description;

    public override void Apply(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponentInParent(typeof(PlayerMovement)) as PlayerMovement;
        if(CanGetEffect(player))
        {
            if(playerMovement.GetRunSpeed() * multiplier <= 30.0f)
            {
                playerMovement.SetRunSpeed(playerMovement.GetRunSpeed() * multiplier);
            }
            else
            {
                playerMovement.SetRunSpeed(30.0f);
            }
        }
    }

    public override bool CanGetEffect(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponentInParent(typeof(PlayerMovement)) as PlayerMovement;
        if (playerMovement != null)
        {
            return (playerMovement.GetRunSpeed() < 30.0f);
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
    public void SetMultiplier(float value)
    {
        multiplier = value;
    }
}

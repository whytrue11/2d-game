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
        if (playerMovement != null)
        {
            playerMovement.SetRunSpeed(playerMovement.GetRunSpeed() * multiplier);
        }
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

using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBuff")]
public class SpeedBuff : PowerUpEffect
{
    [SerializeField] private float runSpeed;
    [SerializeField] private int price;
    public override void Apply(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponentInParent(typeof(PlayerMovement)) as PlayerMovement;
        if (playerMovement != null)
        {
            playerMovement.SetRunSpeed(runSpeed);
        }
    }
    public override int getPrice()
    {
        return price;
    }
}

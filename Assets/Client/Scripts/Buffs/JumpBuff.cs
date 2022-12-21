using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/JumpBuff")]
public class JumpBuff : PowerUpEffect
{
    [SerializeField] private int price;
    [SerializeField] private string description;
    public override void Apply(GameObject player)
    {
        PlayerController playerController = player.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        if (playerController != null)
        {
            playerController.setDoubleJump(true);       
        }
    }
    public override int getPrice()
    {
        return price;
    }

    public override string getDescription()
    {
        return description;
    }
}

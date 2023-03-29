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
            playerController.SetDoubleJump(true);       
        }
    }
    public override bool CanGetEffect(GameObject player)
    {
        PlayerController playerController = player.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        if (playerController != null)
        {
            return (!playerController.GetDoubleJump());
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

using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HealthBuff")]
public class HealthBuff : PowerUpEffect
{
    [SerializeField] private float multiplier;
    [SerializeField] private int price;

    public override void Apply(GameObject player)
    {
        // increase character hp by multiplier
    }
    public override int getPrice()
    {
        return price;
    }

}

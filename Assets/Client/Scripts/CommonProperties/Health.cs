using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int health;

    [SerializeField] private int maxHealth;

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }
        
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }
        
        
    public void DmgUnit(int dmgAmount)
    {
        if (health > 0)
        {
            health-= dmgAmount;
        }
    }

    public void HealUnit(int healAmount)
    {
        if (health < maxHealth)
        {
            health += healAmount;
        }
        else
        {
            health = maxHealth;
        }
    }
}
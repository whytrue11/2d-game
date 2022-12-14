using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;

    [SerializeField] private int maxHealth;

    [SerializeField] private Image hpBar;

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
        DiplayHPBar();
    }
        
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        DiplayHPBar();
    }
        
        
    public void DmgUnit(int dmgAmount)
    {
        if (health > 0)
        {
            health -= dmgAmount;
        }
        DiplayHPBar();
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

        DiplayHPBar();
    }

    public void DiplayHPBar()
    {
        if (!hpBar.IsUnityNull())
        {
            hpBar.fillAmount = (float) health / maxHealth;
            Debug.Log("HPBar updated " + (float) health / maxHealth);
        }
    }
}
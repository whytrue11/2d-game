using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    [SerializeField] private List<PowerUpEffect> buffs;
    [SerializeField] private List<Sprite> buffSprites;

    [SerializeField] private List<PowerUpEffect> weapons;
    [SerializeField] private List<Sprite> weaponSprites;

    private GameManager gameManager;
    private bool enemiesNearby = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Utils").GetComponent<GameManager>();
    }

    public int GetCoins()
    {
        return gameManager.GetCoins();
    }
    public void RemoveCoins(int coins)
    {
        gameManager.RemoveCoins(coins);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Enemy"))
        {
            enemiesNearby = true;
        }       
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Enemy"))
        {
            enemiesNearby = false;
        }
    }

    public bool GetEnemiesNearby()
    {
        return enemiesNearby;
    }


    public Buff GetBuff()
    {
        if(DataHolder.playerDoubleJumpBuff)
        {
            for (int i = 0; i < buffs.Count; i ++)
            {
                if (buffs[i].GetDescription().Equals("Двойной прыжок"))
                {
                    buffs.RemoveAt(i);
                    buffSprites.RemoveAt(i);
                }
            }
        }
        int pos = Random.Range(0, buffs.Count);
        PowerUpEffect buff = buffs[pos];
        buff = buffs[pos];
        buffs.RemoveAt(pos);
        Sprite buffSprite = buffSprites[pos];
        buffSprites.RemoveAt(pos);
        return new Buff(buff, buffSprite);
    }

    public Weapon GetWeapon()
    {
        int pos = Random.Range(0, weapons.Count);
        PowerUpEffect weapon = weapons[pos];
        weapons.RemoveAt(pos);
        Sprite weaponSprite = weaponSprites[pos];
        weaponSprites.RemoveAt(pos);
        return new Weapon(weapon, weaponSprite);
    }

}

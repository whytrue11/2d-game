using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    [SerializeField] private List<PowerUpEffect> buffs;
    [SerializeField] private List<Sprite> buffSprites;
    private GameManager gameManager;

    private void Awake()
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
    public Buff GetBuff()
    {
        int pos = Random.Range(0, buffs.Count);
        PowerUpEffect buff = buffs[pos];
        buffs.RemoveAt(pos);
        Sprite buffSprite = buffSprites[pos];
        buffSprites.RemoveAt(pos);
        return new Buff(buff, buffSprite);
    }


}

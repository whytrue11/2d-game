using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<PowerUpEffect> buffs;
    [SerializeField] private List<Sprite> buffSprites;
    public int GetCoins()
    {
        return DataHolder.coins.GetCoins();
    }
    public void RemoveCoins(int coins)
    {
        DataHolder.coins.RemoveCoins(coins);
        gameManager.DisplayCoins();
    }
    public Buff GetBuff()
    {
        int pos = Random.Range(0, buffs.Count - 1);
        PowerUpEffect buff = buffs[pos];
        buffs.RemoveAt(pos);
        Sprite buffSprite = buffSprites[pos];
        buffSprites.RemoveAt(pos);
        Debug.Log(pos);
        return new Buff(buff, buffSprite);
    }


}

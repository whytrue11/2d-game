using UnityEngine;

public class Buff
{
    private PowerUpEffect buff;
    private Sprite buffSprite;

    public Buff(PowerUpEffect buff, Sprite buffSprite)
    {
        this.buff = buff;
        this.buffSprite = buffSprite;
    }
    public PowerUpEffect GetBuffEffect()
    {
        return buff;
    }
    public Sprite GetBuffSprite()
    {
        return buffSprite;
    }

    public string GetBuffDescription()
    {
        return buff.GetDescription();
    }
}

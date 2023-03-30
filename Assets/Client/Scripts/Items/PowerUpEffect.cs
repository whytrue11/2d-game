using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{

    public abstract void Apply(GameObject player);
    public abstract bool CanGetEffect(GameObject player);
    public abstract int GetPrice();
    public abstract string GetDescription();

}

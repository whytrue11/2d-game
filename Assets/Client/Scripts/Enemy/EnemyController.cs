using UnityEngine;
using UnityEngine.Serialization;

public abstract class EnemyController : MonoBehaviour
{

    private void Start()
    {
    }

    private void Update()
    {
    }

    public abstract void TakeDamage(int damage);
}
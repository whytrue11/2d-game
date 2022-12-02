using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Health enemyHealth;

    [FormerlySerializedAs("unitAttack")] [SerializeField]
    private Attack attack;

    private Rigidbody2D rigidBody2D;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<Health>();
        attack = GetComponent<Attack>();
    }

    private void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        enemyHealth.DmgUnit(damage);
        if (enemyHealth.GetHealth() <= 0)
        {
            Debug.Log("Enemy death");
            Destroy(gameObject);
        }
    }
}
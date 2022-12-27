using UnityEngine;

public class EnemyPatrolHead : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy death");
            FindObjectOfType<GameManager>().AddCoins(1);
            Destroy(transform.parent.gameObject);
        }
    }
}

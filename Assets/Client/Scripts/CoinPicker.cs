using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private void OnTriggerEnter2D(Collider2D coinCollider)
    {
        if (coinCollider.gameObject.CompareTag("Coin"))
        {
            gameManager.AddCoins(1);
            Destroy(coinCollider.gameObject);
        }
    }
}

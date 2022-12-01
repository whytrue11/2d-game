using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private void OnTriggerEnter2D(Collider2D coinCollider)
    {
        if (coinCollider.gameObject.CompareTag("Coin"))
        {
            DataHolder.coins.AddCoins(1);
            gameManager.DisplayCoins();
            Destroy(coinCollider.gameObject);
        }
    }
}

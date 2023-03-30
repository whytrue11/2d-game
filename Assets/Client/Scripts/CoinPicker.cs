using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    private GameManager gameManager;
    
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Utils").GetComponent<GameManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D coinCollider)
    {
        if (coinCollider.gameObject.tag.Contains("Coin"))
        {
            gameManager.AddCoins(1);
            Destroy(coinCollider.gameObject);
        }
    }
}

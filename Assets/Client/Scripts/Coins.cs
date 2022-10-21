using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    private int coins = 0;

    private void OnTriggerEnter2D(Collider2D coinCollider)
    {
        if (coinCollider.gameObject.CompareTag("Coin"))
        {
            ++coins;
            coinsText.text = coins.ToString();
            Destroy(coinCollider.gameObject);
        }
    }
}

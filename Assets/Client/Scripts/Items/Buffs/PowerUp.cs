using UnityEngine;
using TMPro;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] GameObject pickUpButton;
    [SerializeField] GameObject attackButton;
    private PlayerController playerController;
    private GameObject player;
    private Buff buff;

    public void Awake()
    {
        buff = GetComponentInParent<Shop>().GetBuff();
        GetComponent<SpriteRenderer>().sprite = buff.GetBuffSprite();
        priceText.text = buff.GetBuffEffect().GetPrice().ToString();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponentInParent(typeof(PlayerController)) as PlayerController;
            if(playerController != null)
            {
                this.playerController = (PlayerController)collision.GetComponentInParent(typeof(PlayerController));
                this.playerController.NextToTheBuff(this);
                player = collision.gameObject;
                descriptionText.text = buff.GetBuffDescription();
                descriptionText.fontSize = 6.5f;
                pickUpButton.SetActive(true);
                attackButton.SetActive(false);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.NotNextToTheBuff();
            playerController = null;
            player = null;
            descriptionText.text = "";
            pickUpButton.SetActive(false);
            attackButton.SetActive(true);
        }
    }

    public IEnumerator Apply()
    {
        Shop shop = GetComponentInParent(typeof(Shop)) as Shop;
        if(shop != null)
        {
            if (shop.GetCoins() >= buff.GetBuffEffect().GetPrice())
            {
                shop.RemoveCoins(buff.GetBuffEffect().GetPrice());
                buff.GetBuffEffect().Apply(player);
                playerController.NotNextToTheBuff();
                if (buff.GetBuffEffect().GetDescription().Equals("Двойной прыжок"))
                {
                    DataHolder.playerDoubleJumpBuff = true;
                }
                Destroy(gameObject);
            }
            else 
            {
                Color priceColor = new Color(1.0f, 0.6662316f, 0.0f);
                priceText.color = Color.red;
                yield return new WaitForSeconds(2);
                priceText.color = priceColor;
            }
        } 
    }
}

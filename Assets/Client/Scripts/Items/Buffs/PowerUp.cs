using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text descriptionText;
    private GameObject attackButton;
    private GameObject pickUpButton;
    private PlayerController playerController;
    private GameObject player;
    private Buff buff;

    public TMP_Text GetPriceText()
    {
        return priceText;
    }

    public TMP_Text GetDescriptionText()
    {
        return descriptionText;
    }

    public void Start()
    {
        buff = GetComponentInParent<Shop>().GetBuff();
        GetComponent<SpriteRenderer>().sprite = buff.GetBuffSprite();
        priceText.text = buff.GetBuffEffect().GetPrice().ToString();
        attackButton = GameObject.FindGameObjectWithTag("AttackButton");
        pickUpButton = GameObject.FindGameObjectWithTag("PickUpButton");
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
                if(!GetComponentInParent<Shop>().getEnemiesNearby())
                {
                    descriptionText.text = buff.GetBuffDescription();
                    descriptionText.fontSize = 2.8f;
                    pickUpButton.SetActive(true);
                    attackButton.SetActive(false);
                    pickUpButton.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    pickUpButton.SetActive(false);
                    attackButton.SetActive(true);
                }

            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (!GetComponentInParent<Shop>().getEnemiesNearby())
            { 
                pickUpButton.SetActive(true);
                attackButton.SetActive(false);
            }
            else
            {
                pickUpButton.SetActive(false);
                attackButton.SetActive(true);
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
        if(shop != null && !shop.getEnemiesNearby())
        {
            if (buff.GetBuffEffect().CanGetEffect(player))
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
            else
            {
                descriptionText.text = "Вы достигли лимита";
                descriptionText.fontSize = 2.8f;
                Color priceColor = new Color(1.0f, 0.6662316f, 0.0f);
                priceText.color = Color.red;
                yield return new WaitForSeconds(2);
                priceText.color = priceColor;
            } 
        } 
    }
}

using UnityEngine;
using TMPro;


public class PowerUp : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    private PlayerController playerController;
    private GameObject player;
    private Buff buff;

    public void Awake()
    {
        Shop shop = GetComponentInParent<Shop>();
        buff = shop.GetBuff();
        GetComponent<SpriteRenderer>().sprite = buff.GetBuffSprite();
        priceText.text = buff.GetBuffEffect().getPrice().ToString();
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

        }
    }

    public void Apply()
    {
        Shop shop = GetComponentInParent(typeof(Shop)) as Shop;
        if(shop != null)
        {
            if (shop.GetCoins() >= buff.GetBuffEffect().getPrice())
            {
                shop.RemoveCoins(buff.GetBuffEffect().getPrice());
                buff.GetBuffEffect().Apply(player);
                Destroy(gameObject);
            } 
        } 
    }
}

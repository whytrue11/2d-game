using UnityEngine;
using TMPro;
using System.Collections;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] GameObject pickUpButton;
    [SerializeField] GameObject attackButton;
    private PlayerController playerController;
    private GameObject player;
    private Weapon weapon;

    public void Awake()
    {
        weapon = GetComponentInParent<Shop>().GetWeapon();
        GetComponent<SpriteRenderer>().sprite = weapon.GetWeaponSprite();
        priceText.text = weapon.GetWeapon().GetPrice().ToString();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponentInParent(typeof(PlayerController)) as PlayerController;
            if(playerController != null)
            {
                this.playerController = (PlayerController)collision.GetComponentInParent(typeof(PlayerController));
                this.playerController.NextToTheWeapon(this);
                player = collision.gameObject;
                descriptionText.text = weapon.GetWeaponDescription();
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
            playerController.NotNextToTheWeapon();
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
            if (shop.GetCoins() >= weapon.GetWeapon().GetPrice())
            {
                shop.RemoveCoins(weapon.GetWeapon().GetPrice());
                weapon.GetWeapon().Apply(player);
                playerController.NotNextToTheWeapon();
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

using UnityEngine;

public class BossBulletController : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            var controller = collision.gameObject.GetComponentInParent<PlayerController>();
            controller.PlayerDmg(10);
            Destroy(gameObject);    
        }      
    }
}
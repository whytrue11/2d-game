using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBulletController : MonoBehaviour {

    float moveSpeed = 1f;

    Rigidbody2D rb;

    

    // Use this for initialization
    // void Start () {
    //     rb = GetComponent<Rigidbody2D> ();
    //     target = GameObject.FindGameObjectWithTag("Player");
    //     moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
    //     rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
    //     //Destroy (gameObject, 1f);
    // }
    //
    // void OnTriggerEnter2D (Collider2D col)
    // {
    //     if (col.gameObject.name.Equals ("Player") && gameObject != null) {
    //         Debug.Log ("Hit!");
    //         var controller = col.gameObject.GetComponentInParent<PlayerController>();
    //         controller.PlayerDmg(30);
    //        // Destroy (gameObject);
    //     }
    // }
    
    
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
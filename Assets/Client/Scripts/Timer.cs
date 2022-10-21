using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private DateTime time;

    void Start()
    {
        time = DateTime.Now;
    }
    
    private void OnTriggerEnter2D(Collider2D coinCollider)
    {
        if (coinCollider.gameObject.tag == "Coin")
        {
            timerText.text = DateTime.Now.Subtract(time).ToString(@"hh\:mm\:ss");
        }
    }
}

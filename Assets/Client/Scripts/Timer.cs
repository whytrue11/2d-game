using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private TMP_Text timerText;
    private DateTime startTime;

    void Start()
    {
        startTime = DateTime.Now;
    }
    
    public class Score
    {
        public TimeSpan time;
        public DateTime date;
    }

    private void OnTriggerEnter2D(Collider2D coinCollider)
    {
        if (coinCollider.gameObject.tag == "Coin")
        {
            TimeSpan time = DateTime.Now.Subtract(startTime);
            timerText.text = time.ToString(@"dd") + "d " + time.ToString(@"hh\:mm\:ss");
            gameManager.scores.Add(new Score() {time = time, date = DateTime.Now});
        }
    }
}

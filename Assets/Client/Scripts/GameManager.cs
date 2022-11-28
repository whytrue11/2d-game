using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreBoardText;
    [SerializeField] private TMP_Text coinsText;
    public Coin coins;
    public List<Timer.Score> scores;

    private void Awake()
    {
        DisplayCoins();
    }

    //temp score board display
    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            String scoreBoard = "";
            foreach (Timer.Score score in scores)
            {
                scoreBoard += score.date.ToShortDateString() + " " + score.time.ToString(@"hh\:mm\:ss") + "\n";
            }

            scoreBoardText.text = scoreBoard;
        }
        else
        {
            scoreBoardText.text = " ";
        }
    }

    public void DisplayCoins()
    {
        coinsText.text = coins.GetCoins().ToString();
    }
}

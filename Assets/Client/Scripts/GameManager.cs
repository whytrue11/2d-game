using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreBoardText;
    [SerializeField] private TMP_Text coinsText;
    private int coins = 0;
    private List<Timer.Score> scores = new List<Timer.Score>();

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

    public int GetCoins()
    {
        return coins;
    }

    public List<Timer.Score> GetScores()
    {
        return scores;
    }

    public void AddCoins(int coins)
    {
        this.coins += coins;
        coinsText.text = this.coins.ToString();
    }

    public void AddScore(Timer.Score score)
    {
        scores.Add(score);
    }
}

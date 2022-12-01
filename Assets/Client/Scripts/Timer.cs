using System;
using TMPro;
using UnityEngine;

public class Timer: MonoBehaviour
{
    private bool pause = false;
    
    public void Begin()
    {
        DataHolder.startTime = DateTime.Now;
        DataHolder.currentScore = new Score()
        {
            time = new TimeSpan(0, 0 , 0),
            date = DateTime.Now
        };
    }

    public void Pause()
    {
        DataHolder.currentScore.time = DataHolder.currentScore.time.Add(DateTime.Now.Subtract(DataHolder.startTime));
        pause = true;
        Debug.Log("Pause: " + DataHolder.currentScore.time.ToString(@"hh\:mm\:ss") + "\n");
    }
    
    public void Unpause()
    {
        DataHolder.startTime = DateTime.Now;
        pause = false;
        Debug.Log("Unpause: " + DataHolder.currentScore.time.ToString(@"hh\:mm\:ss") + "\n");
    }

    public void End()
    {
        if (!pause)
        {
            DataHolder.currentScore.time = DataHolder.currentScore.time.Add(DateTime.Now.Subtract(DataHolder.startTime));
        }
        DataHolder.currentScore.date = DateTime.Now;
        DataHolder.scores.Add(DataHolder.currentScore);
        Debug.Log("Add score");
    }

    public struct Score
    {
        public TimeSpan time;
        public DateTime date;
    }
}

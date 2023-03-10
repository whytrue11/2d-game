using System;
using TMPro;
using UnityEngine;

public static class Timer
{
    public static void Begin()
    {
        DataHolder.startTime = DateTime.Now;
        DataHolder.currentScore = new Score()
        {
            time = new TimeSpan(0, 0 , 0),
            date = DateTime.Now
        };
        Debug.Log("Begin: " + DataHolder.currentScore.time.ToString(@"hh\:mm\:ss") + "\n");
    }

    public static void Pause()
    {
        DataHolder.currentScore.time = DataHolder.currentScore.time.Add(DateTime.Now.Subtract(DataHolder.startTime));
        Debug.Log("Pause: " + DataHolder.currentScore.time.ToString(@"hh\:mm\:ss") + "\n");
    }
    
    public static void Unpause()
    {
        DataHolder.startTime = DateTime.Now;
        Debug.Log("Unpause: " + DataHolder.currentScore.time.ToString(@"hh\:mm\:ss") + "\n");
    }

    public static void End(bool pause)
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

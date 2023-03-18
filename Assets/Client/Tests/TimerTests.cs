using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

public class TimerTests
{
    [Test]
    public void Begin()
    {
        DateTime expectedStartTime = DateTime.Now;
        Timer.Score expectedScore = new Timer.Score()
        {
            date = DateTime.Now,
            time = new TimeSpan(0, 0, 0)
        };
        
        Timer.Begin();
        
        Assert.AreEqual(expectedStartTime.Year, DataHolder.startTime.Year);
        Assert.AreEqual(expectedStartTime.DayOfYear, DataHolder.startTime.DayOfYear);
        Assert.AreEqual(expectedScore.time, DataHolder.currentScore.time);
    }
    
    [Test]
    public void End()
    {
        DataHolder.scores = new List<Timer.Score>();
        int millisecondsSleep = 100;
        int admittedMillisecondsDelay = 15;
        Timer.Score expectedScore = new Timer.Score()
        {
            date = DateTime.Now,
            time = new TimeSpan(0, 0, 0, 0, millisecondsSleep)
        };
        
        Timer.Begin();
        Thread.Sleep(millisecondsSleep);
        Timer.End(false);

        Assert.True(DataHolder.scores.Count == 1);
        Assert.AreEqual(expectedScore.date.Date, DataHolder.scores[0].date.Date);
        Assert.Less(DataHolder.currentScore.time.Milliseconds - expectedScore.time.Milliseconds, admittedMillisecondsDelay);
    }
    
    [Test]
    public void EndOnPause()
    {
        DataHolder.scores = new List<Timer.Score>();
        int millisecondsSleep = 100;
        int admittedMillisecondsDelay = 15;
        Timer.Score expectedScore = new Timer.Score()
        {
            date = DateTime.Now,
            time = new TimeSpan(0, 0, 0, 0, millisecondsSleep)
        };
        
        Timer.Begin();
        Thread.Sleep(millisecondsSleep);
        Timer.Pause();
        Thread.Sleep(admittedMillisecondsDelay * 2);
        Timer.End(true);

        Assert.True(DataHolder.scores.Count == 1);
        Assert.AreEqual(expectedScore.date.Date, DataHolder.scores[0].date.Date);
        Assert.Less(DataHolder.currentScore.time.Milliseconds - expectedScore.time.Milliseconds, admittedMillisecondsDelay);
    }
    
    [Test]
    public void Pause()
    {
        int millisecondsSleep = 100;
        int admittedMillisecondsDelay = 15;
        Timer.Score expectedScore = new Timer.Score()
        {
            date = DateTime.Now,
            time = new TimeSpan(0, 0, 0, 0, millisecondsSleep)
        };
        
        Timer.Begin();
        Thread.Sleep(millisecondsSleep);
        Timer.Pause();
        
        Assert.Less(DataHolder.currentScore.time.Milliseconds - expectedScore.time.Milliseconds, admittedMillisecondsDelay);
    }
    
    [Test]
    public void Unpause()
    {
        int millisecondsSleep = 100;
        int admittedMillisecondsDelay = 15;
        Timer.Score expectedScore = new Timer.Score()
        {
            date = DateTime.Now,
            time = new TimeSpan(0, 0, 0, 0, millisecondsSleep * 2)
        };
        
        Timer.Begin();
        Thread.Sleep(millisecondsSleep);
        Timer.Pause();
        Thread.Sleep(admittedMillisecondsDelay * 2);
        Timer.Unpause();
        Thread.Sleep(millisecondsSleep);
        Timer.End(false);

        Assert.Less(DataHolder.currentScore.time.Milliseconds - expectedScore.time.Milliseconds, admittedMillisecondsDelay);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void ChangeScene(int scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Debug.Log("Exit button pressed");
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    public void DisplayScores(TMP_Text scoreBoardText)
    {
        string scoreBoard = "";
        foreach (Timer.Score score in DataHolder.scores)
        {
            scoreBoard += score.date.ToShortDateString() + " " + score.time.ToString(@"hh\:mm\:ss") + "\n";
        }

        scoreBoardText.text = scoreBoard;
    }

    public void Options()
    {
        Debug.Log("Options button pressed");
    }
}
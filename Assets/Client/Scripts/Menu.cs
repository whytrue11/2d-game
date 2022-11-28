using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
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
        Debug.Log(Time.timeScale);
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void Options()
    {
        Debug.Log("Options button pressed");
    }
}

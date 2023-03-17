using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    [SerializeField] public List<GameObject> rooms;
    [SerializeField] public GameObject endRoom;

    [SerializeField] public int roomsSpawnLimit;

    [SerializeField] private GameObject titre;
    [SerializeField] private TMP_Text titreTitleText;
    [SerializeField] private TMP_Text titreCreatorsNamesText;
    [SerializeField] private TMP_Text continueButtonText;
    
    [SerializeField] private TMP_Text scoreText;
    
    public int roomsSpawned;

    public bool pause;

    private const double coinPercent = (double) 2 / 3;

    private void Start()
    {
        pause = false;
        DisplayCoins();
    }

    public void Begin()
    {
        Timer.Begin();
    }

    public void Pause()
    {
        pause = true;
        Timer.Pause();
    }

    public void Unpause()
    {
        pause = false;
        Timer.Unpause();
    }

    public void End(bool death)
    {
        if (death == false)
        {
            Timer.End(pause);

            scoreText.text = scoreText.text + " " + DataHolder.scores[DataHolder.scores.Count - 1].time.ToString(@"hh\:mm\:ss");
            titre.SetActive(true);
            StartCoroutine("FadeTitre");
        }
        DataHolder.SetDefault();
        LeavePartCoins();
    }

    public int GetCoins()
    {
        return DataHolder.localCoins.GetCoins() + DataHolder.coins.GetCoins(); 
    }

    public void AddCoins(int coins)
    {
        DataHolder.localCoins.AddCoins(coins);
        DisplayCoins();
    }

    public void RemoveCoins(int coins)
    {
        if (DataHolder.coins.GetCoins() + DataHolder.localCoins.GetCoins() < coins)
        {
            Debug.Log("You have less coins than needed");
            return;
        }

        if (DataHolder.localCoins.GetCoins() >= coins)
        {
            DataHolder.localCoins.RemoveCoins(coins);
        }
        else
        {
            coins -= DataHolder.localCoins.GetCoins();
            DataHolder.localCoins.SetCoins(0);
            DataHolder.coins.RemoveCoins(coins);
        }

        DisplayCoins();
    }
    
    private void DisplayCoins()
    {
        if (coinsText != null)
        {
            coinsText.text = (DataHolder.coins.GetCoins() + DataHolder.localCoins.GetCoins()).ToString();
        }
    }
    
    private void LeavePartCoins()
    {
        DataHolder.coins.AddCoins((int)Math.Round(DataHolder.localCoins.GetCoins() * coinPercent));
        DataHolder.localCoins.SetCoins(0);
    }
    
    private IEnumerator FadeTitre()
    {
        Image image = titre.GetComponent<Image>();
        Color imageColor = image.color;
        imageColor.a = 0;
        image.color = imageColor;
        
        Color titleColor = titreTitleText.color;
        titleColor.a = 0;
        titreTitleText.color = titleColor;
        
        Color creatorsNamesColor = titreCreatorsNamesText.color;
        creatorsNamesColor.a = 0;
        titreCreatorsNamesText.color = creatorsNamesColor;
        
        Color continueButtonColor = continueButtonText.color;
        continueButtonColor.a = 0;
        continueButtonText.color = continueButtonColor;

        float fadeTimeSeconds = 2.5f;
        float time = 1f / fadeTimeSeconds;
        float progress = 0;
		
        while (progress < 1)
        {
            progress += time * Time.deltaTime;
            
            imageColor.a = Mathf.Lerp(0, 1, progress);
            image.color = imageColor;
            
            titleColor.a = Mathf.Lerp(0, 1, progress);
            titreTitleText.color = titleColor;
            
            creatorsNamesColor.a = Mathf.Lerp(0, 1, progress);
            titreCreatorsNamesText.color = creatorsNamesColor;
            
            continueButtonColor.a = Mathf.Lerp(0, 1, progress);
            continueButtonText.color = continueButtonColor;
            yield return null;
        }
    }
}

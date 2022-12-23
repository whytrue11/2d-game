using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    [SerializeField] public List<GameObject> rooms;
    [SerializeField] public GameObject endRoom;

    [SerializeField] public int roomsSpawnLimit;
    public int roomsSpawned;

    public bool pause;

    private double coinPercent = (double) 2 / 3;

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
        }
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
        if (!coinsText.IsUnityNull())
        {
            coinsText.text = (DataHolder.coins.GetCoins() + DataHolder.localCoins.GetCoins()).ToString();
        }
    }
    
    private void LeavePartCoins()
    {
        DataHolder.coins.AddCoins((int)Math.Round(DataHolder.localCoins.GetCoins() * coinPercent));
        DataHolder.localCoins.SetCoins(0);
    }
}

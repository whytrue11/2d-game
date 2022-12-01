using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    
    [SerializeField] public List<GameObject> rooms;
    [SerializeField] public GameObject endRoom;
    
    [SerializeField] public int roomsSpawnLimit;
    public int roomsSpawned;

    private void Start()
    {
        DisplayCoins();
    }

    public void DisplayCoins()
    {
        coinsText.text = DataHolder.coins.GetCoins().ToString();
    }
}

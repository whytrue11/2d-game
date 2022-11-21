using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Saves : MonoBehaviour
{
    private String path;
    [SerializeField] private GameManager gameManager;
    
    private void Awake()
    {
        path = Application.persistentDataPath + "/save.json";
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    

    private class SaveData
    {
        public int coins = 0;
        public List<Timer.Score> scores = new List<Timer.Score>();
    }

    public void Save()
    {
        SaveData data = new SaveData()
        {
            coins = gameManager.GetCoins(),
            scores = gameManager.GetScores()
        };

        File.WriteAllText(path,
            JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            })
        );
        Debug.Log("Game data saved!");
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            SaveData data = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(path));
            if (data == null)
            {
                return;
            }
            gameManager.AddCoins(data.coins);
            foreach (Timer.Score score in data.scores)
            {
                gameManager.AddScore(score);
            }
        }
    }
}
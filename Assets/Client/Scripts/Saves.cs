using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class Saves : MonoBehaviour
{
    private static String path;
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
        public int coins;
        public List<Timer.Score> scores;
    }

    public void Save()
    {
        SaveData data = new SaveData()
        {
            coins = gameManager.coins.GetCoins(),
            scores = gameManager.scores
        };

        File.WriteAllText(path,
            CryptoEngine.Encrypt(
                JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }),
                CryptoEngine.key));

        /*File.WriteAllText(path,
            JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            })
        );*/
        Debug.Log("Game data saved!");
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            SaveData data = JsonConvert.DeserializeObject<SaveData>(
                CryptoEngine.Decrypt(File.ReadAllText(path), CryptoEngine.key));
            //SaveData data = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(path));
            if (data == null)
            {
                return;
            }

            gameManager.coins = new Coin(data.coins);
            gameManager.scores = data.scores;
        }
        else
        {
            gameManager.coins = new Coin(0);
            gameManager.scores = new List<Timer.Score>();
        }
    }

    private class CryptoEngine
    {
        public static string key = "ligh-t1so-uls202"; //128 or 192 bit

        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
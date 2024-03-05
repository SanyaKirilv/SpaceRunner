using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    private string DataFileName => $"data_{SystemInfo.deviceModel}.json";
    private string DataFilePath => Path.Combine(Application.persistentDataPath, DataFileName);

    public void Start()
    {
        //Save();
        LoadFromFile();
    }


    private void LoadFromFile()
    {
        if(File.Exists(DataFilePath))
        {
            var dataFile = File.ReadAllText(DataFilePath);
            gameData = JsonUtility.FromJson<GameData>(dataFile);
            print($"Data loaded from {DataFilePath}");
        }
    }

    private void SaveToFile()
    {
        var dataFile = JsonUtility.ToJson(gameData);
        File.WriteAllText(DataFilePath, dataFile);
        print($"Data saved in {DataFilePath}");
    }




    private void OnApplicationQuit() 
    {
        SaveToFile();
    }

    private void OnApplicationPaused() 
    {
        SaveToFile();
    }
    
    public void SaveToGameData()
    {
        //gameData.currentHealth;
    }
}

using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    private string DataFileName => $"data_{SystemInfo.deviceModel}.json";
    private string DataFilePath => Path.Combine(Application.persistentDataPath, DataFileName);
    public GameData GameData => gameData;

    public void LoadFromFile()
    {
        if(CheckForExists)
        {
            var dataFile = File.ReadAllText(DataFilePath);
            gameData = JsonUtility.FromJson<GameData>(dataFile);
            print($"Data loaded from {DataFilePath}");
        }
    }

    public void SaveToFile()
    {
        var dataFile = JsonUtility.ToJson(gameData);
        File.WriteAllText(DataFilePath, dataFile);
        print($"Data saved in {DataFilePath}");
    }

    public bool CheckForExists => File.Exists(DataFilePath);

    private void OnApplicationQuit() 
    {
        SaveToFile();
    }

    private void OnApplicationPaused() 
    {
        SaveToFile();
    }
}

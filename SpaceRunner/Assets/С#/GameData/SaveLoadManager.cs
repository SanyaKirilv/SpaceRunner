using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour 
{
    public GameData GameData { get; set; }
    private string DataFileName => $"_data.json";
    private string DataFilePath => Path.Combine(Application.persistentDataPath, DataFileName);

    public void LoadFile() => GameData = CheckForExists ? JsonUtility.FromJson<GameData>(File.ReadAllText(DataFilePath)) : null;

    public void SaveFile() => File.WriteAllText(DataFilePath, JsonUtility.ToJson(GameData));
    public bool CheckForExists => File.Exists(DataFilePath);

    private void OnApplicationQuit() => SaveFile();

    private void OnApplicationPaused() => SaveFile();
}

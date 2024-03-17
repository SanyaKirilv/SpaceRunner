using System;
using System.IO;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
    [NonSerialized] public GameData GameData;
    [SerializeField] TextAsset InitialData;
    private string DataFilePath => Path.Combine(Application.persistentDataPath, DataFileName);
    private string DataFileName => $"_data.json";

    private void Awake() => LoadData();

    public void SaveData() => File.WriteAllText(DataFilePath, JsonUtility.ToJson(GameData));

    public void LoadData() => GameData = CheckForExists ? JsonUtility.FromJson<GameData>(File.ReadAllText(DataFilePath))
        : JsonUtility.FromJson<GameData>(InitialData.text);

    private bool CheckForExists => File.Exists(DataFilePath);
}

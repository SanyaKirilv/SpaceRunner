using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private Text _starsCount;

    private GameData GameData => GetComponent<GameDataController>().GameData;
    private int StarsCount => GameData.StarsCount;

    private void Update() => _starsCount.text = $"{string.Format("{0:d9}", StarsCount)}S";

    public void Play() => SceneManager.LoadScene("Game");

    public void Exit() 
    {
        GetComponent<GameDataController>().SaveData();
        Application.Quit();
    }

    private void OnApplicationQuit() => GetComponent<GameDataController>().SaveData();

    private void OnApplicationPaused() => GetComponent<GameDataController>().SaveData();
}

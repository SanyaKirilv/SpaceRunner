using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [Header("Record")]
    [SerializeField] private int record;
    [SerializeField] private Text recordText;
    [Header("Current Score")]
    [SerializeField] private int currentScore;
    [SerializeField] private Text currentScoreText;
    [Header("Skins")]
    [SerializeField] private List<GameObject> skins;
    [Header("Abilityes")]
    [SerializeField] private List<BonusObject> bonuses;
    [Header("Pipes")]
    [SerializeField] private List<GameObject> sectionPrefabs;
    [SerializeField] private List<GameObject> spawnedSections;
    [SerializeField] private GameObject sectionParent;
    [Header("UI")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Text menuMessageText;
    [SerializeField] private GameObject resumeButton;
    private bool isPaused;
    private bool isLosed;

    private SaveLoadManager SaveLoadManager => GetComponent<SaveLoadManager>();
    private GameData GameData => SaveLoadManager.GameData;
    private MoveController MoveController => GetComponent<MoveController>();

    public void UseBonus(string name)
    {
        switch (name)
        {
            case "Shield":
                UseBonus(0);
                break;
            case "Magnet":
                UseBonus(1);
                break;
            case "Multiply":
                UseBonus(2);
                break;
        }
    }

    public void PlayPauseGame(string message)
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        menuPanel.SetActive(isPaused);
        menuMessageText.text = message;
        resumeButton.SetActive(!isLosed);
    }

    public void ExitGame()
    {
        GameData.Record = record;
        GameData.Stars += isLosed ? currentScore / 20 : currentScore / 10;
        StartCoroutine(SaveAndLoadScene("MainMenu"));
    }

    public void RestartGame() => StartCoroutine(SaveAndLoadScene("Game"));

    private void OnEnable() => Obstacle.onTouched += ActionHandler;

    private void OnDisable() => Obstacle.onTouched -= ActionHandler;

    private void Awake() => SaveLoadManager.LoadFile();

    private void Start()
    {
        record = GameData.Record;
        Time.timeScale = isPaused ? 0 : 1;
        FirstSpawn();
        SelectSkin();
        PrepareBonus();
        UpdateScore();
    }

    private void FirstSpawn()
    {
        for(int i = 3; i < 15; i++)
        {
            spawnedSections.Add(Instantiate(sectionPrefabs[Random.Range(0, sectionPrefabs.Count - 1)], new Vector3(0, 0, 50*i),
                MoveController.pipeParent.rotation, sectionParent.transform));
        }
    }

    private void ActionHandler(ObstacleType type)
    {
        if (type == ObstacleType.Star)
        {
            currentScore += bonuses[2].isUsing ? 20 : 10;
            UpdateScore();
        }
        if (type == ObstacleType.Obstacle && !bonuses[0].isUsing)
            Lose();
        if (type == ObstacleType.Spawner)
            SpawnNext();
    }

    private void PrepareBonus()
    {
        for (int i = 0; i < bonuses.Count; i++)
            SwitchBonus(i, false);
    }

    private void UseBonus(int index)
    {
        if (GameData.Bonuses[index].Count > 0 && !bonuses[index].isUsing)
        {
            GameData.Bonuses[index].Count -= 1;
            bonuses[index].text.text = $"{GameData.Bonuses[index].Count}x";
            SwitchBonus(index, true);
            StartCoroutine(Timer(index, GameData.Bonuses[index].Duration));
        }
    }

    private IEnumerator Timer(int index, int duration)
    {
        yield return new WaitForSeconds(duration);
        SwitchBonus(index, false);
    }

    private void SwitchBonus(int index, bool state)
    {
        bonuses[index].isUsing = state;
        bonuses[index].bonus.SetActive(state);
        bonuses[index].text.text = $"{GameData.Bonuses[index].Count}x";
        bonuses[index].button.interactable = GameData.Bonuses[index].Count > 0;
    }

    private void UpdateScore()
    {
        record = currentScore > record ? currentScore : record;
        recordText.text = string.Format("{0:d9}", record);
        currentScoreText.text = string.Format("{0:d9}", currentScore);
    }

    private void Lose()
    {
        isLosed = true;
        PlayPauseGame("You lose");
    }

    private void SpawnNext()
    {
        spawnedSections.Add(Instantiate(sectionPrefabs[Random.Range(0, sectionPrefabs.Count - 1)], new Vector3(0, 0, 750),
            MoveController.pipeParent.rotation, sectionParent.transform));
        Destroy(spawnedSections[0], 1f);
        spawnedSections.RemoveAt(0);
    }

    private void SelectSkin()
    {
        for (int i = 0; i < skins.Count; i++)
            if (GameData.Skins[i].IsEquipped)
                skins[i].SetActive(true);
    }

    private IEnumerator SaveAndLoadScene(string sceneName)
    {
        SaveLoadManager.SaveFile();
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(.25f);
    }
}

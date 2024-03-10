using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [Header("Max Score")]
    public int maxScore;
    public Text maxScoreText;
    [Header("Current Score")]
    public int currentScore;
    public Text currentScoreText;
    [Header("Skins")]
    [SerializeField] private List<GameObject> skins;
    [Header("Abilityes")]
    [SerializeField] private List<BonusObject> abilityes;
    [Header("Pipes")]
    [SerializeField] private List<GameObject> pipes;
    [SerializeField] private List<GameObject> spawnedPipes;
    [SerializeField] private GameObject pipeParent;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Text menuText;
    public bool isPaused;
    public bool isLosed;

    private SaveLoadManager SaveLoadManager => GetComponent<SaveLoadManager>();
    private MoveController MoveController => GetComponent<MoveController>();

    private void OnEnable() => Obstacle.onTouched += ActionHandler;
    private void OnDisable() => Obstacle.onTouched -= ActionHandler;

    private void Start()
    {
        PlayPauseGame("Start");
        SaveLoadManager.LoadFromFile();
        maxScore = SaveLoadManager.GameData.maxScore;
        SelectSkin();
        PrepareAbilityes();
        UpdateScore();
    }

    private void ActionHandler(ObstacleType type)
    {
        if(type == ObstacleType.Star)
        {
            currentScore += abilityes[2].isUsed ? 20 : 10;
            UpdateScore();
        }
        if(type == ObstacleType.Obstacle && !abilityes[0].isUsed)
            Lose();
        if(type == ObstacleType.Spawner)
            SpawnNext();
    }

    public void UseAbility(string name)
    {
        switch(name)
        {
            case "Shield":
                UseAbility(0);
                break;
            case "Magnet":
                UseAbility(1);
                break;
            case "Multiplyer":
                UseAbility(2);
                break;
        }
    }

    private void PrepareAbilityes()
    {
        for (int i = 0; i < abilityes.Count; i++)
        {
            abilityes[i].text.text = $"{SaveLoadManager.GameData.abilityData[i].count.ToString()}x";
            abilityes[i].button.interactable = SaveLoadManager.GameData.abilityData[i].count > 0;
        }
    }

    private void UseAbility(int index)
    {
        if(SaveLoadManager.GameData.abilityData[index].count > 0 && !abilityes[index].isUsed)
        {
            SaveLoadManager.GameData.abilityData[index].count -= 1;
            abilityes[index].isUsed = true;
            abilityes[index].bonus.SetActive(true);
            abilityes[index].button.interactable = false;
            abilityes[index].text.text = $"{SaveLoadManager.GameData.abilityData[index].count.ToString()}x";
            StartCoroutine(Timer(index, SaveLoadManager.GameData.abilityData[index].duration));
        }
    }

    public IEnumerator Timer(int index, int duration)
    {
        yield return new WaitForSeconds(duration);
        abilityes[index].isUsed = false;
        abilityes[index].bonus.SetActive(false);
        abilityes[index].button.interactable = SaveLoadManager.GameData.abilityData[index].count > 0;
    }

    private void UpdateScore()
    {
        maxScore = currentScore > maxScore ? currentScore : maxScore;
        maxScoreText.text = String.Format("{0:d9}", maxScore);
        currentScoreText.text = String.Format("{0:d9}", currentScore);
    }

    private void Lose()
    {
        isLosed = true;
        PlayPauseGame("You lose");
    }

    private void SpawnNext()
    {
        spawnedPipes.Add(Instantiate(pipes[UnityEngine.Random.Range(0, pipes.Count)], 
            new Vector3(0, 0, 20), MoveController.pipes.rotation, pipeParent.transform));
        Destroy(spawnedPipes[0], 1f);
        spawnedPipes.RemoveAt(0);
    }

    private void SelectSkin() => skins[SaveLoadManager.GameData.currentSkin].SetActive(true);

    public void PlayPauseGame(string message)
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        menuPanel.SetActive(isPaused);
        menuText.text = message;
    }

    public void ExitGame()
    {
        SaveLoadManager.GameData.maxScore = maxScore;
        SaveLoadManager.GameData.starsCount += isLosed ? currentScore / 20 : currentScore / 10;
        SaveLoadManager.SaveToFile();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        SaveLoadManager.SaveToFile();
        SceneManager.LoadScene("Game");
    }
}

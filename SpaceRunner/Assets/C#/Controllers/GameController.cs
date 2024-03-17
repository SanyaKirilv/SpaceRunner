using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("UI score objects")]
    [SerializeField] private Text _maxScoreText;
    [SerializeField] private Text _currentScoreText;
    [Header("UI menu objects")]
    [SerializeField] private UIMoveController _menuPanel;
    [SerializeField] private Text _menuText;
    [SerializeField] private Button _resumeButton;
    [Header("UI bonus objects")]
    [SerializeField] private List<BonusObject> _bonuses;
    [Header("Models")]
    [SerializeField] private List<GameObject> _models;
    [Header("Pipes")]
    [SerializeField] private GameObject _sectionParent;
    [SerializeField] private List<GameObject> _sectionPrefabs;
    [SerializeField] private List<GameObject> _spawnedSections;

    private int _maxScore;
    private int _currentScore;
    private bool _isLosed;

    private GameDataController GameDataController => GetComponent<GameDataController>();
    private GameData GameData => GetComponent<GameDataController>().GameData;
    private List<BonusData> Bonuses => GameData.Bonuses;
    private int MaxScore { get => GameData.MaxScore; set => GameData.MaxScore = _maxScore; }
    private MoveController MoveController => GetComponent<MoveController>();

    private void Awake() => GameDataController.LoadData();

    private void Start()
    {
        _maxScore = MaxScore;
        Time.timeScale = 1;
        PreSpawn();
        SelectSkin();
        PrepareBonus();
        UpdateScore();
    }

    private void OnEnable() => Obstacle.onTouched += ActionHandler;

    private void OnDisable() => Obstacle.onTouched -= ActionHandler;

    public void UseBonus(int id)
    {
        if (Bonuses[id].Count > 0 && !_bonuses[id].IsUsing)
        {
            Bonuses[id].Count -= 1;
            SwitchBonus(id, true);
            StartCoroutine(BonusTimer(id, Bonuses[id].Duration));
        }
    }

    public void PauseGame(string message)
    {
        StartCoroutine(MenuTimer(.25f));
        _menuPanel.Switch(true);
        _menuText.text = message;
        _resumeButton.interactable = !_isLosed;
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        _menuPanel.Switch(false);
    }

    public void ExitGame()
    {
        MaxScore = _maxScore;
        GameData.StarsCount += _isLosed ? _currentScore / 20 : _currentScore / 10;
        StartCoroutine(SaveAndLoadScene("Menu"));
    }

    public void RestartGame() => StartCoroutine(SaveAndLoadScene("Game"));

    private void PreSpawn()
    {
        for (int i = 3; i < 15; i++)
            _spawnedSections.Add(Instantiate(_sectionPrefabs[Random.Range(0, _sectionPrefabs.Count - 1)], new Vector3(0, 0, 50 * i),
                MoveController.pipeParent.rotation, _sectionParent.transform));
    }

    private void ActionHandler(ObstacleType type)
    {
        if (type == ObstacleType.Star)
        {
            _currentScore += _bonuses[2].IsUsing ? 20 : 10;
            UpdateScore();
        }
        if (type == ObstacleType.Obstacle && !_bonuses[0].IsUsing)
            Lose();
        if (type == ObstacleType.Spawner)
            SpawnNext();
    }

    private void PrepareBonus()
    {
        for (int i = 0; i < _bonuses.Count; i++)
            SwitchBonus(i, false);
    }

    private void UpdateScore()
    {
        _maxScore = _currentScore > _maxScore ? _currentScore : _maxScore;
        _maxScoreText.text = string.Format("{0:d9}", _maxScore);
        _currentScoreText.text = string.Format("{0:d9}", _currentScore);
    }

    private void Lose()
    {
        _isLosed = true;
        PauseGame("You lose!");
    }

    private void SpawnNext()
    {
        _spawnedSections.Add(Instantiate(_sectionPrefabs[Random.Range(0, _sectionPrefabs.Count - 1)], new Vector3(0, 0, 700),
            MoveController.pipeParent.rotation, _sectionParent.transform));
        Destroy(_spawnedSections[0], 1f);
        _spawnedSections.RemoveAt(0);
    }

    private void SelectSkin()
    {
        for (int i = 0; i < _models.Count; i++)
            _models[i].SetActive(GameData.Models[i].IsSelected);
    }

    private IEnumerator SaveAndLoadScene(string sceneName)
    {
        GameDataController.SaveData();
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        yield return new WaitForSeconds(.25f);
    }

    private void SwitchBonus(int id, bool state)
    {
        _bonuses[id].IsUsing = state;
        _bonuses[id].Bonus.SetActive(state);
        _bonuses[id].Count.text = $"{Bonuses[id].Count}x";
        _bonuses[id].Button.interactable = Bonuses[id].Count > 0;
    }

    private IEnumerator BonusTimer(int id, int duration)
    {
        while(duration > 0)
        {
            _bonuses[id].Count.text = $"{duration}s";
            yield return new WaitForSeconds(1f);
            duration--;
        }
        SwitchBonus(id, false);
    }

    private IEnumerator MenuTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = 0;
    }
}

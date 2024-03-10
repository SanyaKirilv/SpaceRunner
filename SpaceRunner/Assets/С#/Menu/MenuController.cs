using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    private SaveLoadManager SaveLoadManager => GetComponent<SaveLoadManager>();

    public void Play() => SceneManager.LoadScene("Game");

    private void Awake() => SaveLoadManager.LoadFromFile();
}

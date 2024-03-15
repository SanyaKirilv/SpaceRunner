using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public GameData initialGameData;
    public bool ForceSave;
    private SaveLoadManager SaveLoadManager => GetComponent<SaveLoadManager>();

    private void Start()
    {
        if (!SaveLoadManager.CheckForExists || ForceSave)
        {
            SaveLoadManager.GameData = initialGameData;
            SaveLoadManager.SaveFile();
        }
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}

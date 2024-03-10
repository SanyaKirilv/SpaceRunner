using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public bool ForceSave;
    private SaveLoadManager SaveLoadManager => GetComponent<SaveLoadManager>();

    void Start()
    {
        if(!SaveLoadManager.CheckForExists || ForceSave) SaveLoadManager.SaveToFile();
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopController : MonoBehaviour
{
    [SerializeField] private List<SkinObject> skinObjects;
    [SerializeField] private Text starsCountText;
    public int index;
    private SaveLoadManager SaveLoadManager => GetComponent<SaveLoadManager>();

    private void Start() => UpdateScreen();

    public void BuySkin(int index)
    {
        SaveLoadManager.GameData.starsCount -= SaveLoadManager.GameData.skinData[index].cost;
        SaveLoadManager.GameData.skinData[index].isBuyed = true;
        UpdateScreen();
    }

    public void SelectSkin(int index)
    {
        SaveLoadManager.GameData.currentSkin = index;
        UpdateScreen();
    }

    private void UpdateScreen()
    {
        starsCountText.text = $"{String.Format("{0:d9}", SaveLoadManager.GameData.starsCount)}S";
        for(int i = 0; i < skinObjects.Count; i++)
        {
            skinObjects[i]._name.text = $"{SaveLoadManager.GameData.skinData[i].name}";
            skinObjects[i].description.text = $"{SaveLoadManager.GameData.skinData[i].description}";
            skinObjects[i].cost.text = $"Buy\n-{SaveLoadManager.GameData.skinData[i].cost}S";
            skinObjects[i].buy.interactable = SaveLoadManager.GameData.starsCount - SaveLoadManager.GameData.skinData[i].cost > 0 && !SaveLoadManager.GameData.skinData[i].isBuyed;
            skinObjects[i].select.interactable = SaveLoadManager.GameData.skinData[i].isBuyed && i != SaveLoadManager.GameData.currentSkin;
        }
    }

    public void NextSkin(int index)
    {
        this.index = this.index + index > skinObjects.Count - 1 ? 0 : this.index + index < 0 ? skinObjects.Count - 1 : this.index + index;
        for(int i = 0; i < skinObjects.Count; i++)
        {
            skinObjects[i].gameObject.SetActive(false);
            if(i == this.index)
                skinObjects[i].gameObject.SetActive(true);
        }
    }
}

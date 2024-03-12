using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopController : MonoBehaviour {
    [SerializeField] private List<SkinObjectUI> skins;
    [SerializeField] private Text starsCountText;
    private int index;
    private SaveLoadManager SaveLoadManager => GetComponent<SaveLoadManager>();
    private GameData GameData => SaveLoadManager.GameData;

    public void BuySkin(int index) 
    {
        GameData.Stars -= GameData.Skins[index].Cost;
        GameData.Skins[index].IsBuyed = true;
        UpdateScreen();
    }

    public void SelectSkin(int index) 
    {
        GameData.Skins[index].IsEquipped = true;
        UpdateScreen();
    }

    private void Start() => UpdateScreen();

    private void UpdateScreen() {
        starsCountText.text = $"{String.Format("{0:d9}", GameData.Stars)}S";
        for(int i = 0; i < skins.Count; i++) {
            skins[i].Cost.text = $"Buy\n-{GameData.Skins[i].Cost}S";
            skins[i].BuyButton.interactable = GameData.Stars - GameData.Skins[i].Cost > 0 && !GameData.Skins[i].IsBuyed;
            skins[i].SelectButton.interactable = GameData.Skins[i].IsBuyed && !GameData.Skins[i].IsEquipped;
        }
    }

    public void NextSkin(int index) {
        this.index = this.index + index > skins.Count - 1 ? 
            0 : this.index + index < 0 ? skins.Count - 1 : this.index + index;
        foreach (var skinObject in skins)
            skinObject.gameObject.SetActive(false);
        skins[this.index].gameObject.SetActive(true);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusShopController : MonoBehaviour 
{
    [SerializeField] private List<BonusObjectUI> bonuses;
    [SerializeField] private Text starsCountText;
    private SaveLoadManager SaveLoadManager => GetComponent<SaveLoadManager>();
    private GameData GameData => SaveLoadManager.GameData;

    public void BuyBonus(int index) 
    {
        GameData.Stars -= GameData.Bonuses[index].Cost;
        GameData.Bonuses[index].Count++;
        UpdateScreen();
    }

    private void Start() => UpdateScreen();

    private void UpdateScreen() 
    {
        starsCountText.text = $"{String.Format("{0:d9}", GameData.Stars)}S";
        for(int i = 0; i < bonuses.Count; i++) 
        {
            bonuses[i].Description.text += $"\nDuration {GameData.Bonuses[i].Duration}s";
            bonuses[i].Count.text = $"Count {GameData.Bonuses[i].Count}x";
            bonuses[i].Cost.text = $"Buy\n-{GameData.Bonuses[i].Cost}S";
            bonuses[i].BuyButton.interactable = GameData.Stars - GameData.Bonuses[i].Cost > 0;
        }
    }
}

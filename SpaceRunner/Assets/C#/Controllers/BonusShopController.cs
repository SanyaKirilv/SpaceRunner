using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusShopController : MonoBehaviour
{
    [Header("Bonus view UI Objects")]
    [SerializeField] private List<BonusView> _bonusViews;

    private GameData GameData => GetComponent<GameDataController>().GameData;
    private int StarsCount { get => GameData.StarsCount; set => GameData.StarsCount = value; }
    private List<BonusData> BonusData => GameData.Bonuses;

    private int Duration(int index) => BonusData[index].Duration;

    private int Cost(int index) => BonusData[index].Cost;

    private void Start() => UpdateScreen();

    public void BuyBonus(int index)
    {
        StarsCount -= Cost(index);
        BonusData[index].Count++;
        UpdateScreen();
    }

    private void UpdateScreen()
    {
        for (int i = 0; i < _bonusViews.Count; i++)
        {
            _bonusViews[i].BuyButton.interactable = StarsCount > Cost(i);
            _bonusViews[i].BuyButton.gameObject.GetComponentInChildren<Text>().text = $"Buy\n-{Cost(i)}S";
            _bonusViews[i].Duration.text = $"Duration: {Duration(i)}s";
            _bonusViews[i].Count.text = $"{BonusData[i].Count}x";
        }
    }
}

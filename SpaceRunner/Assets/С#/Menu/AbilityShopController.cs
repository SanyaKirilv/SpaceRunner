using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityShopController : MonoBehaviour
{
    [SerializeField] private List<AbilityObject> abilityObjects;
    [SerializeField] private Text starsCountText;
    private SaveLoadManager SaveLoadManager => GetComponent<SaveLoadManager>();

    private void Start() => UpdateScreen();

    public void BuyAbility(int index)
    {
        SaveLoadManager.GameData.starsCount -= SaveLoadManager.GameData.abilityData[index].cost;
        SaveLoadManager.GameData.abilityData[index].count++;
        UpdateScreen();
    }

    private void UpdateScreen()
    {
        starsCountText.text = $"{String.Format("{0:d9}", SaveLoadManager.GameData.starsCount)}S";
        for(int i = 0; i < abilityObjects.Count; i++)
        {
            abilityObjects[i]._name.text = $"{SaveLoadManager.GameData.abilityData[i].name}";
            abilityObjects[i].description.text = $"{SaveLoadManager.GameData.abilityData[i].description}\nDuration {SaveLoadManager.GameData.abilityData[i].duration}x";
            abilityObjects[i].count.text = $"Count {SaveLoadManager.GameData.abilityData[i].count}x";
            abilityObjects[i].cost.text = $"Buy\n-{SaveLoadManager.GameData.abilityData[i].cost}S";
            abilityObjects[i].button.interactable = SaveLoadManager.GameData.starsCount - SaveLoadManager.GameData.abilityData[i].cost > 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

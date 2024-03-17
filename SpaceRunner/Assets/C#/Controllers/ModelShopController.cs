using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelShopController : MonoBehaviour
{
    [Header("Model view UI Objects")]
    [SerializeField] private Text _modelName;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _selectButton;
    [Header("Models icon and indicator")]
    [SerializeField] private List<GameObject> _modelViews;
    private int _currentModelIndex = 0;

    private GameData GameData => GetComponent<GameDataController>().GameData;
    private int StarsCount { get => GameData.StarsCount; set => GameData.StarsCount = value; }
    private List<ModelData> ModelData => GameData.Models;
    private bool IsBuyed { get => ModelData[_currentModelIndex].IsBuyed; set => ModelData[_currentModelIndex].IsBuyed = value; }
    private bool IsSelected { get => ModelData[_currentModelIndex].IsSelected; set => ModelData[_currentModelIndex].IsSelected = value; }
    private int Cost => ModelData[_currentModelIndex].Cost;

    private void Start() => UpdateScreen();

    public void SwitchModel(int num)
    {
        _currentModelIndex = _currentModelIndex + num > _modelViews.Count - 1 ? 0 :
            _currentModelIndex + num < 0 ? _modelViews.Count - 1 : _currentModelIndex += num;
        UpdateScreen();
    }

    public void SelectModel()
    {
        foreach (var model in ModelData)
            model.IsSelected = false;
        IsSelected = true;
    }

    public void BuyModel()
    {
        IsBuyed = true;
        StarsCount -= Cost;
    }

    private void UpdateScreen()
    {
        _modelName.text = ModelData[_currentModelIndex].Name;
        ButtonView(_buyButton, !IsBuyed && GameData.StarsCount - Cost > 0, IsBuyed ? "Buyed" : $"Buy -{Cost}S");
        ButtonView(_selectButton, IsBuyed && !IsSelected, IsBuyed ? !IsSelected ? "Select" : "Selected" : "Select");
        SwitchModelsView();
    }

    private void ButtonView(Button button, bool state, string text)
    {
        button.interactable = state;
        button.gameObject.GetComponentInChildren<Text>().text = text;
    }

    private void SwitchModelsView()
    {
        foreach (var modelView in _modelViews)
            modelView.SetActive(false);
        _modelViews[_currentModelIndex].SetActive(true);
    }
}

using System.Collections.Generic;

[System.Serializable]
public class SkinData
{
    public string name;
    public int cost;
    public bool isBuyed;
}

[System.Serializable]
public class GameData
{
    public int currentHealth;
    public int maxScore;
    public int coinsCount;
    public int currentSkin;
    public List<SkinData> skinData;
    public float exitGameTime;
}

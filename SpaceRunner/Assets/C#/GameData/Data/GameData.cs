using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int MaxScore;
    public int StarsCount;
    public List<BonusData> Bonuses;
    public List<ModelData> Models;
}
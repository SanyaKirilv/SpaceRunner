using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int maxScore;
    public int starsCount;
    public int currentSkin;
    public List<SkinData> skinData;
    public List<AbilityData> abilityData;
    public float exitGameTime;
}

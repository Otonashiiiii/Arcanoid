using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class Targets
{
    public int line;
    public int column;
    public string targetType;
    public string giftType;
}

[System.Serializable]
public class LevelData
{

    public int levelNumber;
    public float ballSpeed;
    public Targets[] targets;
        
}

[System.Serializable]
public class Levels
{
    public LevelData[] level;
}

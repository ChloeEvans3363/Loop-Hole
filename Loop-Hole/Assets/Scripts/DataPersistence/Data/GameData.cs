using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int highScore;
    public int totalGems;

    public GameData()
    {
        this.highScore = 0;
        this.totalGems = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int availableLevel;
    public int highscore;

    public PlayerData(GameManager manager)
    {
        availableLevel = manager.availableLevel;
        highscore = (int)manager.highScore;
    }

}

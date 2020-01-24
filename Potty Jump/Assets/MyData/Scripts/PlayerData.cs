using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int availableLevel;
    public int highscore;
    public bool firstTime;

    public PlayerData(GameManager manager)
    {
        availableLevel = manager.availableLevel;
        highscore = (int)manager.highScore;
        firstTime = manager.firstTime;
    }

}

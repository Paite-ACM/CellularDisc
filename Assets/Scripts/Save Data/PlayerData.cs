using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float score;

    // add powerup stuff (and whatever else we're adding) later

    public PlayerData(GameManager gm)
    {
        score = gm.score;
    }
}

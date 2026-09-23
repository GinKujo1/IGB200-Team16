using UnityEngine;
using System;

[Serializable]
public class ScoreBoardEntry
{
    public string playerName;
    public float score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public ScoreBoardEntry (string name, float scoreValue)
    {
        playerName = name;
        score = scoreValue;
    }
}

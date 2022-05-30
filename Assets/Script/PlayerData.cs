using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int currency;
    private int gamesPlayed;
    private int maxScore;

    public PlayerData()
    {
        Currency = GameplayManager.Money;
        GamesPlayed = GameplayManager.GamesPlayed;
        MaxScore = GameplayManager.MaxScore;
    }

    public int Currency { get => currency; set => currency = value; }
    public int GamesPlayed { get => gamesPlayed; set => gamesPlayed = value; }
    public int MaxScore { get => maxScore; set => maxScore = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int currency;
    private int gamesPlayed;
    private int maxScore;
    public bool acquired1 = false;
    public bool acquired2 = false;
    public bool acquired3 = false;
    public bool acquired4 = false;
    public int selectedSkin;
    public PlayerData()
    {
        Currency = GameplayManager.Money;
        GamesPlayed = GameplayManager.GamesPlayed;
        MaxScore = GameplayManager.MaxScore;
        acquired1 = Market.Acquired[0];
        acquired2 = Market.Acquired[1];
        acquired3 = Market.Acquired[2];
        acquired4 = Market.Acquired[3];
        selectedSkin = Market.SelectedSkin;
    }

    public int Currency { get => currency; set => currency = value; }
    public int GamesPlayed { get => gamesPlayed; set => gamesPlayed = value; }
    public int MaxScore { get => maxScore; set => maxScore = value; }
}

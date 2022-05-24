using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int currency;


    public PlayerData()
    {
        Currency = GameplayManager.Money;
    }

    public int Currency { get => currency; set => currency = value; }
}

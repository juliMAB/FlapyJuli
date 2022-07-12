using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private static int money = 0;
    [SerializeField] private static int gamesPlayed = 0;
    [SerializeField] private static int maxScore = 0;

    [SerializeField] private FlapyBird fb = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private SpikesManager spikesManager = null;
    [SerializeField] private Coint coint = null;
    [SerializeField] private SpawnCurrency spawnCurrency = null;
    [SerializeField] private ProgresionManager progresion = null;
    [SerializeField] private Market market = null;

    [SerializeField] private System.Action OnTouchWall;
    [SerializeField] private System.Action OnBuy;
    [SerializeField] private System.Action OnTouchCoint;
    [SerializeField] private System.Action OnpjDie;
    [SerializeField] private System.Action OnProgress;
    [SerializeField] private System.Action<int> OnScoreChange;
    [SerializeField] private System.Action<int> OnMoneyChange;
    [SerializeField] private System.Action<int> OnMaxScoreChange;
    [SerializeField] private System.Action<int> OnGamePlayedChange;
    private Vector3 pjPos;

    public static int Money { get => money; set => money = value; }
    public static int GamesPlayed { get => gamesPlayed; set => gamesPlayed = value; }
    public static int MaxScore { get => maxScore; set => maxScore = value; }

    private void Start()
    {
        spikesManager.Init(ref OnTouchWall, ref OnProgress);
        progresion.Init(ref OnScoreChange, ref OnProgress);
        OnTouchCoint += Added1Money;
        coint.Init(ref OnTouchCoint);
        OnTouchWall += Added1Score;
        OnpjDie += Added1Game;
        OnpjDie += UpdateMaxScore;
        OnpjDie += DisableBird;
        uiManager.Init(ref OnScoreChange,ref OnpjDie,ref OnMoneyChange, ref OnMaxScoreChange,ref OnGamePlayedChange);
        fb.Init(ref OnTouchWall,ref OnpjDie, UpdatePjPos);
        OnBuy += MarketBuy;
        LoadCurrency();
        market.Init(ref OnBuy);
    }
    void MarketBuy()
    {
        OnMoneyChange?.Invoke(money);
        SaveCurrency();
    }

    void UpdatePjPos(Vector3 pos)
    {
        pjPos = pos;
    }
    void UpdateMaxScore()
    {
        if (score>maxScore)
        {
            maxScore = score;
            OnMaxScoreChange?.Invoke(maxScore);
        }
    }
    void Added1Game()
    {
        gamesPlayed++;
        OnGamePlayedChange?.Invoke(gamesPlayed);
        SaveCurrency();
    }
    void Added1Score()
    {
        score++;
        OnScoreChange?.Invoke(score);
        if (score%5 == 0)
            spawnCurrency.SpawnCoint(pjPos);
    }

    private void ResetScore()
    {
        score = 0;
    }
    void Added1Money()
    {
        print("pj touch coint");
        money++;
        OnMoneyChange?.Invoke(money);
        SaveCurrency();
    }
    private void DisableBird()
    {
        fb.enabled = false;
    }
    private void SaveCurrency()
    {
        SaveSystem.SaveData();
    }

    private void LoadCurrency()
    {
        PlayerData data = SaveSystem.LoadData();
        if(data==null)
            return;
        money = data.Currency;
        gamesPlayed = data.GamesPlayed;
        maxScore = data.MaxScore;
        Market.Acquired[0] = data.acquired1 ;
        Market.Acquired[1] = data.acquired2 ;
        Market.Acquired[2] = data.acquired3 ;
        Market.Acquired[3] = data.acquired4 ;
        Market.SelectedSkin = data.selectedSkin;
        OnBuy?.Invoke();
        OnMoneyChange?.Invoke(money);
        OnMaxScoreChange?.Invoke(maxScore);
        OnGamePlayedChange?.Invoke(gamesPlayed);
    }
    public void DeleteData()
    {
        money = 0;
        gamesPlayed = 0;
        maxScore = 0;
        for (int i = 0; i < 4; i++)
        {
            Market.Acquired[i] = false;
            market.AcquiredGO[i].SetActive(true);
        }
        Market.SelectedSkin = 0;
        SaveSystem.SaveData();
    }
    public void MyReset()
    {
        fb.enabled = true;
        fb.MyReset();
        coint.MyReset();
        spikesManager.MyReset();
        ResetScore();
        uiManager.MyReset();
        progresion.MyReset();
    }
    public void Add10Coint()
    {
        money += 10;
        OnBuy?.Invoke();
        OnMoneyChange?.Invoke(money);
    }
    public void OnCallMarketButton(int v)
    {
        market.OnCallButton(v, ref money);
        OnMoneyChange?.Invoke(money);
        SaveCurrency();
    }
}

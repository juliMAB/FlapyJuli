using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private int score = 0;

    [SerializeField] private FlapyBird fb = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private SpikesManager spikesManager = null;

    [SerializeField] private System.Action OnTouchWall;
    [SerializeField] private System.Action OnpjDie;
    [SerializeField] private System.Action<int> OnScoreChange;
    private void Start()
    {
        spikesManager.Init(ref OnTouchWall);
        OnTouchWall += Added1Score;
        uiManager.Init(ref OnScoreChange,ref OnpjDie);
        fb.Init(ref OnTouchWall,ref OnpjDie);
    }

    void Added1Score()
    {
        score++;
        OnScoreChange?.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}

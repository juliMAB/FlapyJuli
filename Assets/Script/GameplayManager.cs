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
    [SerializeField] private System.Action<int> OnScoreChange;
    private void Start()
    {
        spikesManager.Init(ref OnTouchWall);
        OnTouchWall += Added1Score;
        fb.Init(OnTouchWall);
        uiManager.Init(ref OnScoreChange);
    }

    void Added1Score()
    {
        score++;
        OnScoreChange?.Invoke(score);
    }
}

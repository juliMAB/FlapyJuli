using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_TextMeshPro_Score;
    [SerializeField] private TMPro.TextMeshProUGUI m_TextMeshPro_EndScore;
    [SerializeField] private TMPro.TextMeshProUGUI m_TextMeshPro_Currency;
    [SerializeField] private TMPro.TextMeshProUGUI m_TextMeshPro_MaxScore;
    [SerializeField] private TMPro.TextMeshProUGUI m_TextMeshPro_GamesPlayed;
    [SerializeField] private UnityEvent OnEnd;
    public void Init(
        ref System.Action<int> OnScoreChange,
        ref System.Action OnPlayerDie,
        ref System.Action<int> OnCurrencyChange,
        ref System.Action<int> OnMaxScoreChange,
        ref System.Action<int> OnGamePlayedChange
        )
    {
        OnScoreChange += UpdateScore;
        OnPlayerDie += UpdateEnd;
        OnPlayerDie += UpdateScore;
        OnCurrencyChange += UpdateCurrency;
        OnMaxScoreChange += UpdateMaxScore;
        OnGamePlayedChange += UpdateGamesPlayed;
    }
    private void UpdateScore(int score)
    {
        if (m_TextMeshPro_Score != null)
            m_TextMeshPro_Score.text = score.ToString();
    }
    private void UpdateEnd()
    {
        OnEnd?.Invoke();
    }
    private void UpdateCurrency(int currency)
    {
        if (m_TextMeshPro_Currency != null)
            m_TextMeshPro_Currency.text = "Moneditas: "+ currency.ToString();
    }
    private void UpdateMaxScore(int value)
    {
        if (m_TextMeshPro_MaxScore != null)
            m_TextMeshPro_MaxScore.text = "Best Score: " + value.ToString();
    }
    private void UpdateGamesPlayed(int value)
    {
        if (m_TextMeshPro_GamesPlayed != null)
            m_TextMeshPro_GamesPlayed.text = "Games Played: " + value.ToString();
    }
    public void MyReset()
    {
        UpdateScore(0);
    }
    private void UpdateScore()
    {
        if (m_TextMeshPro_EndScore != null)
            m_TextMeshPro_EndScore.text = m_TextMeshPro_Score.text.ToString();
    }
}

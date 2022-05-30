using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_TextMeshPro_Score;
    [SerializeField] private TMPro.TextMeshProUGUI m_TextMeshPro_Currency;
    [SerializeField] private UnityEvent OnEnd;
    public void Init(ref System.Action<int> OnScoreChange,ref System.Action OnPlayerDie, ref System.Action<int> OnCurrencyChange)
    {
        OnScoreChange += UpdateScore;
        OnPlayerDie += UpdateEnd;
        OnCurrencyChange += UpdateCurrency;
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
            m_TextMeshPro_Currency.text = currency.ToString();
    }
    public void MyReset()
    {
        UpdateScore(0);
    }
}

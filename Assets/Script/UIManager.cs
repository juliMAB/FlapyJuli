using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_TextMeshPro_Score;
    [SerializeField] private GameObject m_endText;
    public void Init(ref System.Action<int> OnScoreChange,ref System.Action OnPlayerDie)
    {
        OnScoreChange += UpdateScore;
        OnPlayerDie += UpdateEndText;
    }
    private void UpdateScore(int score)
    {
        if (m_TextMeshPro_Score != null)
            m_TextMeshPro_Score.text = score.ToString();
    }
    private void UpdateEndText()
    {
        m_endText.SetActive(true);
    }
}

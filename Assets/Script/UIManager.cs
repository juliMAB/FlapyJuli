using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_TextMeshPro_Score;
    public void Init(ref System.Action<int> OnScoreChange)
    {
        OnScoreChange += UpdateScore;
    }
    private void UpdateScore(int score)
    {
        if (m_TextMeshPro_Score != null)
            m_TextMeshPro_Score.text = score.ToString();
    }
}

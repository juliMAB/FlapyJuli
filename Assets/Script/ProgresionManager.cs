using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgresionManager : MonoBehaviour
{
    [System.Serializable]
    public class ValuesProgress
    {
        [SerializeField] public int[] values;
    }
    [SerializeField] ValuesProgress valuesProgress = new ValuesProgress();
    [SerializeField] private enum TYPES {VALUE,CURVE}
    [SerializeField] TYPES tYPES = TYPES.CURVE;
    [SerializeField] BackColorChanger colorChanger = null;
    [SerializeField] AnimationCurve animationCurve = new AnimationCurve();
    [SerializeField] int actualValue = 1;
    System.Action OnChangeDificulty;

    public void Init(ref System.Action<int> OnScore, ref System.Action OnchangeDificulty)
    {
        OnScore += OnProgress;
        this.OnChangeDificulty = OnchangeDificulty;
    }
    private void OnProgress(int score)
    {

        switch (tYPES)
        {
            case TYPES.VALUE:
                UpdateBaseValue(score);
                break;
            case TYPES.CURVE:
                UpdateCurve(score);
                break;
            default:
                break;
        }
        
    }
    void UpdateBaseValue(int score)
    {
        if (valuesProgress.values.Length == 0|| actualValue >= valuesProgress.values.Length)
            return;
        if (score >= valuesProgress.values[actualValue])
        {
            actualValue++;
            colorChanger.OnProgress();
            OnChangeDificulty?.Invoke();
        }
    }
    void UpdateCurve(int score)
    {
        float a = animationCurve.Evaluate(score);
        int z = (int)a;
        if (actualValue != z)
        {
            actualValue = z;
            colorChanger.OnProgress();
            OnChangeDificulty.Invoke();
        }
    }

    public void MyReset()
    {
        actualValue = 0;
    }
}

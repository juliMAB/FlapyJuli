using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgresionManager : MonoBehaviour
{
    [SerializeField]
    class ValuesProgress
    {
        float[] values;
    }
    [SerializeField] private enum TYPES {VALUE,CURVE}
    [SerializeField] TYPES tYPES = TYPES.CURVE;
    [SerializeField] BackColorChanger colorChanger = null;
    [SerializeField] AnimationCurve animationCurve = new AnimationCurve();
    [SerializeField] float actualValue = 1;

    public void Init(ref System.Action<int> OnScore)
    {
        OnScore += OnProgress;
    }
    private void OnProgress(int score)
    {
     
        float a = animationCurve.Evaluate(score);
        int z = (int)a;
        if (actualValue != z)
        {
            actualValue = z;
            colorChanger.OnProgress();
        }
    }
}

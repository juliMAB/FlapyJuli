using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowShowButton : MonoBehaviour
{
    public Button button;
    private bool anim= false;
    public void OnCall()
    {
        if (anim == false)
            StartCoroutine(LerpColor());
    }
    IEnumerator LerpColor()
    {
        anim = true;
        float a = 0;
        Color b = button.targetGraphic.color;
        button.interactable = false;
        while (a<1)
        {
            a+=Time.deltaTime;
            button.targetGraphic.color = new Color(b.r,b.g,b.b,a*255);
            yield return null;
        }
        button.targetGraphic.color = new Color(b.r, b.g, b.b, 255);
        button.interactable = true;
        anim = false;
    }
    private void OnEnable()
    {
        OnCall();
    }
}

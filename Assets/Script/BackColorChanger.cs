using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackColorChanger : MonoBehaviour
{
    [SerializeField] private enum TYPES {RANDOM,LINEAR }
    [SerializeField] TYPES t;
    [SerializeField] private Material background;
    [SerializeField] private Material border;
    [SerializeField] private TMPro.TextMeshProUGUI numberScore;
    [SerializeField] private float durationAnim;
    [SerializeField] private Color[] colors;
    [SerializeField] private int index=0;




    void ChangeColorBack(Color color)
    {
        StartCoroutine(ChangeColor(background, color, durationAnim));
    }
    void ChangeColorSides(Color color)
    {
        StartCoroutine(ChangeColor(border, color, durationAnim));
    }

    void ChangeColorRandom()
    {
        Color var = colors[Random.Range(0, colors.Length)];
        ChangeColorBack(var);
        ChangeColorSides(var);
    }
    void CnahgeColorLinear()
    {
        Color var = colors[index];
        ChangeColorBack(var);
        ChangeColorSides(var);
    }
    public void OnProgress()
    {
        switch (t)
        {
            case TYPES.RANDOM:
                ChangeColorRandom();
                break;
            case TYPES.LINEAR:
                CnahgeColorLinear();
                index++;
                if (index>colors.Length) 
                    index = 0;
                break;
        }
    }


    IEnumerator ChangeColor(Material go, Color to, float time)
    {
        Color initColor = go.color;
        float dTime = 0;
        float percent = 0;
        while (dTime < time)
        {
            dTime += Time.deltaTime;
            percent = dTime / time;
            if (dTime >= time)
                percent = 1;
            go.color = Color.Lerp(initColor, to, percent);
            yield return null;
        } 
    }
}

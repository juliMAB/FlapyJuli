using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HacksScene : MonoBehaviour
{
    public TMP_Text example;
    public TMP_Text[] a;
    public void OnCall()
    {
        for (int i = 0; i < a.Length; i++) 
            a[i].font = example.font;
    }
    public void OnFoundObjects()
    {
        a = FindObjectsOfType<TMP_Text>();
    }
}

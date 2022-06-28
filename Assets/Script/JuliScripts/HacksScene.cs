using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HacksScene : MonoBehaviour
{
    public Image example;
    public Button[] a;
    public void OnCall()
    {
        for (int i = 0; i < a.Length; i++)
            a[i].targetGraphic.GetComponent<Image>().sprite = example.sprite;
    }
    public void OnFoundObjects()
    {
        a = FindObjectsOfType<Button>();
    }
}

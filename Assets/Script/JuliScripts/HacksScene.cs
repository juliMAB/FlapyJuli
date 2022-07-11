using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HacksScene : MonoBehaviour
{
    public string vrs;

    public GameObject secretMenu;
    public void OnCall()
    {
        vrs = Application.version.ToString();
    }

    public void OnCallMagicButton()
    {
        if (Input.GetKey(KeyCode.V))
        {
            secretMenu.SetActive(true);
        }
    }
}

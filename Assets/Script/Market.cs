using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] private static bool[] acquired = new bool[4];
    [SerializeField] Animator animatorBird;
    [SerializeField] private  GameObject[] acquiredGO =null;
    [SerializeField] private int[] value = new int[4];
    [SerializeField] private static  int selectedSkin;
    public static bool[] Acquired { get => acquired; set => acquired = value; }
    public static int SelectedSkin { get => selectedSkin; set => selectedSkin = value; }
    public GameObject[] AcquiredGO { get => acquiredGO; set => acquiredGO = value; }

    [SerializeField] private System.Action OnBuy;
    [SerializeField] private System.Action EndOnBuy;

    public void Init(ref System.Action OnBuy)
    {
        UpdateBuys();
        this.OnBuy = OnBuy;
        this.OnBuy += UpdateBuys;
        //for (int i = 0; i < 4; i++)
        //{
        //    acquired[i] = false;
        //}
        animatorBird.SetInteger("skin", SelectedSkin);
    }

    public void OnCallButton(int v,ref int money)
    {
        if (!acquired[v])
        {
            if (value[v]<=money)
            {
                money -= value[v];
                acquired[v] = true;
                OnBuy?.Invoke();
                
            }
        }
        if (acquired[v])
        {
            selectedSkin = v;
            animatorBird.SetInteger("skin",v);
        }
    }
    private void UpdateBuys()
    {
        for (int i = 0; i < acquired.Length; i++)
            acquiredGO[i].SetActive(!acquired[i]);
    }
}

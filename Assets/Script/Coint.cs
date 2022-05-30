using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coint : MonoBehaviour
{

    [SerializeField] private LayerMask playerLayer = 0;
    [SerializeField] private System.Action OnCoinTouch;

    public void Init(ref System.Action OnCointTouch)
    {
        this.OnCoinTouch = OnCointTouch;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            OnCoinTouch?.Invoke();
            transform.gameObject.SetActive(false);
        }
    }
    public void MyReset()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapyBird : MonoBehaviour
{
    [SerializeField] private float m_jump_force;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float m_speed;
    [SerializeField] private bool m_isDeath = false;

    [SerializeField] private System.Action OnTouchWall;
    [SerializeField] private System.Action OnTouchSpike;

    [SerializeField] private LayerMask wallLayer = 0;
    [SerializeField] private LayerMask spikeLayer = 0;

    private Vector3 pos;

    public void Init(ref System.Action OnTouchWall,ref System.Action OnTouchSpike)
    {
        this.OnTouchWall = OnTouchWall;
        this.OnTouchSpike = OnTouchSpike;
    }
    public void MyReset()
    {
        transform.position = pos;
        transform.rotation = Quaternion.identity;
        _rb.velocity = transform.right * m_speed;
        _rb.angularVelocity = Vector3.zero;
        m_isDeath = false;
    }
    private void OnEnable()
    {
        _rb.velocity = transform.right * m_speed;
    }
    private void Start()
    {
        pos = transform.position;
        _rb.velocity = transform.right * m_speed;
    }
    private void Update()
    {
        if (m_isDeath)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            print("OnClick");
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce( Vector3.up*m_jump_force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Vector3 save = _rb.velocity;
        if ((wallLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            _rb.velocity = new Vector3(-save.x, save.y, -save.z);
            OnTouchWall?.Invoke();
        }
        if ((spikeLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            print("spikeTouch");
            Vector3 expPos = transform.position + Vector3.forward - Vector3.up;
            if (save.x > 0)
                expPos += Vector3.right;
            else
                expPos -= Vector3.right;
            _rb.AddExplosionForce(1000, expPos,100);
            OnTouchSpike?.Invoke();
            m_isDeath = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapyBird : MonoBehaviour
{
    [SerializeField] private float m_jump_force;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float m_speed;

    private Vector3 pos;

    public void MyReset()
    {
        transform.position = pos;
        _rb.velocity = transform.right * m_speed;
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
        //_rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, 0f);
        if (Input.GetMouseButtonDown(0))
        {
            print("OnClick");
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce( Vector3.up*m_jump_force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("trigerEnter");
        Vector3 save = _rb.velocity;
        _rb.velocity = new Vector3(-save.x, save.y, -save.z);
        //_rb.velocity = transform.right * m_speed *-1;
        //_rb.velocity = _rb.velocity - transform.right * m_speed;
    }
}

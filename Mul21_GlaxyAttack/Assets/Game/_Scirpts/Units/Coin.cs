using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PoolMember
{
    private Rigidbody rb;
    private float _speed = 2f;
    private int _quantity = 20;
    private float _timeStamp;

    private Transform _magnet;
    private bool _movingToMagnet = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _movingToMagnet = false;
        _magnet = null;
        rb.velocity = Vector3.zero;

    }
    
    private void Update()
    {
        Moving();

        if (tf.position.y < -12f || (_magnet && Vector3.Distance(tf.position, _magnet.position) <= 0.1f))
        {
            AudioSystem.Instance.PlaySoundByName(SOUND_NAME.Clamp);
            OnDespawn();
        }

    }

    private void Moving()
    {
        if(_movingToMagnet)
        {
            Vector3 dir = -(tf.position - _magnet.position).normalized;
            rb.velocity = new Vector2(dir.x, dir.y) * 7f * (Time.time / _timeStamp);
        }
        else 
            tf.Translate(Vector2.down * Time.deltaTime * _speed, Space.Self);
    }
    
    private void OnDespawn()
    {
        ShipPreferences.Instance.AddCoin(this._quantity);
        PoolManager.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals("Magnet"))
        {
            _timeStamp = Time.time;
            _movingToMagnet = true;
            _magnet = other.transform;
        }
    }
}

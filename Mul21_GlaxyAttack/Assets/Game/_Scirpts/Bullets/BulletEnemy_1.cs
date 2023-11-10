using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BulletEnemy_1 : PoolMember
{
    private float _speed = -5f;
    private float _damage = 2f;

    private void Moving()
    {
        tf.Translate(tf.up * Time.deltaTime * _speed, Space.Self);
    }

    private void OnDespawn()
    {
        PoolManager.Despawn(this);
    }

    private void Update()
    {
        Moving();

        if(tf.position.y < -12f)
        {
            OnDespawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnDespawn();
            PoolManager.Spawn(PoolType.VFX_SPARK, tf.position, Quaternion.identity);
            CacheCollider.GetITakeHit(other).TakeHit(_damage);
        }
    }
}

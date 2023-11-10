using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BulletShip_1 : PoolMember
{
    private float _speed = 23f;
    private float _damage = 3f;
    public LayerMask damageLayer;
    RaycastHit hit;

    private void OnDespawn()
    {
        PoolManager.Despawn(this);        
    }

    private void Moving()
    {
        tf.Translate(tf.up * Time.deltaTime * _speed, Space.Self);
    }

    private void Update()
    {
        Moving();

        if(IsHitEnemy())
        {
            OnDespawn();
            PoolManager.Spawn(PoolType.VFX_SPARK, tf.position, Quaternion.identity);
        }

        if(tf.position.y > 12f)
        {
            OnDespawn();
        }
    }

    private bool IsHitEnemy()
    {
        if (Physics.Raycast(tf.position, tf.up, out hit, Time.deltaTime * _speed * 2.5f, damageLayer))
        {
            ITakeHit takeHit = CacheCollider.GetITakeHit(hit.collider);
            
            if (takeHit != null)
                takeHit.TakeHit(_damage);

            return true;
        }

        return false;
    }
}

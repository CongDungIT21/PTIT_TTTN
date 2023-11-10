using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolMember
{
    private float _speed = 20f;
    private float _damage = 1f;
    RaycastHit hit;
    public LayerMask enemyLayer;

    //TODO: Note set up type dau vao
    public void OnInit()
    {
        //set dau vao cho bullet
    }

    public void OnDespawn()
    {
        PoolManager.Despawn(this);
    }

    // Update is called once per frame
    void Update()
    {
        tf.Translate(tf.up * Time.deltaTime * _speed, Space.Self);

        if (IsHitEnemy() || tf.position.y > 12f)
        {
            OnDespawn();
        }
    }

    private bool IsHitEnemy()
    {
        if (Physics.Raycast(tf.position, tf.up, out hit, Time.deltaTime * _speed * 2.5f, enemyLayer))
        {
            //TODO: Spawn Effect
            //CacheCollider.GetEnemy(hit.collider).TakeDamage(1);
            CacheCollider.GetITakeHit(hit.collider).TakeHit(1);
            return true;
        }

        return false;
    }
}

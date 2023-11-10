using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : AbstractEnemy, ITakeHit
{
    public bool isImmortal { get; set ; }

    private void OnEnable()
    {
        OnInit(3);
    }

    public override void OnSetup(Wave wave)
    {
        this.wave = wave;
    }

    public void TakeHit(float damage)
    {
        if (isImmortal) return;

        this.OnHit(damage);

        if (this.IsDead())
        {
            PoolManager.Despawn(this);
            wave.Detach(this);
        }
    }

    //public void Shoot()
    //{
    //    //anim.SetBool("isShoot", true);
    //    _canShoot = true;
    //    counterTime.CounterStart(null, Fire, 0.2f);
    //    OnShoot();
    //}

    //public void Fire()
    //{
    //    for (int i = 0; i < barrel.points.Length; i++)
    //    {
    //        PoolManager.Spawn(PoolType.BULLET_ENEMY_1, barrel.points[i].position, barrel.points[i].rotation);
    //    }

    //    counterTime.CounterStart(null, Fire, barrel.rate);
    //}

    //private void Update()
    //{
    //    if (_canShoot)
    //    {
    //        counterTime.CounterExecute();
    //    }
    //}

    public override void TweenJoinWave(Vector3[] pathJoin, float t, PathType linear)
    {
        isImmortal = true;

        tf.DOPath(pathJoin, t, linear)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                isImmortal = false;
            });
    }
}

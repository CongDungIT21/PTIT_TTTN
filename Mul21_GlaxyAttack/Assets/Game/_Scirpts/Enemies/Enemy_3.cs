using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : AbstractEnemy, ITakeHit, IShootable
{
    [SerializeField]
    Barrel barrel;
    CounterTime counterTime = new CounterTime();
    public bool isImmortal { get; set ; }
    private bool _canShoot;

    private void OnEnable()
    {
        OnInit(10);
        _canShoot = false;
        isImmortal = true;
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

    public override void TweenJoinWave(Vector3[] pathJoin, float t, PathType linear)
    {
        isImmortal = true;

        tf.DOPath(pathJoin, t, linear)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                isImmortal = false;
                Shoot();
            });
    }

    public void Shoot()
    {
        //anim.SetBool("isShoot", true);
        _canShoot = true;
        counterTime.CounterStart(null, Fire, 0.2f);
    }

    public void Fire()
    {
        for (int i = 0; i < barrel.points.Length; i++)
        {
            PoolManager.Spawn(PoolType.BULLET_ENEMY_1, barrel.points[i].position, barrel.points[i].rotation);
        }

        counterTime.CounterStart(null, Fire, barrel.rate);
    }

    private void Update()
    {
        if (_canShoot)
        {
            counterTime.CounterExecute();
        }
    }

    internal void TweenMoveToTarget(Vector3 posTarget, float time)
    {
        _canShoot = false;
        tf.DOMove(posTarget, time)
          .SetEase(Ease.Linear)
          .OnComplete(() =>
          {
              _canShoot = true;
          });
    }
}

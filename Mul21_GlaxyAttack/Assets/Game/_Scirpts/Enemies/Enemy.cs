using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Enemy : AbstractEnemy, ITakeHit, IShootable
{
    [SerializeField]
    Transform skin;

    [SerializeField]
    Barrel barrel;
    CounterTime counterTime = new CounterTime();

    public int _idx { get; private set; }

    private bool _canShoot = false;
    public bool isImmortal { get ; set;}

    private void Start()
    {
        this.OnInit(10);
    }

    public override void OnSetup(Wave wave)
    {
        this.wave = wave;
        this._canShoot = false;
    }

    public void TakeHit(float damage)
    {
        if (isImmortal) return;

        this.OnHit(damage);

        if(this.IsDead())
        {
            PoolManager.Despawn(this);
            this.wave.Detach(this);
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

    public void TweenMoveToTarget(Vector3 posTarget, float t)
    {
        tf.DOMove(posTarget, t)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("Tween Complite");                
            });
    }


    public void Shoot()
    {
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
}

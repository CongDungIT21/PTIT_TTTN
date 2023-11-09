using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character, ITakeHit
{
    [SerializeField]
    Transform skin;

    public int _idx { get; private set; }
    public bool isImmortal { get ; set;}

    private Wave _wave;

    private void Start()
    {
        this.OnInit(10);
    }

    public void OnSetup(Wave wave, int idx)
    {
        this._idx = idx;
        this._wave = wave;
    }

    public void TakeHit(float damage)
    {
        if (isImmortal) return;

        this.OnHit(damage);

        if(this.IsDead())
        {
            //GameObject.Destroy(this.gameObject);
            PoolManager.Despawn(this);
            this._wave.Detach(this);
        }
    }

    public void TweenJoinWave(Vector3[] pathJoin, float t, PathType linear)
    {
        isImmortal = true;

        tf.DOPath(pathJoin, t, linear)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                isImmortal = false;
            });
    }

    public void TweenMoveToTarget(Vector3 posTarget, float t, UnityAction onCompleteCallback)
    {
        tf.DOMove(posTarget, t)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                onCompleteCallback?.Invoke();
            });
    }

    public void TweenMoveUpDown(float t)
    {
        Debug.Log("TweenMoveUpDown");
        float endValue = 0.25f * ((_idx % 2) - 0.5f) * 2;
        float dt = t / 3;

        skin.DOLocalMoveY(endValue, dt)
            .OnComplete(() =>
            {
                skin.DOLocalMoveY(-endValue, dt)
                    .OnComplete(() =>
                    {
                        skin.DOLocalMoveY(0, dt);
                    });
            });
    }
}

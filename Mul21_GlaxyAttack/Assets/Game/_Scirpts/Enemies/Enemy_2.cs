using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : AbstractEnemy, ITakeHit
{
    public bool isImmortal { get ; set ; }

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

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : Character
{
    public Wave wave;

    public abstract void OnSetup(Wave wave);

    public abstract void TweenJoinWave(Vector3[] pathJoin, float t, PathType linear);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : Character
{
    [SerializeField]
    Transform skin;

    private Wave _wave;
}

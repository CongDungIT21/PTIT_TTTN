using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Spark : PoolMember
{
    private void OnEnable()
    {
        Invoke(nameof(OnDespawn), 0.5f);
    }

    private void OnDespawn()
    {
        PoolManager.Despawn(this);
    }
}

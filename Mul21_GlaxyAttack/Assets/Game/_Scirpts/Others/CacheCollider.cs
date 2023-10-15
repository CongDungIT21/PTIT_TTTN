using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CacheCollider
{
    private static Dictionary<Collider, ITakeHit> cacheHits = new Dictionary<Collider, ITakeHit>();

    public static ITakeHit GetITakeHit(Collider collider)
    {
        if(!cacheHits.ContainsKey(collider))
        {
            cacheHits.Add(collider, collider.GetComponent<ITakeHit>());
        }
        return cacheHits[collider];
    }
}

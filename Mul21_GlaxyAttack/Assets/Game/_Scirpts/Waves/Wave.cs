using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointSpawn
{
    public Transform tf_pointSpawn;
    public Transform[] tf_pointModels;
}

public abstract class Wave : MonoBehaviour
{
    protected List<AbstractEnemy> _AliveEnemies = new List<AbstractEnemy>();
    protected Level level;
    public abstract void OnInit(Level level);

    public virtual void Attach(AbstractEnemy enemy)
    {
        this._AliveEnemies.Add(enemy);
    }

    public virtual void Detach(AbstractEnemy enemy)
    {
        this._AliveEnemies.Remove(enemy);

        if(this._AliveEnemies.Count <= 0)
        {
            //TODO: Load Next Wave
            this.AllEnemyDetach();
        }
    }

    public virtual void DetachAllEnemy()
    {
        foreach (AbstractEnemy enemy in _AliveEnemies)
            if(enemy != null) PoolManager.Despawn(enemy);
    }

    public abstract void AllEnemyDetach();
}

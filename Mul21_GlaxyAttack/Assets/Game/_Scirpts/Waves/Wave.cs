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
        Debug.Log("Detach");
        this._AliveEnemies.Remove(enemy);
        Debug.Log(this._AliveEnemies.Count);

        if(this._AliveEnemies.Count <= 0)
        {
            Debug.Log("Alive Count < 0");
            //TODO: Load Next Wave
            this.AllEnemyDetach();
        }
    }

    public abstract void AllEnemyDetach();
}

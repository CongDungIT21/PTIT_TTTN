using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{
    protected List<Enemy> _AliveEnemies = new List<Enemy>();
    protected Level level;
    public abstract void OnInit(Level level);

    public virtual void Attach(Enemy enemy)
    {
        this._AliveEnemies.Add(enemy);
    }

    public virtual void Detach(Enemy enemy)
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

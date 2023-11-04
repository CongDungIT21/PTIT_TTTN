using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{
    protected List<Enemy> _AliveEnemies = new List<Enemy>();

    public abstract void OnInit();

    public virtual void Attach(Enemy enemy)
    {
        this._AliveEnemies.Add(enemy);
    }

    public virtual void Detach(Enemy enemy)
    {
        this._AliveEnemies.Remove(enemy);

        if(this._AliveEnemies.Count < 0)
        {
            //TODO: Load Next Wave
        }
    }

    
}

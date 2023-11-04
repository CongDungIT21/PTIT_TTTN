using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wave_1 : Wave
{
    [SerializeField]
    Transform[] tf_modelPoints;
    [SerializeField]
    Transform tf_pointSpawn;

    private int _idxModelPoint = 0;
    private List<Enemy> _enemies = new List<Enemy>();

    public override void OnInit()
    {
        this.SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < tf_modelPoints.Length; i++)
        {
            Enemy enemy = PoolManager.Spawn<Enemy>(PoolType.ENEMY, tf_pointSpawn.position, Quaternion.identity);
            enemy.OnSetup(this, i);
            this.Attach(enemy);
            _enemies.Add(enemy);
            this.JoinModel(enemy);
        }

        Invoke(nameof(LoopEnemiesMoving), 1f);
    }

    private void JoinModel(Enemy enemy)
    {
        Vector3[] pathJoin = { tf_pointSpawn.position, tf_modelPoints[enemy._idx].position };
        enemy.TweenJoinWave(pathJoin, 1f, DG.Tweening.PathType.Linear);
    } 

    private void LoopEnemiesMoving()
    {
        this.MoveEnemies(this._idxModelPoint++);
    }

    private void MoveEnemies(int idx)
    {
        if(_AliveEnemies.Count <= 0)
        {
            Debug.Log("All Enemies Die");
            return;
        }

        for(int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].gameObject.activeInHierarchy)
            {
                int idxModelPointTarget = (idx + i) % tf_modelPoints.Length;
                Vector3 posTarget = tf_modelPoints[idxModelPointTarget].position;

                _enemies[i].TweenMoveToTarget(posTarget, 0.75f, () => { _enemies[i].TweenMoveUpDown(1f); });
            }
        }

        Invoke(nameof(LoopEnemiesMoving), 1.75f);
    }

}

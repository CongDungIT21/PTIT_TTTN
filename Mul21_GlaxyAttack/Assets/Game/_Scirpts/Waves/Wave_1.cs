using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wave_1 : Wave
{
    [SerializeField]
    PointSpawn[] pointSpawns;

    private int _idxModelPoint = 0;
    private List<AbstractEnemy> _enemies = new List<AbstractEnemy>();

    public override void OnInit(Level level)
    {
        this.level = level;

        foreach (PointSpawn pointSpawn in pointSpawns)
        {
            SpawnEnemies(pointSpawn);
        }
    }

/*    private void SpawnEnemies()
    {
        for (int i = 0; i < tf_modelPoints.Length; i++)
        {
            AbstractEnemy enemy = PoolManager.Spawn<AbstractEnemy>(PoolType.ENEMY_1, tf_pointSpawn.position, Quaternion.identity);
            enemy.OnSetup(this);
            Attach(enemy);
            _enemies.Add(enemy);
            JoinModel(enemy);
        }

        //Invoke(nameof(LoopEnemiesMoving), 1f);
    }*/

    private void SpawnEnemies(PointSpawn pointSpawn)
    {
        Transform tf_pointSpawn = pointSpawn.tf_pointSpawn;
        foreach (Transform pointModel in pointSpawn.tf_pointModels)
        {
            AbstractEnemy enemy = PoolManager.Spawn<AbstractEnemy>(PoolType.ENEMY_1, tf_pointSpawn.position, Quaternion.identity);
            enemy.OnSetup(this);
            Attach(enemy);
            JoinModel(enemy, tf_pointSpawn, pointModel);
        }
    }

    private void JoinModel(AbstractEnemy enemy, Transform spawnPoint, Transform modelPoint)
    {
        Vector3[] pathJoin = { spawnPoint.position, modelPoint.position };
        enemy.TweenJoinWave(pathJoin, 1f, DG.Tweening.PathType.Linear);
    }

    /*    private void LoopEnemiesMoving()
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

                    //_enemies[i].TweenMoveToTarget(posTarget, 0.75f);
                }
            }

            Invoke(nameof(LoopEnemiesMoving), 1.75f);
        }*/

    public override void AllEnemyDetach()
    {
        Debug.Log("All Enemy Detach");
        this.level.LoadNextWave();
    }
}

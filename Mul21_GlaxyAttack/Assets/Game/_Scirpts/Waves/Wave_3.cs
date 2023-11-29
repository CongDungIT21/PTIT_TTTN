using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_3 : Wave
{
    [SerializeField]
    PointSpawn[] pointSpawns;

    public override void AllEnemyDetach()
    {
        level.LoadNextWave();
    }

    private void SpawnEnemies(PointSpawn pointSpawn, PoolType type)
    {
        Transform tf_pointSpawn = pointSpawn.tf_pointSpawn;
        foreach (Transform pointModel in pointSpawn.tf_pointModels)
        {
            AbstractEnemy enemy = PoolManager.Spawn<AbstractEnemy>(type, tf_pointSpawn.position, Quaternion.identity);
            enemy.OnSetup(this);
            this.Attach(enemy);
            this.JoinModel(enemy, tf_pointSpawn, pointModel);
        }
    }

    private void JoinModel(AbstractEnemy enemy, Transform spawnPoint, Transform modelPoint)
    {
        Vector3[] pathJoin = { spawnPoint.position, modelPoint.position };
        enemy.TweenJoinWave(pathJoin, 1f, DG.Tweening.PathType.Linear);
    }

    public override void OnInit(Level level)
    {
        this.level = level;
        this._AliveEnemies = new List<AbstractEnemy>();
        
        for (int i = 0; i < pointSpawns.Length; i++)
        {
            if (i == 1)
            {
                SpawnEnemies(pointSpawns[i], PoolType.ENEMY_3);
            }
            else
            {
                SpawnEnemies(pointSpawns[i], PoolType.ENEMY_1);
            }
        }
    }

}

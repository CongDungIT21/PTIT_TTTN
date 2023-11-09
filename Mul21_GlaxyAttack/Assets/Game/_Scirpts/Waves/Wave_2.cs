using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointSpawn
{
    public Transform tf_pointSpawn;
    public Transform[] tf_pointModels;
}

public class Wave_2 : Wave
{
    [SerializeField]
    PointSpawn[] pointSpawns;

    public override void OnInit(Level level)
    {
        this.level = level;
        foreach (PointSpawn pointSpawn in pointSpawns)
        {
            this.SpawnEnemies(pointSpawn);
        }
    }

    private void SpawnEnemies(PointSpawn pointSpawn)
    {
        Transform tf_pointSpawn = pointSpawn.tf_pointSpawn;
        foreach (Transform pointModel in pointSpawn.tf_pointModels)
        {
            Enemy enemy = PoolManager.Spawn<Enemy>(PoolType.ENEMY, tf_pointSpawn.position, Quaternion.identity);
            enemy.OnSetup(this, 0);
            this.Attach(enemy);
            this.JoinModel(enemy, tf_pointSpawn, pointModel);
        }
    }

    private void JoinModel(Enemy enemy, Transform spawnPoint, Transform modelPoint)
    {
        Vector3[] pathJoin = { spawnPoint.position, modelPoint.position };
        enemy.TweenJoinWave(pathJoin, 1f, DG.Tweening.PathType.Linear);
    }

    public override void AllEnemyDetach()
    {
        this.level.LoadNextWave();
    }
}

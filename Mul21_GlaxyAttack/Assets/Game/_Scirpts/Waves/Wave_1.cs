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
        this._AliveEnemies = new List<AbstractEnemy>();
        this._enemies = new List<AbstractEnemy>();

        foreach (PointSpawn pointSpawn in pointSpawns)
        {
            SpawnEnemies(pointSpawn);
        }
    }

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

    public override void AllEnemyDetach()
    {
        Debug.Log("All Enemy Detach");
        this.level.LoadNextWave();
    }
}

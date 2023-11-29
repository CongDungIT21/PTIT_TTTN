using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_4 : Wave
{
    [SerializeField]
    PointSpawn[] pointSpawns;

    private readonly float TIME_DELAY = 1.5f;

    public override void AllEnemyDetach()
    {
        level.LoadNextWave();
    }

    public override void OnInit(Level level)
    {
        this.level = level;
        this._AliveEnemies = new List<AbstractEnemy>();
        
        for (int i = 0; i < pointSpawns.Length; i++ )
        {
           
            if(i == 1)
            {
                List<Enemy_3> listEnemies_3 = SpawnEnemies_3(pointSpawns[i], PoolType.ENEMY_3);
                StartCoroutine(IE_CycleMoving(listEnemies_3, pointSpawns[i].tf_pointModels, 0));
            }
            else if(i==3)
            {
                SpawnEnemies(pointSpawns[i], PoolType.ENEMY_3);
            }
            else
            {
                SpawnEnemies(pointSpawns[i], PoolType.ENEMY_2);
            }
        }
    }

    private void CycleMoving(List<Enemy_3> listEnemies_3, Transform[] tf_pointModels, int idx)
    {
        if (_AliveEnemies.Count <= 0) return;

        for (int i = 0; i < listEnemies_3.Count; i++)
        {
            if (listEnemies_3[i].gameObject.activeInHierarchy)
            {
                int idxModelPointTarget = (idx + i) % tf_pointModels.Length;
                Vector3 posTarget = tf_pointModels[idxModelPointTarget].position;

                listEnemies_3[i].TweenMoveToTarget(posTarget, 1f);
            }
        }        
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

    private List<Enemy_3> SpawnEnemies_3(PointSpawn pointSpawn, PoolType type)
    {
        List<Enemy_3> listEnemies_3 = new List<Enemy_3>();
        Transform tf_pointSpawn = pointSpawn.tf_pointSpawn;
        foreach (Transform pointModel in pointSpawn.tf_pointModels)
        {
            Enemy_3 enemy = PoolManager.Spawn<Enemy_3>(type, tf_pointSpawn.position, Quaternion.identity);
            enemy.OnSetup(this);
            this.Attach(enemy);
            listEnemies_3.Add(enemy);
            this.JoinModel(enemy, tf_pointSpawn, pointModel);
        }

        return listEnemies_3;
    }

    private void JoinModel(AbstractEnemy enemy, Transform spawnPoint, Transform modelPoint)
    {
        Vector3[] pathJoin = { spawnPoint.position, modelPoint.position };
        enemy.TweenJoinWave(pathJoin, 1f, DG.Tweening.PathType.Linear);
    }

    IEnumerator IE_CycleMoving(List<Enemy_3> listEnemies_3, Transform[] tf_pointModels, int idx)
    {
        Debug.Log("IE_CycleMoving");
        yield return new WaitForSeconds(TIME_DELAY);
        CycleMoving(listEnemies_3, tf_pointModels, idx);
        StartCoroutine(IE_CycleMoving(listEnemies_3, tf_pointModels, idx + 1));
    }
}

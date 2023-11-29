using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    private System.Action _loadCallback;
    public void LoadResource(System.Action callback)
    {
        this._loadCallback = callback;
        this.LoadPlayerData();
        this.LoadEnemyData();
        this._loadCallback?.Invoke();
    }

    private void LoadPlayerData() {
        Debug.Log("Load Player Data Done");
    }

    private void LoadEnemyData()
    {
        Debug.Log("Load Enemy Data Done");
    }
}

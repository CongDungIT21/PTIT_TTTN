using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    [SerializeField]
    Wave[] waves;

    private int _IdxWave;
    private Wave _CurrentWave;

    public Wave CurrentWave { get { return _CurrentWave; } }

    public void OnInit()
    {
        this.LoadStartWave();
        _IdxWave = 0;
        _CurrentWave = this.waves[_IdxWave];
    }

    public void LoadStartWave()
    {
        _IdxWave = 0;
        _CurrentWave = this.waves[_IdxWave];
        this._CurrentWave.OnInit(this);
    }

    public void LoadNextWave()
    {
        this._IdxWave++;

        if (this._IdxWave < this.waves.Length)
        {
            this._CurrentWave = this.waves[this._IdxWave];
            this._CurrentWave.OnInit(this);
        }
        else
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }
}

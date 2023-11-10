using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    Level[] levels;

    private Level _CurrentLevel;
    private int _idxLevel;

    private void Start()
    {
       LoadStartLevel();
    }

    private void LoadStartLevel()
    {
        _idxLevel = 0;
        this.LoadLevel();
    }

    public void LoadNextLevel()
    {
        _idxLevel++;
        this.LoadLevel();
    }

    private void LoadLevel()
    {
        Debug.Log("Load Level: " + _idxLevel);
        this._CurrentLevel = this.levels[this._idxLevel];
        this._CurrentLevel.OnInit();
    }
}

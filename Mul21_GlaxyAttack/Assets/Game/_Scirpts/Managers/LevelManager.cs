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
        this.LoadStartLevel();
    }

    private void LoadStartLevel()
    {
        _idxLevel = 0;
        this._CurrentLevel = this.levels[this._idxLevel];
        this._CurrentLevel.OnInit();
    }
}

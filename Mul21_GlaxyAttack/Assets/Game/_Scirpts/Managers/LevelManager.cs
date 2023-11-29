using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    Level[] levels;

    private Level _CurrentLevel;
    private int _idxLevel;

    public Action OnStartLevel;

    private void OnEnable()
    {
        OnStartLevel += StartLevel;
    }

    private void OnDisable()
    {
        OnStartLevel -= StartLevel;
    }

    public void LoadStartLevel()
    {
        Debug.Log("load Start level");
        _idxLevel = 0;
        this._CurrentLevel = this.levels[this._idxLevel];
       // this.ClearLevel();
        this.LoadLevel();
    }

    public void ClearIndex()
    {
        _idxLevel = 0;
    }

    public void ClearLevel()
    {        
        this._CurrentLevel.CurrentWave.DetachAllEnemy();
    }

    public void LoadNextLevel()
    {
        EndLevel();
        IEDelayAction(null, 2f);

        if (_idxLevel + 1 < this.levels.Length)
        {
            _idxLevel++;
            LoadLevel();
        }
        
    }

    private void LoadLevel()
    {
        Debug.Log("Load Level: " + this._idxLevel);
        this._CurrentLevel = this.levels[this._idxLevel];
        GUIManager.Instance.CreatePopup(POPUP_NAME.PopupReward, "Level " + (_idxLevel + 1));
    }

    private void StartLevel()
    {
        Debug.Log("Start Level");
        this._CurrentLevel.OnInit();
        Ship.Instance.OnStartLevel();
    }

    private void EndLevel()
    {
        Ship.Instance.OnStopLevel();   
        if(_idxLevel + 1 >= this.levels.Length)
        {
            GameManager.Instance.EndGameFinish();
        }
    }

    IEnumerator IEDelayAction(UnityAction callBack, float delay)
    {
        yield return new WaitForSeconds(delay);
        callBack?.Invoke();
    }
}

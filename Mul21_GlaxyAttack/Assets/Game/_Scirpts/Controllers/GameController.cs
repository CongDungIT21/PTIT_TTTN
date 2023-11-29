using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE
{
    NONE,
    LOADING,
    START,
    PLAYING,
    PAUSE,
    END
}
public class GameController : Singleton<GameController>
{
    [SerializeField]
    ResourceSystem _ResourceSystem;
    private GAME_STATE _currentState;
    public GAME_STATE CurrentState
    { 
        get { return _currentState; } 
        set { _currentState = value; }
    }

        
 
    public void Start()
    {
        this._currentState = GAME_STATE.LOADING;
        _ResourceSystem.LoadResource(this.OnLoadedResourceSuccess);        
    }

    public void OnLoadedResourceSuccess()
    {
        Debug.Log("On Loaded Resource Success");     
        GameManager.Instance.StartGame();
    }
}

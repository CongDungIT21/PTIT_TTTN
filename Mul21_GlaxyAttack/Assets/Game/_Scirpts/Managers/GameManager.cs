using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    Ship ship;

    public void StartGame()
    {
        Debug.Log("Start Game");
        GameController.Instance.CurrentState = GAME_STATE.START;
        SetupGameObject();
        SetupGameUI();
    }

    public void PlayGame()
    {
        Debug.Log("Play Game");
        GameController.Instance.CurrentState = GAME_STATE.PLAYING;
        GUIManager.Instance.SetScreen(SCREEN_NAME.ScreenMain);
        ship.OnPlay(LoadLevel);
    }

     public void PauseGame()
    {
        GameController.Instance.CurrentState = GAME_STATE.PAUSE;
        Time.timeScale = 0;
    }

     public void EndPauseGame()
    {
        GameController.Instance.CurrentState = GAME_STATE.PLAYING;
        Time.timeScale = 1f;
    }

    public void EndPlay()
    {
        Time.timeScale = 1f;
        GameController.Instance.CurrentState = GAME_STATE.END;
        LevelManager.Instance.ClearLevel();
        LevelManager.Instance.ClearIndex();
        PoolManager.Collect(PoolType.BULLET_SHIP_1);
        PoolManager.Collect(PoolType.BULLET_ENEMY_1);
        PoolManager.Collect(PoolType.COIN);
        PoolManager.Collect(PoolType.VFX_SPARK);
        Ship.Instance.OnStopLevel();
    }

    public void EndGameLose()
    {
        GameController.Instance.CurrentState = GAME_STATE.END;
        ScreenOver screenOver = (ScreenOver) GUIManager.Instance.SetScreen(SCREEN_NAME.ScreenOver);
        screenOver.Init("" + ShipPreferences.Instance.GetCoin(), "Game Over");
    }

    public void EndGameFinish()
    {
        GameController.Instance.CurrentState = GAME_STATE.END;
        ScreenOver screenOver = (ScreenOver)GUIManager.Instance.SetScreen(SCREEN_NAME.ScreenOver);
        screenOver.Init("" + ShipPreferences.Instance.GetCoin(), "Game Finish");
    }
    private void SetupGameUI()
    {
        GUIManager.Instance.SetScreen(SCREEN_NAME.ScreenHome);
    }


    private void SetupGameObject()
    {
        ship.Init();
        ship.OnStart();
    }

    private void LoadLevel()
    {
        Debug.Log("Load Level");
        LevelManager.Instance.LoadStartLevel();
    }

    public void ClearGame()
    {
        PoolManager.Collect(PoolType.COIN);
        PoolManager.Collect(PoolType.VFX_SPARK);
    }
}

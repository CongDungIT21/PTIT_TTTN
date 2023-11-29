using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu : AbstractPopup
{
    public void OnClickBtnClose()
    {
        GameManager.Instance.EndPauseGame();
        Dismiss();
    }

    public void OnClickBtnHome()
    {
        GUIManager.Instance.SetScreen(SCREEN_NAME.ScreenHome);
        GameManager.Instance.EndPlay();
        Ship.Instance.OnStart();
        Dismiss();
    }

    public void OnClickBtnReplay()
    {
        GameManager.Instance.EndPlay();
        LevelManager.Instance.LoadStartLevel();
        Ship.Instance.OnPlay();
        Dismiss();
    }

    public void OnClickBtnRank()
    {
        Debug.Log("OnClickBtnRank");
        GUIManager.Instance.CreatePopup(POPUP_NAME.PopupRank);
    }

    public void OnClickBtnSetting()
    {
        Debug.Log("OnClickBtnSetting");
        GUIManager.Instance.CreatePopup(POPUP_NAME.PopupSetting);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ScreenHome : AbstractScreen
{
    public void OnClickBtnTutorial()
    {
        GUIManager.Instance.CreatePopup(POPUP_NAME.PopupTutorial);
    }

    public void OnClickBtnExit()
    {
        Debug.Log("OnclickbtnExit");
        EditorApplication.ExitPlaymode();
    }

    public void OnClickBtnSetting()
    {
        GUIManager.Instance.CreatePopup(POPUP_NAME.PopupSetting);
    }
    
    public void OnClickBtnRank()
    {
        GUIManager.Instance.CreatePopup(POPUP_NAME.PopupRank);
    }

    public void OnClickBtnStore()
    {
        Debug.Log("OnClickBtnStore");
    }

    public void OnClickBtnMode()
    {
        Debug.Log("OnclickbtnMode");
    }

    public void OnClickBackground()
    {
        GameManager.Instance.PlayGame();
        this.Hide();
    }
}

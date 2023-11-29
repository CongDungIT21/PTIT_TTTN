using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPause : AbstractPopup
{
    public void OnClickBackground()
    {
        ScreenMain screenMain = (ScreenMain) GUIManager.Instance.GetScreen(SCREEN_NAME.ScreenMain);
        if (screenMain) screenMain.OnClickBtnPlay();
        Dismiss();
    }
}

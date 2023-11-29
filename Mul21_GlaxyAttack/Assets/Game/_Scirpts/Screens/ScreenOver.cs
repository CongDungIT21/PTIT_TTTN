using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenOver : AbstractScreen
{
    public TMP_Text coinText;
    public TMP_Text resultText;
    public void Init(string coin, string result)
    {        
        coinText.text = "Coin: " + coin;
        resultText.text = result;
    }

    public void OnClickBtnReplay()
    {       
        GUIManager.Instance.SetScreen(SCREEN_NAME.ScreenMain);
        GameManager.Instance.EndPlay();
        LevelManager.Instance.LoadStartLevel();
        Ship.Instance.OnPlay();
        Ship.Instance.OnStart();
        Hide();
    }

    public void OnClickBtnHome()
    {
        GUIManager.Instance.SetScreen(SCREEN_NAME.ScreenHome);
        GameManager.Instance.EndPlay();
        Ship.Instance.OnStart();
        Hide();
    }
}

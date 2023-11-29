using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenMain : AbstractScreen
{
    [SerializeField]
    public TMP_Text CoinText;

    public GameObject PauseObj;
    public GameObject PlayObj;

    private void OnEnable()
    {
        ShipPreferences.Instance.OnChangeCoin += SetCoinText;
        CoinText.text = "0";
        PauseObj.SetActive(true);
        PlayObj.SetActive(false) ;
    }

    private void OnDisable()
    {
        ShipPreferences.Instance.OnChangeCoin -= SetCoinText;
    }

    private void SetCoinText()
    {
        this.CoinText.text = ShipPreferences.Instance.GetCoin().ToString();
    }
    public void OnClickBtnMenu()
    {
        GUIManager.Instance.CreatePopup(POPUP_NAME.PopupMenu);
        GameManager.Instance.PauseGame();
    }   

    public void OnClickBtnPause()
    {
        PauseObj.SetActive(false);
        PlayObj.SetActive(true);
        GUIManager.Instance.CreatePopup(POPUP_NAME.PopupPause);
        GameManager.Instance.PauseGame();        
    }    

    public void OnClickBtnPlay()
    {
        PauseObj.SetActive(true);
        PlayObj.SetActive(false);
        GameManager.Instance.EndPauseGame();
    }
}

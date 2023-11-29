using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnSceenActive(AbstractScreen screen);
public class GUIManager : Singleton<GUIManager>
{
    public Transform ScreenRoot;
    public Transform PopupRoot;

    private Dictionary<SCREEN_NAME, AbstractScreen> _screens = new Dictionary<SCREEN_NAME, AbstractScreen>();
    private Dictionary<POPUP_NAME, AbstractPopup> _dontDestroyPopups = new Dictionary<POPUP_NAME, AbstractPopup>();

    private SCREEN_NAME _currentScreenName;
    public int NumberPopupShow;

    private void Awake()
    {
        ClearScreens();
        ClearPopups(true);
    }

    #region Screen
    public void ClearScreens()
    {
        for (int i = 0; i < this.ScreenRoot.childCount; i++)
        {
            Transform child = this.ScreenRoot.GetChild(i);
            if (child == null) 
                continue;
            Destroy(child.gameObject);
        }
    }

    private AbstractScreen LoadScreen(SCREEN_NAME screenName)
    {
        UnityEngine.Object o = Resources.Load("Screens/" + EnumUtils.ParseString(screenName));

        GameObject go = (GameObject)Instantiate(o, ScreenRoot);

        if (go == null)
        {
            return null;
        }

        Vector3 pos = go.transform.localPosition;
        go.transform.localPosition = pos;
        go.transform.localScale = Vector3.one;

        AbstractScreen obj = go.GetComponent<AbstractScreen>();

        if (obj == null)
        {
            Debug.Log(22222);
            return null;
        }

        obj.name = EnumUtils.ParseString(screenName);

        if (_screens.ContainsKey(screenName))
        {
            Debug.Log(3333);
            _screens[screenName] = obj;
        }
        else
        {
            Debug.Log(44444);
            _screens.Add(screenName, obj);
        }
        Debug.Log(5555);
        return obj;
    }

    public AbstractScreen GetScreen(SCREEN_NAME screenName, bool isForceLoad = true)
    {
        if (_screens.ContainsKey(screenName) == false)
        {
            if (isForceLoad)
                LoadScreen(screenName);
            else
                return null;
        }

        return _screens[screenName];
    }

    public AbstractScreen SetScreen(SCREEN_NAME screenName, OnSceenActive onScreenActive = null)
    {
        if (_screens.ContainsKey(_currentScreenName))
        {
            _screens[_currentScreenName].Hide();
        }

        _currentScreenName = screenName;
        AbstractScreen screen = GetScreen(_currentScreenName);
        screen.Show();
        if (onScreenActive != null) 
            onScreenActive(screen);
        return screen;
    }
    #endregion

    #region Popup
    public void ClearPopups(bool isDestroyAll = false)
    {
        for (int i = 0; i < this.PopupRoot.childCount; i++)
        {
            Transform child = this.PopupRoot.GetChild(i);
            if (child == null) continue;

            if (!isDestroyAll)
            {
                AbstractPopup popup = child.GetComponent<AbstractPopup>();
                if (popup == null)
                {
                    Destroy(child.gameObject);
                }
                else
                    popup.Dismiss();
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
    }

    public GameObject GetPopup(POPUP_NAME popupName)
    {
        return Resources.Load("Popups/" + EnumUtils.ParseString(popupName)) as GameObject;
    }

    public AbstractPopup CreatePopup(POPUP_NAME popupName, params object[] args)
    {
        Debug.Log("Create Popup" + EnumUtils.ParseString(popupName));
        if (this._dontDestroyPopups.ContainsKey(popupName))
        {
            AbstractPopup popup = this._dontDestroyPopups[popupName];
            popup.Show(args);
            popup.transform.SetAsLastSibling();
            popup.gameObject.SetActive(true);
            this.NumberPopupShow += 1;
            return popup;
        };

        GameObject pref = GetPopup(popupName);
        if (pref != null)
        {
            GameObject obj = Instantiate(pref, this.PopupRoot) as GameObject;
            AbstractPopup popup = obj.GetComponent<AbstractPopup>();
            popup.name = EnumUtils.ParseString(popupName);
            popup.Create(args);

            if (popup.IsDontDestroy)
                this._dontDestroyPopups[popupName] = popup;

            popup.transform.SetAsLastSibling();
            popup.gameObject.SetActive(true);
            this.NumberPopupShow += 1;
            return popup;
        }
        return null;
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPopup : MonoBehaviour
{
    public bool IsDontDestroy;

    public virtual void Create(params object[] paras)
    {

    }
    public virtual void Show(params object[] paras)
    {

    }

    public virtual void Hide(params object[] paras)
    {

    }

    public virtual void Dismiss()
    {
        GUIManager.Instance.NumberPopupShow -= 1;
        if (!this.IsDontDestroy)
            Destroy(this.gameObject);
        else
            this.gameObject.SetActive(false);
    }
}

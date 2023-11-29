using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupReward : AbstractPopup
{
    public TMP_Text txtLevel;

    public override void Create(params object[] paras)
    {
        base.Create(paras);
        txtLevel.text = (string) paras[0];
    }
    public void OnClickItemBarrel()
    {
        Ship.Instance.SetPowerup(POWERUP.BARREL);
        LevelManager.Instance.OnStartLevel?.Invoke();
        this.Dismiss();
    }

    public void OnClickItemHP()
    {
        Ship.Instance.SetPowerup(POWERUP.HP);
        LevelManager.Instance.OnStartLevel?.Invoke();
        this.Dismiss();
    }
}

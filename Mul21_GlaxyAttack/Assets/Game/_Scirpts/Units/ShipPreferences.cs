using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPreferences : Singleton<ShipPreferences>
{
    private ShipData _data;

    public event Action OnChangeCoin;

    private void Start()
    {
        Init();        
    }

    private void Init()
    {
        //TODO Initlize Shipdata     
        this._data = new ShipData();
        this._data.NumCoin = 0;
    }

    public int GetCoin()
    {
        return this._data.NumCoin;
    }

    public bool AddCoin(int num)
    {
        bool result = this._data.NumCoin + num >= 0;
        if(result)
        {
            this._data.NumCoin += num;
            this.OnChangeCoin?.Invoke();
        }

        return result;
    } 

}

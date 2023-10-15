using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, ITakeHit
{
    private void Start()
    {
        this.OnInit(10);
    }

    public void TakeHit(float damage)
    {
        this.OnHit(damage);

        Debug.Log(this._hp);
        if(this.IsDead())
        {
            Debug.Log("Deddddddddddddddddddddđ");
            GameObject.Destroy(this.gameObject);
        }
    }
}

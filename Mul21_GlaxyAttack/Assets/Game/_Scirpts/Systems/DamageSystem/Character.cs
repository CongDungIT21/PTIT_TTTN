using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : PoolMember
{
    private float _MAX_HP;
    public float _hp;

    public void OnInit(float defHP)
    {
        this._MAX_HP = defHP;
        this._hp = defHP;
    }

    public bool IsDead()
    {
        return this._hp <= 0;
    }

    private void ResertHP()
    {
        this._hp = this._MAX_HP;
    }

    public void OnHit(float damage)
    {
        this._hp -= damage;
    }

    public void OnHeal(float heal)
    {
        this._hp += heal;
    }
}

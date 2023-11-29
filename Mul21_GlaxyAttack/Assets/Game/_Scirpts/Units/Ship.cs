using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.EventSystems;

[System.Serializable]
public class Barrel
{
    public float rate;
    public Transform[] points;
}

public enum POWERUP
{
    NONE,
    HP,
    BARREL
}

public class Ship : Character, ITakeHit, IShootable
{
    //public static Action<POWERUP> OnSetPowerup;
    public static Ship Instance;

    [SerializeField]
    Barrel[] barrels;
    CounterTime counterTime = new CounterTime();
    
    private int _barrelIdx = 0;
    Vector2 clampfMouse = new Vector2(3, 5);
    private bool canControl = false;
    private bool isShooting = false;
    public bool isImmortal { get ; set ; }

    private POWERUP _currPowerup = POWERUP.NONE;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        canControl = false;
        OnInit(20);
    }

    public void OnStart()
    {
        //canControl = false;
        tf.position = new Vector3(0, -10, 0);

        DOTween.Sequence()
            .Append(tf.DOMove(new Vector3(0, -2, 0), 0.5f).SetEase(Ease.Linear))
            .AppendInterval(1f);
    }

    public void OnPlay(Action callback = null)
    {
        DOTween.Sequence()
            .Append(tf.DOMoveY(tf.position.y + 10, 1).SetEase(Ease.Linear).OnComplete(() => tf.position = new Vector3(0, -10, 0)))
            .Append(tf.DOMove(new Vector3(0, -3, 0), 0.5f).SetEase(Ease.Linear))
            .AppendCallback(() =>
            {
                callback?.Invoke();
            });

    }

    private void OnDead()
    {
        this.isImmortal = true;
        this.isShooting = false;
        this.canControl = false;
        GameManager.Instance.EndGameLose();
    }

    public void OnStartLevel()
    {
        switch (this._currPowerup)
        {
            case POWERUP.BARREL:
                ApplyPowerupBarrel();
                break;
            case POWERUP.HP:                
                ApplyPowerupHP();
                break;
            case POWERUP.NONE:
                break;
        }

        isShooting = true;
        canControl = true;
        Shoot();
    }

    public void OnStopLevel()
    {
        switch (_currPowerup)
        {
            case POWERUP.BARREL:
                ResertPowerupBarrel();
                break;
            case POWERUP.HP:
                break;
            case POWERUP.NONE: 
                break;
        }

        isShooting = false;
        canControl = false;
    }

    public void SetPowerup(POWERUP type)
    {
        this._currPowerup = type;
    }


    private void Update()
    {
        if (canControl)
        {
            if(!IsPointerOverUIObject())
                ControlShip();
        }

        if(isShooting)
        {
            counterTime.CounterExecute();
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void ControlShip()
    {        
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePoint.x = Mathf.Clamp(mousePoint.x, -clampfMouse.x, clampfMouse.x);
            mousePoint.y = Mathf.Clamp(mousePoint.y, -clampfMouse.y, clampfMouse.y);
            mousePoint.z = 0;

            tf.position = Vector3.Lerp(tf.position, mousePoint, Time.deltaTime * 20f);
        }
    }


    private void Fire()
    {
        //if (!isShooting) return;
        AudioSystem.Instance.PlaySoundByName(SOUND_NAME.ShipShoot);
        Barrel barrel = barrels[_barrelIdx];
        for (int i = 0; i < barrel.points.Length; i++)
        {
            PoolManager.Spawn(PoolType.BULLET_SHIP_1, barrel.points[i].position, barrel.points[i].rotation);
        }

        counterTime.CounterStart(null, Fire, barrel.rate);
    }


    IEnumerator IEDelayAction(UnityAction callBack, float delay)
    {
        yield return new WaitForSeconds(delay);
        callBack?.Invoke();
    }

    public void TakeHit(float damage)
    {        
        OnHit(damage);
        if (this.IsDead())
        {
            OnDead();
        }
    }

    public void Shoot()
    {
        isShooting = true;
        counterTime.CounterStart(null, Fire, 0.2f);        
    }

    private void ApplyPowerupBarrel()
    {
        this._barrelIdx += 2;
    }

    private void ApplyPowerupHP()
    {
        this.OnHeal(10);
    }

    private void ResertPowerupBarrel()
    {
        this._barrelIdx -= 2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Barrel
{
    public float rate;
    public Transform[] points;
}

public class Ship : GameUnit
{
    [SerializeField]
    Barrel[] barrels;
    CounterTime counterTime = new CounterTime();

    private float _speed = 300f;
    private int _barrelIdx = 0;

    Vector3 mousePoint;
    Vector2 clampfMouse = new Vector2(3, 5);

    private bool canControl = false;

    private void Start()
    {
        counterTime.CounterStart(null, Fire, 0.2f);
        canControl = true;
    }

    private void Update()
    {
        if (canControl)
        {
            ControlShip();
        }
    }

    private void ControlShip()
    {
        counterTime.CounterExecute();

        if (Input.GetMouseButton(0))
        {
            mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePoint.x = Mathf.Clamp(mousePoint.x, -clampfMouse.x, clampfMouse.x);
            mousePoint.y = Mathf.Clamp(mousePoint.y, -clampfMouse.y, clampfMouse.y);
            mousePoint.z = 0;

            tf.position = Vector3.Lerp(tf.position, mousePoint, Time.deltaTime * 20f);
        }
    }


    private void Fire()
    {
        Barrel barrel = barrels[_barrelIdx];
        for (int i = 0; i < barrel.points.Length; i++)
        {
            PoolManager.Spawn(PoolType.BULLET, barrel.points[i].position, barrel.points[i].rotation);
        }

        counterTime.CounterStart(null, Fire, barrel.rate);
    }


    IEnumerator IEDelayAction(UnityAction callBack, float delay)
    {
        yield return new WaitForSeconds(delay);
        callBack?.Invoke();
    }
}

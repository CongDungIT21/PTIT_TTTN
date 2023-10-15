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
    //[SerializeField] GameObject holdEffect;
    //public BulletPoint[] bulletPos //rateFire và các vị trí X
    //CounterTime counterTime = new CounterTime() X
    //ShipState shipState = ShipState.Alive
    //public GameObject[] shipSkin; // Cacs drone theo sau
    //[SerializeField] Animator shipAnim;
    //public Transform skin;

    [SerializeField]
    Barrel[] barrels;
    CounterTime counterTime = new CounterTime();

    private float _speed = 300f;
    private int _barrelIdx = 0;

    Vector3 mousePoint;
    Vector2 clampfMouse = new Vector2(6, 10);

    private bool canControl = false;

    private void Start()
    {
        counterTime.CounterStart(null, Fire, 0.2f);
        //shipState = ShipState.Alive;
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
        //if (canControl && Ply_GameManager.gameState == GameState.GamePlay)
        //{
            counterTime.CounterExecute();

            if (Input.GetMouseButton(0))
            {
                mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePoint.x = Mathf.Clamp(mousePoint.x, -clampfMouse.x, clampfMouse.x);
                mousePoint.y = Mathf.Clamp(mousePoint.y, -clampfMouse.y, clampfMouse.y);
                mousePoint.z = 0;

                tf.position = Vector3.Lerp(tf.position, mousePoint, Time.deltaTime * 20f);
            }
        //}
    }

    //internal void LevelUp()
    //{
    //    if (bulletIndex + 1 < bulletPos.Length)
    //    {
    //        bulletIndex++;
    //        Ply_SoundManager.Ins.PlayFx(FxType.PowerUp);
    //    }
    //}

    //public void ChangeShip(ShipType shipType)
    //{
    //    if (currentShip != null)
    //    {
    //        currentShip.SetActive(false);
    //    }

    //    currentShip = shipSkin[(int)shipType];

    //    currentShip.SetActive(true);
    //}

    private void Fire()
    {
        Barrel barrel = barrels[_barrelIdx];
        for (int i = 0; i < barrel.points.Length; i++)
        {
            PoolManager.Spawn(PoolType.BULLET, barrel.points[i].position, barrel.points[i].rotation);
        }

        counterTime.CounterStart(null, Fire, barrel.rate);
        //Ply_SoundManager.Ins.PlayFx(FxType.Shoot);
    }

    //public void Moving(Vector3 targetPoint, float duration, UnityAction callBack, float delay = 0)
    //{
    //    canControl = false;
    //    tf.DOMove(targetPoint, duration).OnComplete(
    //        () =>
    //        {
    //            callBack?.Invoke();
    //            canControl = true;
    //        }).SetDelay(delay);
    //}

    //public void HoldEffect(bool active)
    //{
    //    holdEffect.SetActive(active);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (Ply_GameManager.gameState == GameState.GamePlay)
    //    {
    //        //chet lan dau tien -> hoi sinh
    //        switch (shipState)
    //        {
    //            case ShipState.Alive:
    //                Ply_Pool.Ins.Spawn(PoolType.VFX_Explore, tf.position, Quaternion.identity).transform.localScale = Vector3.one * 3;
    //                shipState = ShipState.Reviving;
    //                skin.localPosition = Vector3.up * -10;
    //                Ply_UIManager.Ins.OpenUI(UIID.Alert);

    //                skin.DOLocalMove(Vector3.zero, .7f).OnComplete(() =>
    //                {
    //                    shipAnim.SetBool("Fade", true);

    //                    StartCoroutine(IEDelayAction(
    //                        () =>
    //                        {
    //                            shipAnim.SetBool("Fade", false);
    //                            shipState = ShipState.Revived;
    //                            Ply_UIManager.Ins.CloseUI(UIID.Alert);
    //                        }, 2f));
    //                });

    //                break;

    //            //dang chet
    //            case ShipState.Reviving:
    //                break;

    //            //chet len thu 2 la lose luon
    //            case ShipState.Revived:

    //                canControl = false;
    //                gameObject.SetActive(false);
    //                Ply_UIManager.Ins.OpenUI(UIID.Lose);
    //                Ply_Pool.Ins.Spawn(PoolType.VFX_Explore, tf.position, Quaternion.identity).transform.localScale = Vector3.one * 3;

    //                break;
    //        }
    //    }
    //}

    IEnumerator IEDelayAction(UnityAction callBack, float delay)
    {
        yield return new WaitForSeconds(delay);
        callBack?.Invoke();
    }
}

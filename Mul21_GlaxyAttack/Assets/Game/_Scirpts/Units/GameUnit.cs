using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    private Transform trans;
    public Transform tf
    {
        get
        {
            if (trans == null)
            {
                trans = transform;
            }
            return trans;
        }
    }
}

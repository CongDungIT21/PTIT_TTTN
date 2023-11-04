using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _Instance;

    private void Awake()
    {
        _Instance = this as T;
    }

    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<T>();

                if(_Instance == null)
                {
                    _Instance = new GameObject(nameof(T)).AddComponent<T>();
                }
            }            

            return _Instance;
        }
    }    
}

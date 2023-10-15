﻿using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    Transform m_sRoot = null;
    PoolMember prefab = null;

    Queue<PoolMember> inactive;
    List<PoolMember> active;

    bool m_clamp;
    bool m_collect;
    int m_Amount;

    public bool isCollect { get => m_collect; }
    public int Count
    {
        get { return inactive.Count; }
    }
    public PoolType poolType()
    {
        return prefab.poolType;
    }

    // Constructor
    public Pool(Transform parent, PoolMember prefab, int initialQty,  bool collect, bool clamp)
    {
        this.PreLoad(parent, prefab, initialQty, collect, clamp);
    }

    public void PreLoad(Transform parent, PoolMember prefab, int initialQty, bool collect, bool clamp)
    {        
        this.m_sRoot = parent;
        this.prefab = prefab;
        this.m_collect = collect;
        this.m_clamp = clamp; //Có cố định số lượng không
        if (m_collect) 
            active = new List<PoolMember>(); //Có lưu trữ những cái đang active không

        if (m_clamp) 
            m_Amount = initialQty;

        this.inactive = new Queue<PoolMember>();
        for(int i = 0; i < initialQty; i++)
        {
            PoolMember clone = (PoolMember) GameObject.Instantiate(prefab);
            clone.transform.parent = m_sRoot;
            clone.gameObject.SetActive(false);
            this.inactive.Enqueue(clone);
        }
    }

    // Spawn an object from our pool
    public PoolMember Spawn(Vector3 pos, Quaternion rot)
    {
        //[Nếu clamp = true và đả spawn hết thì sao ????]

        // Grab the last object in the inactive array
        PoolMember obj = inactive.Dequeue();

        if (obj == null)
        {
            Debug.Log("Null rooif");
            return Spawn(pos, rot);
        }

        obj.tf.SetPositionAndRotation(pos, rot);
        obj.gameObject.SetActive(true);

        if (m_collect) active.Add(obj);
        if (m_clamp && active.Count >= m_Amount) Despawn(active[0]); // Để luôn giữ số luong active <= amount init

        return obj;
    }


    // Return an object to the inactive pool.
    public void Despawn(PoolMember obj)
    {
        obj.gameObject.SetActive(false);
        if (m_collect) 
            active.Remove(obj);
        inactive.Enqueue(obj);
    }

    public void Clamp(int amount)
    {
        while (inactive.Count > amount)
        {
            PoolMember go = inactive.Dequeue();
            GameObject.DestroyImmediate(go);
        }
    }
    public void Release()
    {
        while (inactive.Count > 0)
        {
            PoolMember go = inactive.Dequeue();
            GameObject.DestroyImmediate(go);
        }
        inactive.Clear();
    }

    public void Collect()
    {
        while (active.Count > 0)
        {
            Despawn(active[0]);
        }
    }
}
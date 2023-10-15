using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterTime
{
    private UnityAction actionComplete;
    private float totalTime;
    private float time;
    public float m_Time => totalTime - time;

    public void CounterStart(UnityAction startAction, UnityAction doneAction, float time)
    {
        startAction?.Invoke();
        actionComplete = doneAction;
        totalTime = time;
        this.time = time;
    }

    public void CounterExecute()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                time = 0;
                CounterExit();
            }
        }
    }

    private void CounterExit()
    {
        actionComplete?.Invoke();
    }
}

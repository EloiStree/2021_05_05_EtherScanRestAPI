using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_TimerRequestPusher : MonoBehaviour
{
    public float m_timeStart = 2;
    public float m_timeLoop = 5;
    public UnityEvent m_push;
    
    void Start()
    {
        InvokeRepeating("Push", m_timeStart, m_timeLoop);
        
    }

    public void Push() {
        m_push.Invoke();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Experiment_ObserverWorkersAPIUpdated : JsonRequestObserverAPI
{
    public EthMineRequest_Workers m_workersInfo;

    private void Awake()
    {
        m_workersInfo = new EthMineRequest_Workers("");
        m_workersInfo.AddListener(CheckForChange);
        InvokeRepeating("Ping", 1, m_pingTimeInSeconds);
    }



    public void SetAddress(string minerAddress)
    {
        m_workersInfo.SetAddress(minerAddress);
    }

    public override PublicRestRequest GetRequest()
    {
        return m_workersInfo;
    }

    public string GetFocusAddress()
    {
        return m_workersInfo.GetFocusAddress();
    }
}

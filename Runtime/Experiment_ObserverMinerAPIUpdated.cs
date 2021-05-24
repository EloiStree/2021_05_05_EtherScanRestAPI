using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Experiment_ObserverMinerAPIUpdated : JsonRequestObserverAPI
{
    public EthMineRequest_MinerCurrentStats m_minerInfo;
  

    private void Awake()
    {
        m_minerInfo = new EthMineRequest_MinerCurrentStats("");
        m_minerInfo.AddListener(CheckForChange);
        InvokeRepeating("Ping", 1, m_pingTimeInSeconds);
    }

    public void SetAddress(string minerAddress)
    {
        m_minerInfo.SetAddress(minerAddress);
    }

    public override PublicRestRequest GetRequest()
    {
        return m_minerInfo;
    }
}


public abstract class JsonRequestObserverAPI :MonoBehaviour{
    public Experiment_EtherRequestAntiSpamAPI m_antiSpamSender;
    private string m_previousJson;

    public float m_pingTimeInSeconds = 10;
    public UnityEvent m_apiHadBeenRefresh;
    public void CheckForChange(PublicRestRequest arg0)
    {
        if (arg0.HasBeenDownloaded() && !arg0.HasError() && arg0.HasText())
        {
            string json = arg0.GetText();
            if (json != m_previousJson)
            {
                m_apiHadBeenRefresh.Invoke();
                m_previousJson = json;
                //Debug.Log("Ping:" + m_previousJson);
            }
        }
    }

    public void AddListener(UnityAction action)
    {
        m_apiHadBeenRefresh.AddListener(action);
    }

    public void ForceReload()
    {
        Ping();
    }
    void Ping()
    {
        if (m_antiSpamSender)
            m_antiSpamSender.AddRequest(GetRequest());
    }

    public abstract PublicRestRequest GetRequest();
}
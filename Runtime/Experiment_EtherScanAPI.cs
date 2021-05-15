using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Experiment_EtherScanAPI : MonoBehaviour
{

    [Range(0.2f,2)]
    public float m_antiSpamTime=0.3f;
    public AntiSpamRequest m_antiSpam = new AntiSpamRequest();
    public bool m_useUpdateTForAntiSpam=true;

    public void AddFromUrl(string urlOfRequest, out EtherScanRequest request, List<EtherScanRequest> debugList = null)
    {
        request = new EtherScanRequestDefault(urlOfRequest);
        m_antiSpam.Add(request);
        if (debugList != null)
            debugList.Add(request);

    }

    public void AddRequest(EtherScanRequest request)
    {
        m_antiSpam.Add(request);
    }

    //https://api.etherscan.io/apis
    void Start()
    {

        m_antiSpam.m_timeBetweenRequestSend = m_antiSpamTime;
    }

    void Update()
    {
        if (m_useUpdateTForAntiSpam)
            CheckForNext();
        
    }

    private void CheckForNext()
    {
        if (m_antiSpam.HasRequestAndTimepastEnough()) { 
            EtherScanRequest tosend =   m_antiSpam.DequeueRquestToSend();
            StartCoroutine(tosend.SendRequest());
        }
    }

    private void OnValidate()
    {
        m_antiSpam.m_timeBetweenRequestSend = m_antiSpamTime;
    }

}



public class AntiSpamRequest {

    public float m_timeBetweenRequestSend = 0.4f;
    public Queue<EtherScanRequest> m_toSend = new Queue<EtherScanRequest>();
    public DateTime m_lastSendRequest;

    public AntiSpamRequest() {
        m_lastSendRequest = DateTime.Now;
    }

    public void Add(EtherScanRequest request) { m_toSend.Enqueue(request); }

    public bool HasRequest() { return m_toSend.Count > 0; }
    public bool HasTimepastEnough() { return (DateTime.Now - m_lastSendRequest).TotalSeconds > m_timeBetweenRequestSend; }
    public bool HasRequestAndTimepastEnough() { return HasRequest() && HasTimepastEnough(); }

    public EtherScanRequest DequeueRquestToSend() {
        if (m_toSend == null)
            return null;
        EtherScanRequest r = m_toSend.Dequeue();
        if (r != null) {
            r.SendRequest();
            m_lastSendRequest = DateTime.Now;
            return r;
        }
        return null;
    }
}




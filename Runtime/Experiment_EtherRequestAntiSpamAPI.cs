using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Experiment_EtherRequestAntiSpamAPI : MonoBehaviour
{

    [Range(0.2f,2)]
    public float m_antiSpamTime=0.3f;
    public AntiEtherSpamRequest m_antiSpam = new AntiEtherSpamRequest();
    public bool m_useUpdateTForAntiSpam=true;

    public void AddFromUrl(string urlOfRequest, out PublicRestRequest request, List<PublicRestRequest> debugList = null)
    {
        request = new PublicRestRequestDefault(urlOfRequest);
        m_antiSpam.Add(request);
        if (debugList != null)
            debugList.Add(request);

    }

    public void AddRequest(PublicRestRequest request)
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
            PublicRestRequest tosend =   m_antiSpam.DequeueRquestToSend();
            StartCoroutine(tosend.SendRequest());
        }
    }

    private void OnValidate()
    {
        m_antiSpam.m_timeBetweenRequestSend = m_antiSpamTime;
    }

}



public class AntiEtherSpamRequest {

    public float m_timeBetweenRequestSend = 0.4f;
    public Queue<PublicRestRequest> m_toSend = new Queue<PublicRestRequest>();
    public DateTime m_lastSendRequest;

    public AntiEtherSpamRequest() {
        m_lastSendRequest = DateTime.Now;
    }

    public void Add(PublicRestRequest request) { m_toSend.Enqueue(request); }

    public bool HasRequest() { return m_toSend.Count > 0; }
    public bool HasTimepastEnough() { return (DateTime.Now - m_lastSendRequest).TotalSeconds > m_timeBetweenRequestSend; }
    public bool HasRequestAndTimepastEnough() { return HasRequest() && HasTimepastEnough(); }

    public PublicRestRequest DequeueRquestToSend() {
        if (m_toSend == null)
            return null;
        PublicRestRequest r = m_toSend.Dequeue();
        if (r != null) {
            r.SendRequest();
            m_lastSendRequest = DateTime.Now;
            return r;
        }
        return null;
    }
}




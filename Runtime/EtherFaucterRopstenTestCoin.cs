using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherFaucterRopstenTestCoin : MonoBehaviour
{

    public string m_serverUrl = "https://faucet.ropsten.be/donate/{0}";
    public string m_defaultAddress= "0x837677c0Fb5E83f40ACa10A96B1d58ea13F0EEf8";
    public PublicRestRequest m_request;
    public Experiment_EtherRequestAntiSpamAPI m_antiSpam;

    public void SendRequest(string address)
    {

        m_request = new PublicRestRequestDefault(string.Format(m_serverUrl,  m_defaultAddress));
        m_antiSpam.AddRequest(m_request);
    }
    [ContextMenu("Request Ether")]
    public void SendRequest()
    {
        SendRequest(m_defaultAddress);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GazPrice : MonoBehaviour
{
    public Text m_low;
    public Text m_medium;
    public Text m_hight;

    public Experiment_EtherRequestAntiSpamAPI m_etherScan;
    public EthScanRequest_GasOracle m_gazTracker;

    
    public void Refresh() {
        m_etherScan.AddRequest(m_gazTracker);
    }
}

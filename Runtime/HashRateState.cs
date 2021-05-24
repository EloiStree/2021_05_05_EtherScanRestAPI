using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashRateState
{
    [SerializeField] HashRateBean m_current;
    [SerializeField] HashRateBean m_average;
    [SerializeField] HashRateBean m_reported;

    public HashRateBean GetRealHashrate() { return m_current; }
    public HashRateBean GetAverageHashrate() { return m_average; }
    public HashRateBean GetHardwareReportHashrate() { return m_reported; }


    public void SetWith(double current, double average, double hardwareReport)
    {
        m_current.SetHashRate(current);
        m_average.SetHashRate(average);
        m_reported.SetHashRate(hardwareReport);
    }
}

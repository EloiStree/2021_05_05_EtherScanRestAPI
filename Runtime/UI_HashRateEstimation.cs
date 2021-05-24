using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HashRateEstimation : MonoBehaviour
{
    public Experiment_HardwareEstimationConverter m_estimationTool;
    public Text m_hardwaretypeLowDebug;
    public Text m_hardwaretypeUpDebug;
    public Text m_currentHashPerSecondDebug;
    
    public HashRateBean m_used;

    public void SetWith(double dispalyRate)
    {
        SetWith(new HashRateBean(dispalyRate));
    }
    public void SetWith(HashRateBean dispalyRate)
    {
        m_used = dispalyRate;
        m_estimationTool.m_converter.TryToFindEquivalent(dispalyRate,
            out string m_hardwaretypeLow, out string m_hardwaretypeUp);
        if (m_hardwaretypeLowDebug != null)
            m_hardwaretypeLowDebug.text = m_hardwaretypeLow;
        if (m_hardwaretypeUpDebug != null)
            m_hardwaretypeUpDebug.text = m_hardwaretypeUp;
        if (m_currentHashPerSecondDebug != null)
            m_currentHashPerSecondDebug.text = 
                HashRateConvertion.GetCloseTypeStringCompressionOf( dispalyRate);
    }

}

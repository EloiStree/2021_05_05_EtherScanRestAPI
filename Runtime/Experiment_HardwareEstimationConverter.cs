using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Experiment_HardwareEstimationConverter : MonoBehaviour
{
    public HashRateBean m_in;
    public string m_hardwaretypeUp;
    public string m_hardwaretypeLow;
    public Text m_hardwaretypeLowDebug;
    public Text m_hardwaretypeUpDebug;
    public Text m_currentHashPerSecondDebug;

    public HardwareEsitmationConverter m_converter;

    private void Awake()
    {
        m_converter.SortList();
    }
    private void OnValidate()
    {
        DisplayHashRateEstimationOfHardwarePower();
    }

    public void DisplayHashRateEstimationOfHardwarePower()
    {
        m_converter.TryToFindEquivalent(m_in, out m_hardwaretypeLow, out m_hardwaretypeUp);
        if (m_hardwaretypeLowDebug != null)
            m_hardwaretypeLowDebug.text = m_hardwaretypeLow;
        if (m_hardwaretypeUpDebug != null)
            m_hardwaretypeUpDebug.text = m_hardwaretypeUp;
        if (m_currentHashPerSecondDebug != null)
            m_currentHashPerSecondDebug.text = m_in.ToString();
    }
}

[System.Serializable]
public class HardwareEsitmationConverter {
    public List<OnlineEstimation> m_givenComparaison = new List<OnlineEstimation>();

    public void TryToFindEquivalent(HashRateBean hashRate, out string hardwareTypeBelow,out string hardwareTypeUpper)
    {
        hardwareTypeBelow = hardwareTypeUpper = "";
        
        for (int i = 1; i < m_givenComparaison.Count; i++)
        {
            if (HashRateBean.ALessThenB(hashRate, m_givenComparaison[i].m_hashType  ) )
            {
                hardwareTypeBelow = m_givenComparaison[i-1].m_labelOfTheHash;
                
                hardwareTypeUpper = m_givenComparaison[i].m_labelOfTheHash;
                return;
            }
        }
        hardwareTypeBelow = m_givenComparaison[m_givenComparaison.Count - 2].m_labelOfTheHash;
        hardwareTypeUpper = m_givenComparaison[m_givenComparaison.Count-1].m_labelOfTheHash;

    }

    public void SortList()
    {
        m_givenComparaison = m_givenComparaison.OrderBy(k => k.GetAsDecimalHash()).ToList();
    }
}

[System.Serializable]
public class OnlineEstimation
{

    public string m_labelOfTheHash;
    public HashRateBean m_hashType;

    public decimal GetAsDecimalHash()
    {
        m_hashType.GetAsHashPerSecond(out decimal value);
        return value;
    }
}
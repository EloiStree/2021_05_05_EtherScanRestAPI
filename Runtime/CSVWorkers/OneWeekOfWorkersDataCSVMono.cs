using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OneWeekOfWorkersDataCSVMono : MonoBehaviour
{
   
    public OneWeekOfWorkersDataCSV m_data = new OneWeekOfWorkersDataCSV();


    
    string m_shareCSVDebug;
    public ulong m_shareSizeUsed;
    
    string m_hashrateCSVDebug;
    public ulong m_hashrateSizeUsed;

    public void RefreshDebugStrings() {

        m_shareCSVDebug = m_data.m_oneWeekShares.GetAsCsvText();
        m_shareSizeUsed = m_data.m_oneWeekShares.GetMemorySizeUsed();
        m_hashrateCSVDebug = m_data.m_oneWeekHashrate.GetAsCsvText();
        m_hashrateSizeUsed = m_data.m_oneWeekHashrate.GetMemorySizeUsed();
    }

    public string GetShareCSVDebug() { return m_shareCSVDebug; }
    public string GetHashRateSizedDebug() { return m_hashrateCSVDebug; }
}

public class OneWeekOfWorkersDataCSV {
    public Csv1DArrayBufferableUint m_oneWeekShares = new Csv1DArrayBufferableUint();
    public Csv1DArrayBufferableDouble m_oneWeekHashrate = new Csv1DArrayBufferableDouble();

    /// 1024 column is the maxium
    public void AllocateSize(int worker, int column = 1000)
    {
        string[] columnName = new string[column];
        for (int i = 0; i < column; i++)
        {
            columnName[i] = i.ToString();
        }
        string[] workerName = new string[worker];
        for (int i = 0; i < worker; i++)
        {
            workerName[i] = i.ToString();
        }
        m_oneWeekShares.SetSize(columnName, workerName);
        m_oneWeekHashrate.SetSize(columnName, workerName);
    }


}


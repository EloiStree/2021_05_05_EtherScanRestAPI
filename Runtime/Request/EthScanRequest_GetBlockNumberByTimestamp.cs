using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class EthScanRequest_GetBlockNumberByTimestamp : PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public ulong m_currentBlockNumber;
    public EthScanRequest_GetBlockNumberByTimestamp(string apiToken) : base(EthScanUrl.GetBlockNumberByTimestamp(apiToken, "" + EthScanUrl.GetTimestamp() + 10, EthScanUrl.SideType.Before))
    {
    }



    protected override void NotifyToChildrenAsChanged()
    {
        //{"status":"1","message":"OK","result":{"ethbtc":"0.06149","ethbtc_timestamp":"1620179532","ethusd":"3370.85","ethusd_timestamp":"1620179533"}}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_currentBlockNumber = result.GetBlockNumberNow();
        }
        else isConverted = false;
    }

    public void LookForBlockNumberAfter(string apiToken, long secondsAdjustation)
    {
        LookForBlockNumber(apiToken, EthScanUrl.GetTimestamp(), secondsAdjustation, EthScanUrl.SideType.After);
    }
    public void LookForBlockNumberAfter(string apiToken, long timestamp, long secondsAdjustation)
    {
        LookForBlockNumber(apiToken, timestamp, secondsAdjustation, EthScanUrl.SideType.After);
    }
    public void LookForBlockNumberBefore(string apiToken, long secondsAdjustation)
    {
        LookForBlockNumber(apiToken, EthScanUrl.GetTimestamp(), secondsAdjustation, EthScanUrl.SideType.Before);
    }

    public ulong GetBlockNumber()
    {
        return m_currentBlockNumber;
    }

    public void LookForBlockNumberBefore(string apiToken, long timestamp, long secondsAdjustation )
    {
        LookForBlockNumber(apiToken, timestamp, secondsAdjustation, EthScanUrl.SideType.Before);
    }
    public void LookForBlockNumber(string apiToken, long timestamp, long secondsAdjustation ,  EthScanUrl.SideType side)
    {
       SetUrl( EthScanUrl.GetBlockNumberByTimestamp(apiToken, string.Format("{0:0}",(timestamp + secondsAdjustation)), side));
    }

    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public string result;
        public ulong GetBlockNumberNow()
        {
            ulong value = 0;
            ulong.TryParse(result, out value);
            return value;
        }
    }

}
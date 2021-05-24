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
    public EthScanRequest_GetBlockNumberByTimestamp(string apiToken) : base(EthScanUrl.GetBlockNumberByTimestamp(apiToken, "" + (((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() + 10)))
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
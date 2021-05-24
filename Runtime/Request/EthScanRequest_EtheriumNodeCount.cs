using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EthScanRequest_EtheriumNodeCount : PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public long m_nodeCount;
    public EthScanRequest_EtheriumNodeCount(string apiToken) : base(EthScanUrl.GetTotalNodesCount(apiToken))
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
        //{"status":"1","message":"OK","result":{"UTCDate":"2021-05-04","TotalNodeCount":"5609"}}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_nodeCount = result.result.GetNodeCount();
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_ResultInput result;
    }
    [System.Serializable]
    public class Json_ResultInput
    {
        public string UTCDate;
        public string TotalNodeCount;

        public long GetNodeCount()
        {
            long value = 0;
            long.TryParse(TotalNodeCount, out value);
            return value;
        }
    }
}

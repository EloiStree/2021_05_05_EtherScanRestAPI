using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class EthScanRequest_CheckContarctStatus : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public string m_status;
    public bool m_valide;
    public EthScanRequest_CheckContarctStatus(string apiToken, string transactionAddress) : base(EthScanUrl.CheckTransactionReceiptStatus(apiToken, transactionAddress))
    {
    }


    protected override void NotifyToChildrenAsChanged()
    {
        //{ "status":"1","message":"OK","result":{ "status":""} }
        //{"status":"1","message":"OK","result":{"isError":"0","errDescription":""}}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_valide = result.result.GetStatusValide();
            m_status = result.result.GetStatusState();
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_Status result;

    }
    [System.Serializable]
    public class Json_Status
    {
        public string status;
        public string isError;
        public string errDescription;
        public string GetStatusState()
        {
            return status;
        }
        public bool GetStatusValide()
        {
            if (status == null)
                return false;
            return status.Trim() == "1";
        }
    }

}
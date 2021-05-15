using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class EthScanRequest_GasOracle : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public long m_gazSafeInGWei;
    public long m_gazProposeInGWei;
    public long m_gazFastInGWei;
    public EthScanRequest_GasOracle(string apiToken) : base(EthScanUrl.GetGasOracle(apiToken))
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
        //{ "status":"1","message":"OK","result":{ "LastBlock":"12371850","SafeGasPrice":"30","ProposeGasPrice":"39","FastGasPrice":"45"} }
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_gazSafeInGWei = result.result.GetGasSafeInGwei();
            m_gazProposeInGWei = result.result.GetGasProposedInGwei();
            m_gazFastInGWei = result.result.GetGasFastInGwei();

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
        public string LastBlock;
        public string SafeGasPrice;
        public string FastGasPrice;
        public string ProposeGasPrice;

        public long GetGasSafeInGwei()
        {
            long value = 0;
            long.TryParse(SafeGasPrice, out value);
            return value;
        }
        public long GetGasFastInGwei()
        {
            long value = 0;
            long.TryParse(FastGasPrice, out value);
            return value;
        }
        public long GetGasProposedInGwei()
        {
            long value = 0;
            long.TryParse(ProposeGasPrice, out value);
            return value;
        }

    }
}
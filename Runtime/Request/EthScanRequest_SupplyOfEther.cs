using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class EthScanRequest_SupplyOfEther : PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public string m_wei;
    public ulong m_ether;
    public EthScanRequest_SupplyOfEther(string apiToken) : base(EthScanUrl.GetTotalSupplyOfEther(apiToken))
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
        //{"status":"1","message":"OK","result":"115742724936500000000000000"}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_ether = result.GetSupplyInEtherUlong();
            m_wei = result.GetSupplyInWei().ToString();
        }
        else isConverted = false;
    }


    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public string result;

        public decimal GetSupplyInWei()
        {
            decimal value = 0;
            decimal.TryParse(result, out value);
            return value;
        }
        public decimal GetSupplyInEther()
        {
            decimal value = 0;
            decimal.TryParse(result, out value);
            return value / 1000000000000000000;
        }
        public ulong GetSupplyInEtherUlong()
        {
            decimal value = 0;
            decimal.TryParse(result, out value);
            return (ulong)(value / 1000000000000000000);
        }
    }
    // public class Json_
}
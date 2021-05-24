using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EthScanRequest_GetBalance : PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public string m_walletAddressTarget;
    public string m_wei;
    public double m_ether;
    public EthScanRequest_GetBalance(string apiToken, string walletaddress) :
        base(EthScanUrl.GetEtherBalanceForSingleAddress(apiToken, walletaddress))
    {
        m_walletAddressTarget = walletaddress;
    }


    protected override void NotifyToChildrenAsChanged()
    {
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_wei = result.GetWei().ToString();
            m_ether = result.GetEther();
        }
        else isConverted = false;
    }

    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public string result;

        public decimal GetWei()
        {
            decimal value;
            decimal.TryParse(result, out value);
            return value;
        }
        public double GetEther()
        {
            decimal d = 0;
            decimal.TryParse(result, out d);
            return (double)(d / 1000000000000000000);
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EthScanRequest_ERC20TokenSupplyByContract : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public string contractAddress;
    public string m_balance;
    public EthScanRequest_ERC20TokenSupplyByContract(string apiToken, string contractAddress) :
        base(EthScanUrl.GetERC20TokenTotalSupplyByContractAddress(apiToken, contractAddress))
    {
        this.contractAddress = contractAddress;
    }


    protected override void NotifyToChildrenAsChanged()
    {
        // {"status":"1","message":"OK","result":"17041654363022"}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_balance = result.GetBalanceInWei().ToString();
        }

        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public string result;

        public decimal GetBalanceInWei()
        {
            return decimal.Parse(result);
        }
    }



}
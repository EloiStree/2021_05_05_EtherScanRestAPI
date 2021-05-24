using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class EthScanRequest_ERC20TokenAccountBalanceForTokenContractAddress : PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public string contractAddress;
    public string address;
    public string m_balance;
    public EthScanRequest_ERC20TokenAccountBalanceForTokenContractAddress(string apiToken, string contractAddress, string address) :
        base(EthScanUrl.GetERC20TokenAccountBalanceForTokenContractAddress(apiToken, contractAddress, address))
    {
        this.contractAddress = contractAddress;
        this.address = address;
    }


    protected override void NotifyToChildrenAsChanged()
    {
        //  { "status":"1","message":"OK","result":[{ "account":"0x91771a9f9d5a3215ea649bcc6525e6ff7f0f1bb5","balance":"98331791348000000"},{ "account":"0x9ec213d65fea207cc5c3940197a186ab7f08b946","balance":"4699245044180019796"},{ "account":"0x326642ffd33072e0158d78d7ddf501a9584c5d65","balance":"197016262000000000"},{ "account":"0x728a8ab23f29679d2aba7e63acd5144e42e91c81","balance":"488959599908406343"}]}
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
            decimal value;
            decimal.TryParse(result, out value);
            return value;
        }
    }

}

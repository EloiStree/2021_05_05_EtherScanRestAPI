using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EthScanRequest_GetBalanceOfWallets : PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public string[] m_addresses;
    public EthScanRequest_GetBalanceOfWallets(string apiToken, params string[] walletAddresses) :
        base(EthScanUrl.GetEtherBalanceForMultipleAddresses(apiToken, walletAddresses))
    {
        m_addresses = walletAddresses;
    }


    protected override void NotifyToChildrenAsChanged()
    {
        //  { "status":"1","message":"OK","result":[{ "account":"0x91771a9f9d5a3215ea649bcc6525e6ff7f0f1bb5","balance":"98331791348000000"},{ "account":"0x9ec213d65fea207cc5c3940197a186ab7f08b946","balance":"4699245044180019796"},{ "account":"0x326642ffd33072e0158d78d7ddf501a9584c5d65","balance":"197016262000000000"},{ "account":"0x728a8ab23f29679d2aba7e63acd5144e42e91c81","balance":"488959599908406343"}]}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_Account[] result;



    }

    [System.Serializable]
    public class Json_Account
    {
        public string account;
        public string balance;

        public decimal GetBlanceInWei() {
            decimal value;
            decimal.TryParse(balance, out value);
            return value;
        }
        public string GetAddress() { return account; }
    }

    public void GetWalletsInformation(out string[] wallets, out string[] balanceInWei)
    {
        wallets = new string[result.result.Length];
        balanceInWei = new string[result.result.Length];
        for (int i = 0; i < result.result.Length; i++)
        {
            wallets[i] = result.result[i].GetAddress();
            balanceInWei[i] = result.result[i].GetBlanceInWei().ToString();

        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class EthScanRequest_GetWalletTransaction : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public string m_walletAddress;
    public string m_receivedEhterium;

    //public string m_wei;
    //public ulong m_ether;
    public EthScanRequest_GetWalletTransaction(string apiToken, string walletaddress) :
        base(EthScanUrl.GetListOfClassicTransactionsByAddress(apiToken, walletaddress))
    {
        m_walletAddress = walletaddress;
    }

    protected override void NotifyToChildrenAsChanged()
    {
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_receivedEhterium = result.result.Where(k => k.GetToWallet().ToLower() == m_walletAddress.ToLower()).Sum(k => k.GetValueInWei()).ToString();
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_Transaction[] result;

    }

    [System.Serializable]
    public class Json_Transaction
    {

        //blockNumber : "12317243"
        public string blockNumber;
        public ulong GetBlockNumber() { 

            ulong value;
            ulong.TryParse(blockNumber, out value);
            return value;
        }
        //timeStamp : "1619457324"
        public string timeStamp;
        public ulong GetTimeStamp() { return ulong.Parse(timeStamp); }
        public DateTime GetDateTime() { return new DateTime(1970, 1, 1).AddSeconds(double.Parse(timeStamp)); }
        //hash : "0xe2f08cd9124a24f5d3d85b9296d581fd1b41d1d67dc0d99abec81d5025df8e42"
        public string hash;
        public string GetHash() { return hash; }
        //nonce : "0"
        public string nonce;
        public string GetNonce() { return nonce; }
        //blockHash : "0x80c037a0632d10d55d74bf715883c07e432a5597b71542cc8fac6d0141ab13c0"
        public string blockHash;
        public string GetBlockHash() { return blockHash; }
        //transactionIndex : "217"
        public string transactionIndex;
        public uint GetTransactionIndex() { return uint.Parse(transactionIndex); }
        //from : "0xd199ddfd38161df098fdc126c506a5d5cde961bb"
        public string from;
        public string GetFromWallet() { return from; }
        //to : "0xfeeacde5d735b8b347d9bbf8fbd02fed153b564a"
        public string to;
        public string GetToWallet() { return to; }
        //value : "19380000000000000"
        public string value;
        public decimal GetValueInWei() { return decimal.Parse(value); }
        //gas : "21000"
        public string gas;
        public ulong GetGasInWei() { return ulong.Parse(gas); }

        //gasPrice : "79000000000"
        public string gasPrice;
        public decimal GetGasPriceInWei() { return decimal.Parse(gasPrice); }

        //isError : "0"
        public string isError;
        public bool HadError() { return isError.Trim() != "0"; }
        //txreceipt_status : "1"
        public string txreceipt_status;
        public bool HadSucced() { return txreceipt_status.Trim() != "1"; }
        //input : "0x"
        public string input;
        public string GetMetaInput() { return input; }
        //contractAddress : ""
        public string contractAddress;
        public bool HasContractAddress() { return !string.IsNullOrWhiteSpace(contractAddress); }
        public string GetContractAddress() { return contractAddress; }

        //cumulativeGasUsed : "13067262"
        public string cumulativeGasUsed;
        public decimal GetCumulativeGasUsed() { return decimal.Parse(cumulativeGasUsed); }
        //gasUsed : "21000"
        public string gasUsed;
        public decimal GetGasUsed() { return decimal.Parse(gasUsed); }
        //confirmations : "54966"
        public string confirmations;
        public ulong GetNumberOfConfrimations()
        {
            return ulong.Parse(confirmations);
        }

    }

}

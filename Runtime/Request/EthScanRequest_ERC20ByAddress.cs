using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EthScanRequest_ERC20ByAddress : PublicRestRequest
{
    public bool isConverted;
    public string m_address;
    public Json_Result result;
    public EthScanRequest_ERC20ByAddress(string apiToken, string walletAddress) :
        base(EthScanUrl.GetListOfERC20(apiToken, walletAddress))
    {
        m_address = walletAddress;
    }


    protected override void NotifyToChildrenAsChanged()
    {

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
        public Json_ERC20[] result;

    }
    [System.Serializable]
    public class Json_ERC20
    {

        public string blockNumber;
        public ulong GetBlockNumber() { return ulong.Parse(blockNumber); }
        public string timeStamp;
        public ulong GetTimeStamp() { return ulong.Parse(timeStamp); }
        public DateTime GetDateTime() { return new DateTime(1970, 1, 1).AddSeconds(double.Parse(timeStamp)); }
        public string hash;
        public string GetHash() { return hash; }
        public string nonce;
        public string GetNonce() { return nonce; }
        public string blockHash;
        public string GetBlockHash() { return blockHash; }
        public string transactionIndex;
        public uint GetTransactionIndex() { return uint.Parse(transactionIndex); }
        public string from;
        public string GetFromWallet() { return from; }
        public string to;
        public string GetToWallet() { return to; }
        public decimal GetGasPriceInWei() { return decimal.Parse(gasPrice); }

        public string input;
        public string GetMetaInput() { return input; }
        public string contractAddress;
        public bool HasContractAddress() { return !string.IsNullOrWhiteSpace(contractAddress); }
        public string GetContractAddress() { return contractAddress; }
        public string cumulativeGasUsed;
        public decimal GetCumulativeGasUsed() { return decimal.Parse(cumulativeGasUsed); }
        public string confirmations;
        public ulong GetNumberOfConfrimations()
        {
            return ulong.Parse(confirmations);
        }
        public string gas;
        public ulong GetGasInWei() { return ulong.Parse(gas); }
        public string gasPrice;
        public decimal GetGasPrice() { return decimal.Parse(gasPrice); }
        public string gasUsed;
        public decimal GetGasUsed() { return decimal.Parse(gasUsed); }
        public string value;
        public decimal GetValue() { return decimal.Parse(value); }



        public string tokenID;
        public string GetTokenID() { return tokenID; }
        public string tokenName;
        public string GetTokenName() { return tokenName; }
        public string tokenSymbol;
        public string GetTokenSymbol() { return tokenSymbol; }
        public string tokenDecimal;
        public string GetTokenDecimal() { return tokenDecimal; }

    }

    //[System.Serializable]
    //public class Json_Transaction
    //{

    //    //value : "19380000000000000"
    //    public string value;
    //    public decimal GetValueInWei() { return decimal.Parse(value); }
    //    //isError : "0"
    //    public string isError;
    //    public bool HadError() { return isError.Trim() != "0"; }
    //    //txreceipt_status : "1"
    //    public string txreceipt_status;
    //    public bool HadSucced() { return txreceipt_status.Trim() != "1"; }




    //}




}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class EthScanRequest_GetWalletTransaction : PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public string m_walletAddress;
    public string m_receivedEhterium;
    private string m_startBlock;
    private string m_endBlock;

    //public string m_wei;
    //public ulong m_ether;
    public EthScanRequest_GetWalletTransaction(string apiToken, string walletaddress) :
        base(EthScanUrl.GetListOfClassicTransactionsByAddress(apiToken, walletaddress))
    {
        m_walletAddress = walletaddress;
    }

    public EthScanRequest_GetWalletTransaction(string apiToken, string walletaddress, string startBlock, string endBlock) : base(EthScanUrl.GetListOfClassicTransactionsByAddress(apiToken, walletaddress, startBlock, endBlock))
    {
        m_walletAddress = walletaddress;
        m_startBlock = startBlock;
        m_endBlock = endBlock;
    }

    public void SetWith(string apiToken, string walletaddress, string startBlock, string endBlock)
    {
        SetUrl( EthScanUrl.GetListOfClassicTransactionsByAddress(apiToken, walletaddress, startBlock, endBlock) );
        m_walletAddress = walletaddress;
        m_startBlock = startBlock;
        m_endBlock = endBlock;
    }

    protected override void NotifyToChildrenAsChanged()
    {
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_receivedEhterium = result.result.Where(k => k.GetToWallet().ToLower() == m_walletAddress.ToLower()).Sum(k => decimal.Parse(k.GetValueInWei())).ToString();
        }
        else isConverted = false;
    }
    public void GetTransactionInformation(out List<Json_Transaction> transactions)
    {
        if (!HasError() && HasText())
        {
            transactions = result.result.ToList();
        }
        else transactions = new List<Json_Transaction>();

    }

    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_Transaction[] result;

    }

    public void GetTransactionInformation(out string usedStartBlock, out string usedEndBlock, out TransactionFullInfoBean[] transactionsInfo)
    {
        usedStartBlock = m_startBlock;
        usedEndBlock = m_endBlock;
        GetTransactionInformation(out transactionsInfo);
    }

    public void GetTransactionInformation(out TransactionFullInfoBean[] transactionsInfo)
    {
        if (!HasError() && HasText())
        {
            Json_Transaction[] trans = result.result.ToArray();
            transactionsInfo = new TransactionFullInfoBean[trans.Length];
            for (int i = 0; i < trans.Length; i++)
            {
                transactionsInfo[i] = ConvertToTransactionFrom(trans[i]);

            }
        }
        else transactionsInfo = new TransactionFullInfoBean[0];

    }

    private TransactionFullInfoBean ConvertToTransactionFrom(Json_Transaction jtransaction)
    {
        TransactionFullInfoBean result = new TransactionFullInfoBean();

        result.m_timeStampInSecondsUTC = jtransaction.GetTimeStamp();
        result.transactionHash = jtransaction.GetTransactionHash();
        result.nonce = jtransaction.GetNonce();
        result.blockNumber = jtransaction.GetBlockNumber();
        result.blockHash = jtransaction.GetBlockHash();
        result.transactionIndexInBlock = jtransaction.GetTransactionIndex();
        result.m_fromAddress = jtransaction.GetFromWallet();
        result.m_toAddress = jtransaction.GetToWallet();
        result.m_weiValue = jtransaction.GetValueInWei();
        result.m_gasUsed = jtransaction.GetGasUsed();
        result.m_gasPriceInWei = jtransaction.GetGasPriceInWei();
        result.m_gasValueInWei = jtransaction.GetGasInWei();
        result.m_cumulativeGasUsed = jtransaction.GetCumulativeGasUsed();
        result.SetTransactionReceiptStatus(jtransaction.GetStatusState());
        result.m_isEvmError = jtransaction.HadError();
        result.m_metaInput = jtransaction.GetMetaInput();
        result.contractAddress = jtransaction.GetContractAddress();
        result.confirmations = jtransaction.GetNumberOfConfrimations();

        return result;
    }




        public string GetStartBlock()
    {
        return m_startBlock;
    }

    public string GetEndBlock()
    {
        return m_endBlock;
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
        public string GetTransactionHash() { return hash; }
        //nonce : "0"
        public string nonce;
        public uint GetNonce() { return uint.Parse(nonce); }
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
        public string GetValueInWei() { return value; }
   
        //isError : "0"
        public string isError;
        public bool HadError() { return isError.Trim() != "0"; }
        //txreceipt_status : "1"
        public string txreceipt_status;
        public bool GetStatusState() { return txreceipt_status.Trim() == "1"; }
        //input : "0x"
        public string input;
        public string GetMetaInput() { return input; }
        //contractAddress : ""
        public string contractAddress;
        public bool HasContractAddress() { return !string.IsNullOrWhiteSpace(contractAddress); }
        public string GetContractAddress() { return contractAddress; }

        //gas : "21000"
        public string gas;
        public ulong GetGasInWei() { return ulong.Parse(gas); }
        //gasPrice : "79000000000"
        public string gasPrice;
        public ulong GetGasPriceInWei() { return ulong.Parse(gasPrice); }
        
        //cumulativeGasUsed : "13067262"
        public string cumulativeGasUsed;
        public decimal GetCumulativeGasUsed() { return decimal.Parse(cumulativeGasUsed); }
        //gasUsed : "21000"
        public string gasUsed;
        public ulong GetGasUsed() { return ulong.Parse(gasUsed); }
        //confirmations : "54966"
        public string confirmations;
        public ulong GetNumberOfConfrimations()
        {
            return ulong.Parse(confirmations);
        }

    }

}

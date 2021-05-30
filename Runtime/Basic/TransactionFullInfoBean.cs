using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TransactionFullInfoBean 
{
    //blockNumber : "12317243"
    public ulong blockNumber;
    public ulong GetBlockNumber()
    {
        return blockNumber;
    }
    //timeStamp : "1619457324" blockNumber
    public ulong m_timeStampInSecondsUTC;
    public ulong GetTimeStamp() { return m_timeStampInSecondsUTC; }
    public DateTime GetDateTime() { return new DateTime(1970, 1, 1).AddSeconds(m_timeStampInSecondsUTC); }
    //hash : "0xe2f08cd9124a24f5d3d85b9296d581fd1b41d1d67dc0d99abec81d5025df8e42"
    public string transactionHash;
    public string GetTransactionIdHash() { return transactionHash; }
    //nonce : "0"
    public uint nonce;
    public uint GetNonce() { return nonce; }
    //blockHash : "0x80c037a0632d10d55d74bf715883c07e432a5597b71542cc8fac6d0141ab13c0"
    public string blockHash;
    public string GetBlockHash() { return blockHash; }
    //transactionIndex : "217"
    public uint transactionIndexInBlock;
    public uint GetTransactionIndex() { return transactionIndexInBlock; }
    //from : "0xd199ddfd38161df098fdc126c506a5d5cde961bb"
    public string m_fromAddress;
    public string GetFromWallet() { return m_fromAddress; }
    //to : "0xfeeacde5d735b8b347d9bbf8fbd02fed153b564a"
    public string m_toAddress;
    public string GetToWallet() { return m_toAddress; }
    //value : "19380000000000000"
    public string m_weiValue;
    public decimal GetValueInWei() { return decimal.Parse(m_weiValue); }
    //gas : "21000"
    public ulong m_gasValueInWei;
    public ulong GetGasInWei() { return(m_gasValueInWei); }


   
    //gasPrice : "79000000000"
    public ulong m_gasPriceInWei;
    public ulong GetGasPriceInWei() { return (m_gasPriceInWei); }

    //isError : "0"
    public bool m_isEvmError;
    public bool HadError() { return m_isEvmError; }
    //txreceipt_status : "1"
    public bool m_txreceipt_status;
    public void SetTransactionReceiptStatus(bool statusIsTrue) {
        m_txreceipt_status = statusIsTrue;
    }
    public bool GetTransactionStatusSucced() { return m_txreceipt_status; }
    //input : "0x"
    public string m_metaInput;
    public string GetMetaInput() { return m_metaInput; }
    //contractAddress : ""
    public string contractAddress;
    public bool HasContractAddress() { return !string.IsNullOrWhiteSpace(contractAddress); }
    public string GetContractAddress() { return contractAddress; }

    //cumulativeGasUsed : "13067262"
    public ulong cumulativeGasUsed;
    public ulong GetCumulativeGasUsed() { return cumulativeGasUsed; }
    //gasUsed : "21000"
    public ulong m_gasUsed;
    public ulong GetGasUsed() { return m_gasUsed; }
    //confirmations : "54966"
    public ulong confirmations;
    internal decimal m_cumulativeGasUsed;

    public ulong GetNumberOfConfrimations()
    {
        return confirmations;
    }

    public string GetOneLiner(char spliter)
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} | {12} | {13} | {14} | {15} | {16} | {17} | {18}",
            blockNumber, m_timeStampInSecondsUTC, transactionHash, nonce, blockHash, transactionIndexInBlock,
            m_fromAddress, m_toAddress, m_weiValue, m_gasValueInWei, m_gasPriceInWei, m_isEvmError,
            m_txreceipt_status, m_metaInput, contractAddress, cumulativeGasUsed, m_gasUsed, confirmations, m_cumulativeGasUsed).Replace('|', spliter);

    }

    public double GetValueInEth()
    {
        decimal.TryParse(m_weiValue, out decimal result);
        EthereumConverttion.ApproximateConvert(result, out decimal newValue, EtherType.Wei, EtherType.Ether);
        return (double) newValue;
    }
}


[System.Serializable]
public class WalletTransactionsHistoryRange
{
    [SerializeField] TransactionCursor m_start;
    [SerializeField] TransactionCursor m_end;
    [SerializeField] TransactionFullInfoBean[] m_transactions;
    public void SetWith()
    {
        m_start = new TransactionCursor("", "");
        m_end = new TransactionCursor("", "");
        m_transactions = new TransactionFullInfoBean[0];
    }
    public void SetWith(string giveStartBlock, string givenEndBlock, TransactionFullInfoBean[] transactionsFound)
    {
        m_start = new TransactionCursor(giveStartBlock, "");
        m_end = new TransactionCursor(givenEndBlock, "");
        m_transactions = transactionsFound;
    }
    public uint GetTransactionsCount() { return (uint) m_transactions.Length; }
    public ITransactionId GetStart() { return m_start; }
    public ITransactionId GetEnd() { return m_end; }
    public TransactionFullInfoBean[] GetTransactionsOrderedFromPastToRecent() { return m_transactions; }
    public TransactionFullInfoBean[] GetTransactionsOrderedFromPastToRecent(uint lastCount)
    {
        TransactionFullInfoBean[] result = new TransactionFullInfoBean[lastCount];
        int transactionCount = m_transactions.Length;
        if (transactionCount <= lastCount)
            return result;
        for (int i = 0; i < lastCount; i++)
        {
            //TODO: not checked yet
            result[i] = m_transactions[transactionCount - (lastCount + i)];
        }
        return m_transactions;
    }
    public string[] GetTransactionsHashOrderedFromPastToRecent()
    {
        return GetTransactionsOrderedFromPastToRecent().Select(k => k.GetTransactionIdHash()).ToArray();
    }
    public TransactionFullInfoBean GetTransactionHashPastToRecentAt(uint index)
    {
        return m_transactions[index];

    }
    public TransactionFullInfoBean GetTransactionHashRecentToPastAt(uint index)
    {
        return m_transactions[m_transactions.Length-1-index];
    }

    public TransactionFullInfoBean[] GetTransactionHashRecentToPastCount(uint count)
    {
        if (count > m_transactions.Length)
            count = (uint)m_transactions.Length;
        TransactionFullInfoBean[] result = new TransactionFullInfoBean[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = GetTransactionHashRecentToPastAt(i);
        }
        return result;

    }
    public TransactionFullInfoBean[] GetTransactionHashPastToRecentCount(uint count)
    {
        if (count > m_transactions.Length)
            count = (uint)m_transactions.Length;
        TransactionFullInfoBean[] result = new TransactionFullInfoBean[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = GetTransactionHashPastToRecentAt(i);
        }
        return result;

    }



}
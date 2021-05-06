using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EthScanRequest_EstimationOfConfirmationTIme : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public double m_gazProposeEstimateTimeInSeconds;
    public EthScanRequest_EstimationOfConfirmationTIme(string apiToken, string gazInWei) : base(EthScanUrl.GetEstimationOfConfirmationTime(apiToken,gazInWei))
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
        //{ "status":"1","message":"OK","result":"975"}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_gazProposeEstimateTimeInSeconds = result.GetSecondsEstimation();
          
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public string result;

        public uint GetSecondsEstimation() {
            uint value = 0;
            uint.TryParse(result, out value);
            return value;
        } 
    }
}




[System.Serializable]
public class EthScanRequest_GetBlockNumberByTimestamp : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public ulong m_currentBlockNumber;
    public EthScanRequest_GetBlockNumberByTimestamp(string apiToken) : base(EthScanUrl.GetBlockNumberByTimestamp(apiToken, ""+(((DateTimeOffset) DateTime.Now).ToUnixTimeSeconds()+10)))
    {
    }
    

    protected override void NotifyToChildrenAsChanged()
    {
        //{"status":"1","message":"OK","result":{"ethbtc":"0.06149","ethbtc_timestamp":"1620179532","ethusd":"3370.85","ethusd_timestamp":"1620179533"}}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_currentBlockNumber = result.GetBlockNumberNow();
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public string result;
        public ulong GetBlockNumberNow()
        {
            ulong value = 0;
            ulong.TryParse(result, out value);
            return value;
        }
    }
   
}


[System.Serializable]
public class EthScanRequest_CheckContarctStatus : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public string m_status;
    public bool m_valide;
    public EthScanRequest_CheckContarctStatus(string apiToken, string transactionAddress) : base(EthScanUrl.CheckTransactionReceiptStatus(apiToken, transactionAddress))
    {
    }


    protected override void NotifyToChildrenAsChanged()
    {
        //{ "status":"1","message":"OK","result":{ "status":""} }
        //{"status":"1","message":"OK","result":{"isError":"0","errDescription":""}}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_valide = result.result.GetStatusValide();
            m_status = result.result.GetStatusState();
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_Status result;
      
    }
    [System.Serializable]
    public class Json_Status
    {
        public string status;
        public string isError;
        public string errDescription;
        public string GetStatusState()
        {
            return status;
        }
        public bool GetStatusValide()
        {
            if (status == null)
                return false;
            return status.Trim() == "1";
        }
    }

}



[System.Serializable]
public class EthScanRequest_GetBalance : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public string m_wei;
    public double m_ether;
    public EthScanRequest_GetBalance(string apiToken, string walletaddress) :
        base(EthScanUrl.GetEtherBalanceForSingleAddress(apiToken, walletaddress))
    {
    }


    protected override void NotifyToChildrenAsChanged()
    {
          if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_wei = result.GetWei();
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

        public string GetWei()
        {
            return result;
        }
        public double GetEther()
        {
            decimal d = 0;
            decimal.TryParse(result, out d);
            return (double) (d/ 1000000000000000000);
        }

    }
   
}


[System.Serializable]
public class EthScanRequest_GetWalletTransaction: EtherScanRequest
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
            m_receivedEhterium = result.result.Where(k => k.GetToWallet().ToLower()== m_walletAddress.ToLower()).Sum(k => k.GetValueInWei()).ToString();
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_Transaction [] result;

    }

    [System.Serializable]
    public class Json_Transaction {
        
        //blockNumber : "12317243"
        public string blockNumber;
        public ulong GetBlockNumber() { return ulong.Parse(blockNumber); }
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
        public ulong GetNumberOfConfrimations() {
            return ulong.Parse(confirmations);
        }

    }

}


[System.Serializable]
public class EthScanRequest_GetBalanceOfWallets : EtherScanRequest
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

        public decimal GetBlanceInWei() { return decimal.Parse(balance); }
        public string GetAddress() { return account; }
    }

}



[System.Serializable]
public class EthScanRequest_ERC20TokenSupplyByContract : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public string contractAddress;
    public string m_balance;
    public EthScanRequest_ERC20TokenSupplyByContract(string apiToken,  string contractAddress) :
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
[System.Serializable]
public class EthScanRequest_ERC20TokenAccountBalanceForTokenContractAddress : EtherScanRequest
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

        public decimal GetBalanceInWei() {
            return decimal.Parse(result);
        }
    }

}




[System.Serializable]
public class EthScanRequest_ERC20ByAddress : EtherScanRequest
{
    public bool isConverted;
    public string m_address;
    public Json_Result result;
    public EthScanRequest_ERC20ByAddress(string apiToken,  string walletAddress) :
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
        public Json_ERC20 [] result;

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

[System.Serializable]
public class EthScanRequest_ERC721ByAddress : EtherScanRequest
{
    public bool isConverted;
    public string m_address;
    public Json_Result result;
    public EthScanRequest_ERC721ByAddress(string apiToken, string walletAddress) :
        base(EthScanUrl.GetListOfERC721(apiToken, walletAddress))
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
        public Json_ERC721 [] result;
    }

    [System.Serializable]
    public class Json_ERC721
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
        public string gas;
        public ulong GetGasInWei() { return ulong.Parse(gas); }
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
        public string gasPrice;
        public decimal GetGasPrice() { return decimal.Parse(gasPrice); }



        public string tokenID;
        public string GetTokenID() { return tokenID; }
        public string tokenName;
        public string GetTokenName() { return tokenName; }
        public string tokenSymbol;
        public string GetTokenSymbol() { return tokenSymbol; }
        public string tokenDecimal;
        public string GetTokenDecimal() { return tokenDecimal; }

    }

}

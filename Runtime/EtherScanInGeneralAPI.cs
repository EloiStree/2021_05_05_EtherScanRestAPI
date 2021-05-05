using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherScanInGeneralAPI : MonoBehaviour
{
    public string m_etherScanApiToken;
    public Experiment_EtherScanAPI m_requestSender;
    public EthScanRequest_SupplyOfEther m_etherSupply;
    public EthScanRequest_EtherLastPrice m_etherLastPrice;
    public EthScanRequest_EtheriumNodeCount m_etherNodeCount;
    public EthScanRequest_GasOracle m_etherGasOracle;
    public EthScanRequest_GetBlockNumberByTimestamp m_etherBlocks;

    public EthScanRequest_EstimationOfConfirmationTIme m_ehterGasEstimationTime;

    public string m_targetTransactionFail;
    public EthScanRequest_CheckContarctStatus m_transactionStateFail;
    public string m_targetTransactionValide;
    public EthScanRequest_CheckContarctStatus m_transactionStateValide;


    public string m_addressWallet;
    public EthScanRequest_GetBalance m_walletBalance;
    public EthScanRequest_GetWalletTransaction m_walletTransaction;

    // Start is called before the first frame update
    IEnumerator Start()
    {
         m_etherSupply= new EthScanRequest_SupplyOfEther(m_etherScanApiToken);
        m_requestSender.AddRequest(m_etherSupply);

        m_etherLastPrice = new EthScanRequest_EtherLastPrice(m_etherScanApiToken);
        m_requestSender.AddRequest(m_etherLastPrice);

        m_etherNodeCount = new EthScanRequest_EtheriumNodeCount(m_etherScanApiToken);
        m_requestSender.AddRequest(m_etherNodeCount);

        m_etherGasOracle = new EthScanRequest_GasOracle(m_etherScanApiToken);
        m_requestSender.AddRequest(m_etherGasOracle);

        m_etherBlocks = new EthScanRequest_GetBlockNumberByTimestamp(m_etherScanApiToken);
        m_requestSender.AddRequest(m_etherBlocks);

        m_transactionStateFail = new EthScanRequest_CheckContarctStatus(m_etherScanApiToken, m_targetTransactionFail);
        m_requestSender.AddRequest(m_transactionStateFail);

        m_transactionStateValide = new EthScanRequest_CheckContarctStatus(m_etherScanApiToken, m_targetTransactionValide);
        m_requestSender.AddRequest(m_transactionStateValide);

        m_walletBalance = new EthScanRequest_GetBalance(m_etherScanApiToken, m_addressWallet);
        m_requestSender.AddRequest(m_walletBalance);
        m_walletTransaction = new EthScanRequest_GetWalletTransaction(m_etherScanApiToken, m_addressWallet);
        m_requestSender.AddRequest(m_walletTransaction);




        yield return new WaitForSeconds(10);
        m_ehterGasEstimationTime = new EthScanRequest_EstimationOfConfirmationTIme(m_etherScanApiToken, m_etherGasOracle.m_gazProposeInGWei+ "000000000");
        m_requestSender.AddRequest(m_ehterGasEstimationTime);
    }

  
}

[System.Serializable]
public class EthScanRequest_SupplyOfEther : EtherScanRequest
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
    public class Json_Result{
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
            return (ulong)( value / 1000000000000000000 );
        }
    }
    // public class Json_
}
[System.Serializable]
public class EthScanRequest_EtherLastPrice : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public double m_ehterVsBitcoin;
    public double m_ehterVsDollard;
    public EthScanRequest_EtherLastPrice(string apiToken) : base(EthScanUrl.GetETHERLastPrice(apiToken))
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
        //{"status":"1","message":"OK","result":{"ethbtc":"0.06149","ethbtc_timestamp":"1620179532","ethusd":"3370.85","ethusd_timestamp":"1620179533"}}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_ehterVsBitcoin = result.result.GetEtherAsBitcoinValue();
            m_ehterVsDollard = result.result.GetEtherAsDollarValue();
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_ResultInput result;
    }
    [System.Serializable]
    public class Json_ResultInput
    {
        public string ethbtc;
        public string ethbtc_timestamp;
        public string ethusd;
        public string ethusd_timestamp;

        public double GetEtherAsBitcoinValue()
        {
            double value = 0;
            double.TryParse(ethbtc, out value);
            return value;
        }
        public double GetEtherAsDollarValue()
        {
            double value = 0;
            double.TryParse(ethusd, out value);
            return value;
        }
    }
    // public class Json_
}


[System.Serializable]
public class EthScanRequest_EtheriumNodeCount : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public long m_nodeCount;
    public EthScanRequest_EtheriumNodeCount(string apiToken) : base(EthScanUrl.GetTotalNodesCount(apiToken))
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
        //{"status":"1","message":"OK","result":{"UTCDate":"2021-05-04","TotalNodeCount":"5609"}}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_nodeCount = result.result.GetNodeCount();
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_ResultInput result;
    }
    [System.Serializable]
    public class Json_ResultInput
    {
        public string UTCDate;
        public string TotalNodeCount;

        public long GetNodeCount()
        {
            long value = 0;
            long.TryParse(TotalNodeCount, out value);
            return value;
        }
    }
}


[System.Serializable]
public class EthScanRequest_GasOracle : EtherScanRequest
{
    public bool isConverted;
    public Json_Result result;
    public long m_gazSafeInGWei;
    public long m_gazProposeInGWei;
    public long m_gazFastInGWei;
    public EthScanRequest_GasOracle(string apiToken) : base(EthScanUrl.GetGasOracle(apiToken))
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
        //{ "status":"1","message":"OK","result":{ "LastBlock":"12371850","SafeGasPrice":"30","ProposeGasPrice":"39","FastGasPrice":"45"} }
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_gazSafeInGWei = result.result.GetGasSafeInGwei();
            m_gazProposeInGWei = result.result.GetGasProposedInGwei();
            m_gazFastInGWei = result.result.GetGasFastInGwei();

        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_ResultInput result;
    }
    [System.Serializable]
    public class Json_ResultInput
    {
        public string LastBlock;
        public string SafeGasPrice;
        public string FastGasPrice;
        public string ProposeGasPrice;

        public long GetGasSafeInGwei()
        {
            long value = 0;
            long.TryParse(SafeGasPrice, out value);
            return value;
        }
        public long GetGasFastInGwei()
        {
            long value = 0;
            long.TryParse(FastGasPrice, out value);
            return value;
        }
        public long GetGasProposedInGwei()
        {
            long value = 0;
            long.TryParse(ProposeGasPrice, out value);
            return value;
        }
        
    }
}
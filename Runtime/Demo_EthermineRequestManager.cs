using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Demo_EthermineRequestManager : MonoBehaviour
{
    public Experiment_EtherRequestAntiSpamAPI m_requestSender;

    [Header("Address")]
    public string m_addressObserved;
    public string m_farmerChinoisAddress = "c319936AbB7E9e14c834db4a79f5Ff79B15189a1";
    public EthMineRequest_Workers m_workers;
    public EthMineRequest_MinerCurrentStats m_minerCurrentStats;

    [Header("Workers")]
    public string m_workerName;
    public EthMineRequest_WorkersStatistics m_workerStats;
    public EthMineRequest_WorkersHistory m_workerHisory;


    [Header("Website")]
    public EthMineRequest_WebsitePoolStatistics     m_webstats;
    public EthMineRequest_WebsiteServersHistory     m_webServersHistory;
    public EthMineRequest_WebsiteNetworkStatistics  m_webNetwork;
    public EthMineRequest_WebsiteBlocksStatistics   m_webBlocks;
    //public EthMineRequest_WorkersMonitors m_workerMonitors;


    private void Awake()
    {
        m_workers = new EthMineRequest_Workers(m_addressObserved);
        m_minerCurrentStats = new EthMineRequest_MinerCurrentStats(m_addressObserved);
        m_workerStats = new EthMineRequest_WorkersStatistics(m_addressObserved, m_workerName);
        m_workerHisory = new EthMineRequest_WorkersHistory(m_addressObserved, m_workerName);

        m_webstats = new EthMineRequest_WebsitePoolStatistics();
        m_webServersHistory = new EthMineRequest_WebsiteServersHistory();
        m_webNetwork= new EthMineRequest_WebsiteNetworkStatistics();
        m_webBlocks = new EthMineRequest_WebsiteBlocksStatistics();

        //Don't know why I can't access those information onthey website
        //m_workerMonitors = new EthMineRequest_WorkersMonitors(m_addressObserved, m_workerName);
        // AddListenToAndPush(m_workerMonitors);
        AddListenToAndPush(m_workers);
        AddListenToAndPush(m_minerCurrentStats);
        AddListenToAndPush(m_workerStats);
        AddListenToAndPush(m_workerHisory);
        AddListenToAndPush(m_webstats);
        AddListenToAndPush(m_webServersHistory);
        AddListenToAndPush(m_webNetwork);
        AddListenToAndPush(m_webBlocks);


    }

    //public EthScanRequest_EtherLastPrice m_etherLastPrice;
    //public EthScanRequest_EtheriumNodeCount m_etherNodeCount;
    //public EthScanRequest_GasOracle m_etherGasOracle;
    //public EthScanRequest_GetBlockNumberByTimestamp m_etherBlocks;

    //public EthScanRequest_EstimationOfConfirmationTIme m_ehterGasEstimationTime;

    //public string m_targetTransactionFail;
    //public EthScanRequest_CheckContarctStatus m_transactionStateFail;
    //public string m_targetTransactionValide;
    //public EthScanRequest_CheckContarctStatus m_transactionStateValide;


    //public string m_addressWallet;
    //public EthScanRequest_GetBalance m_walletBalance;
    //public EthScanRequest_GetWalletTransaction m_walletTransaction;
    //public EthScanRequest_ERC20ByAddress m_walletErc20;
    //public EthScanRequest_ERC721ByAddress m_walletErc721;

    //public string[] m_address20Max = new string[20];
    //public EthScanRequest_GetBalanceOfWallets m_etherWallets;


    //public string ercContractAddress;
    //public string ercContractLinkedAddress;
    //public EthScanRequest_ERC20TokenSupplyByContract m_contact;
    //public EthScanRequest_ERC20TokenAccountBalanceForTokenContractAddress m_linkedContact;


    //public SupplyOfEtherChange supplyOfEtherListener;
    //public EtherLastPriceChange lastPriceListener;
    //public EtherNodeCount nodeCountListener;
    //public EhterCurrentBlockNumber blockNumberListener;
    //public EtherGasOracle gasOracleListener;
    //public WalletBalanceState walletStateListener;

    //public delegate void SupplyOfEtherChange(decimal supplyOfEther);
    //public delegate void EtherLastPriceChange(double lastPriceInBitcont, double lastPriceInDollar);
    //public delegate void EtherNodeCount(decimal nodeCount);
    //public delegate void EhterCurrentBlockNumber(decimal currentBlockNumber);
    //public delegate void EtherGasOracle(decimal safeGWei, decimal proposedInGwei, decimal fastInGwei);
    //public delegate void WalletBalanceState(string address, string weiState);



    //IEnumerator Start()
    //{
    //    m_etherSupply = new EthScanRequest_SupplyOfEther(m_etherScanApiToken);
    //    AddListenToAndPush(m_etherSupply);
    //    m_etherLastPrice = new EthScanRequest_EtherLastPrice(m_etherScanApiToken);
    //    AddListenToAndPush(m_etherLastPrice);
    //    m_etherNodeCount = new EthScanRequest_EtheriumNodeCount(m_etherScanApiToken);
    //    AddListenToAndPush(m_etherNodeCount);
    //    m_etherGasOracle = new EthScanRequest_GasOracle(m_etherScanApiToken);
    //    AddListenToAndPush(m_etherGasOracle);
    //    m_etherBlocks = new EthScanRequest_GetBlockNumberByTimestamp(m_etherScanApiToken);
    //    AddListenToAndPush(m_etherBlocks);

    //    CheckWalletState(m_addressWallet);
    //    CheckWalletStateMax20Accounts(m_address20Max);
    //    CheckWalletStateMoreThat20Accounts(m_address20Max);


    //    m_transactionStateFail = new EthScanRequest_CheckContarctStatus(m_etherScanApiToken, m_targetTransactionFail);
    //    AddListenToAndPush(m_transactionStateFail);
    //    m_transactionStateValide = new EthScanRequest_CheckContarctStatus(m_etherScanApiToken, m_targetTransactionValide);
    //    AddListenToAndPush(m_transactionStateValide);




    //    m_walletErc20 = new EthScanRequest_ERC20ByAddress(m_etherScanApiToken, m_addressWallet);
    //    AddListenToAndPush(m_walletErc20);
    //    m_walletErc721 = new EthScanRequest_ERC721ByAddress(m_etherScanApiToken, m_addressWallet);
    //    AddListenToAndPush(m_walletErc721);

    //    m_walletTransaction = new EthScanRequest_GetWalletTransaction(m_etherScanApiToken, m_addressWallet);
    //    AddListenToAndPush(m_walletTransaction);




    //    yield return new WaitForSeconds(10);


    //    m_ehterGasEstimationTime = new EthScanRequest_EstimationOfConfirmationTIme(m_etherScanApiToken, 80 + "000000000");
    //    CheckTransactionTimeForInGwei(m_etherGasOracle.m_gazProposeInGWei.ToString());


    //    m_contact = new EthScanRequest_ERC20TokenSupplyByContract(m_etherScanApiToken, ercContractAddress);
    //    AddListenToAndPush(m_etherWallets);
    //    m_linkedContact = new EthScanRequest_ERC20TokenAccountBalanceForTokenContractAddress(m_etherScanApiToken, ercContractAddress, ercContractLinkedAddress);
    //    AddListenToAndPush(m_etherWallets);


    //}
    //public void CheckTransactionTimeForInGwei(string gwei)
    //{
    //    AddListenToAndPush(new EthScanRequest_EstimationOfConfirmationTIme(m_etherScanApiToken, gwei + "000000000"));
    //}
    //public void CheckTransactionTimeForInWei(string wei)
    //{
    //    AddListenToAndPush(new EthScanRequest_EstimationOfConfirmationTIme(m_etherScanApiToken, wei));
    //}



    //public void CheckERC20(string address)
    //{
    //    AddListenToAndPush(new EthScanRequest_ERC20ByAddress(m_etherScanApiToken, m_addressWallet));
    //}
    //public void CheckERC721(string address)
    //{
    //    AddListenToAndPush(new EthScanRequest_ERC721ByAddress(m_etherScanApiToken, m_addressWallet));
    //}
    //public void CheckTransaction(string address)
    //{
    //    AddListenToAndPush(new EthScanRequest_GetWalletTransaction(m_etherScanApiToken, m_addressWallet));
    //}








    //public void CheckWalletState(string walletAddress)
    //{
    //    m_walletBalance = new EthScanRequest_GetBalance(m_etherScanApiToken, walletAddress);
    //    AddListenToAndPush(m_walletBalance);
    //}
    //public void CheckWalletStateMax20Accounts(params string[] walletAddress)
    //{
    //    m_etherWallets = new EthScanRequest_GetBalanceOfWallets(m_etherScanApiToken, walletAddress);
    //    AddListenToAndPush(m_etherWallets);

    //}
    //public void CheckWalletStateMoreThat20Accounts(params string[] walletAddress)
    //{
    //    int i = 0;
    //    List<string> addressStack = new List<string>();
    //    while (i < walletAddress.Length)
    //    {
    //        addressStack.Add(walletAddress[i]);
    //        if (addressStack.Count == 20)
    //        {
    //            AddListenToAndPush(new EthScanRequest_GetBalanceOfWallets(m_etherScanApiToken, addressStack.ToArray()));
    //            addressStack.Clear();
    //        }
    //        if (addressStack.Count > 20)
    //        {
    //            Debug.LogError("Big problem here. Contact developer, the spliting of address by group of 20 did not work  properly.");
    //        }

    //        i++;
    //    }
    //    if (addressStack.Count > 0)
    //    {
    //        AddListenToAndPush(new EthScanRequest_GetBalanceOfWallets(m_etherScanApiToken, addressStack.ToArray()));
    //        addressStack.Clear();
    //    }
    //}

    private void AddListenerTo(PublicRestRequest request)
    {
        request.AddListener(ManageRequestCallback);
    }
    private void AddListenToAndPush(PublicRestRequest request)
    {
        request.AddListener(ManageRequestCallback);
        m_requestSender.AddRequest(request);
    }

    private void ManageRequestCallback(PublicRestRequest requestReceived)
    {
        if (requestReceived is EthMineRequest_Workers)
        {
            WorkerRequestCastedCallback((EthMineRequest_Workers)requestReceived);
        }
      
        else
        {
            if(m_debugUnknowRequest)
                Debug.Log("Received unable to cast:" + m_requestSender.ToString());
        }

    }

    public bool m_debugUnknowRequest;
   private void WorkerRequestCastedCallback(EthMineRequest_Workers requestReceived)
   {
       Debug.Log(">" + requestReceived.ToString());
   }
   
    //private void ManageRequestCastedCallback(EthScanRequest_GetBalanceOfWallets requestReceived)
    //{
    //    Debug.Log(">" + requestReceived.ToString());
    //    if (walletStateListener != null)
    //    {
    //        string[] wallets, balanceInWei;
    //        requestReceived.GetWalletsInformation(out wallets, out balanceInWei);
    //        for (int i = 0; i < wallets.Length; i++)
    //        {
    //            if (!string.IsNullOrEmpty(wallets[i]) && !string.IsNullOrEmpty(balanceInWei[i]))
    //            {
    //                walletStateListener(wallets[i], balanceInWei[i]);
    //            }
    //        }
    //    }
    //}

   

    //private void ManageRequestCastedCallback(EthScanRequest_GasOracle requestReceived)
    //{
    //    Debug.Log("Gaz safe:" + requestReceived.m_gazSafeInGWei);
    //    Debug.Log("Gaz middle:" + requestReceived.m_gazProposeInGWei);
    //    Debug.Log("Gaz fast:" + requestReceived.m_gazFastInGWei);

    //    if (gasOracleListener != null)
    //        gasOracleListener(requestReceived.m_gazSafeInGWei, requestReceived.m_gazProposeInGWei, requestReceived.m_gazFastInGWei);
    //}
}

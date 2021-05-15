using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EtherRequestManagerFacade : MonoBehaviour
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
    public EthScanRequest_ERC20ByAddress m_walletErc20;
    public EthScanRequest_ERC721ByAddress m_walletErc721;

    public string[] m_address20Max = new string[20];
    public EthScanRequest_GetBalanceOfWallets m_etherWallets;


    public string ercContractAddress;
    public string ercContractLinkedAddress;
    public EthScanRequest_ERC20TokenSupplyByContract m_contact;
    public EthScanRequest_ERC20TokenAccountBalanceForTokenContractAddress m_linkedContact;


    public SupplyOfEtherChange supplyOfEtherListener;
    public EtherLastPriceChange lastPriceListener;
    public EtherNodeCount nodeCountListener;
    public EhterCurrentBlockNumber blockNumberListener;
    public EtherGasOracle gasOracleListener;
    public WalletBalanceState walletStateListener;

    public delegate void SupplyOfEtherChange(decimal supplyOfEther);
    public delegate void EtherLastPriceChange(double lastPriceInBitcont, double lastPriceInDollar);
    public delegate void EtherNodeCount(decimal nodeCount);
    public delegate void EhterCurrentBlockNumber(decimal currentBlockNumber);
    public delegate void EtherGasOracle(decimal safeGWei, decimal proposedInGwei, decimal fastInGwei);
    public delegate void WalletBalanceState(string address, string weiState);



    IEnumerator Start()
    {
        m_etherSupply = new EthScanRequest_SupplyOfEther(m_etherScanApiToken);
        AddListenToAndPush(m_etherSupply);
        m_etherLastPrice = new EthScanRequest_EtherLastPrice(m_etherScanApiToken);
        AddListenToAndPush(m_etherLastPrice);
        m_etherNodeCount = new EthScanRequest_EtheriumNodeCount(m_etherScanApiToken);
        AddListenToAndPush(m_etherNodeCount);
        m_etherGasOracle = new EthScanRequest_GasOracle(m_etherScanApiToken);
        AddListenToAndPush(m_etherGasOracle);
        m_etherBlocks = new EthScanRequest_GetBlockNumberByTimestamp(m_etherScanApiToken);
        AddListenToAndPush(m_etherBlocks);

        CheckWalletState(m_addressWallet);
        CheckWalletStateMax20Accounts(m_address20Max);
        CheckWalletStateMoreThat20Accounts(m_address20Max);


        m_transactionStateFail = new EthScanRequest_CheckContarctStatus(m_etherScanApiToken, m_targetTransactionFail);
        AddListenToAndPush(m_transactionStateFail);
        m_transactionStateValide = new EthScanRequest_CheckContarctStatus(m_etherScanApiToken, m_targetTransactionValide);
        AddListenToAndPush(m_transactionStateValide);




        m_walletErc20 = new EthScanRequest_ERC20ByAddress(m_etherScanApiToken, m_addressWallet);
        AddListenToAndPush(m_walletErc20);
        m_walletErc721 = new EthScanRequest_ERC721ByAddress(m_etherScanApiToken, m_addressWallet);
        AddListenToAndPush(m_walletErc721);

        m_walletTransaction = new EthScanRequest_GetWalletTransaction(m_etherScanApiToken, m_addressWallet);
        AddListenToAndPush(m_walletTransaction);




        yield return new WaitForSeconds(10);


        m_ehterGasEstimationTime = new EthScanRequest_EstimationOfConfirmationTIme(m_etherScanApiToken, 80 + "000000000");
        CheckTransactionTimeForInGwei(m_etherGasOracle.m_gazProposeInGWei.ToString());
        
        
        m_contact = new EthScanRequest_ERC20TokenSupplyByContract(m_etherScanApiToken, ercContractAddress);
        AddListenToAndPush(m_etherWallets);
        m_linkedContact = new EthScanRequest_ERC20TokenAccountBalanceForTokenContractAddress(m_etherScanApiToken, ercContractAddress, ercContractLinkedAddress);
        AddListenToAndPush(m_etherWallets);

     
    }
    public void CheckTransactionTimeForInGwei(string gwei)
    {
        AddListenToAndPush(new EthScanRequest_EstimationOfConfirmationTIme(m_etherScanApiToken, gwei + "000000000"));
    }
    public void CheckTransactionTimeForInWei(string wei)
    {
        AddListenToAndPush(new EthScanRequest_EstimationOfConfirmationTIme(m_etherScanApiToken, wei));
    }



    public void CheckERC20(string address)
    {
        AddListenToAndPush(new EthScanRequest_ERC20ByAddress(m_etherScanApiToken, m_addressWallet));
    }
    public void CheckERC721(string address)
    {
        AddListenToAndPush(new EthScanRequest_ERC721ByAddress(m_etherScanApiToken, m_addressWallet));
    }
    public void CheckTransaction(string address)
    {
        AddListenToAndPush(new EthScanRequest_GetWalletTransaction(m_etherScanApiToken, m_addressWallet));
    }








    public void CheckWalletState(string walletAddress)
    {
        m_walletBalance = new EthScanRequest_GetBalance(m_etherScanApiToken, walletAddress);
        AddListenToAndPush(m_walletBalance);
    }
    public void CheckWalletStateMax20Accounts(params string[] walletAddress)
    {
        m_etherWallets = new EthScanRequest_GetBalanceOfWallets(m_etherScanApiToken, walletAddress);
        AddListenToAndPush(m_etherWallets);
        
    }
    public void CheckWalletStateMoreThat20Accounts(params string[] walletAddress)
    {
        int i = 0;
        List<string> addressStack= new List<string>();
        while (i < walletAddress.Length) {
            addressStack.Add(walletAddress[i]);
            if (addressStack.Count == 20) {
                AddListenToAndPush(new EthScanRequest_GetBalanceOfWallets(m_etherScanApiToken, addressStack.ToArray()));
                addressStack.Clear();
            }
            if (addressStack.Count > 20)
            {
                Debug.LogError("Big problem here. Contact developer, the spliting of address by group of 20 did not work  properly.");
            }
            
            i++;
        }
        if (addressStack.Count > 0)
        {
            AddListenToAndPush(new EthScanRequest_GetBalanceOfWallets(m_etherScanApiToken, addressStack.ToArray()));
            addressStack.Clear();
        }
    }

    private void AddListenerTo(EtherScanRequest request)
    {
        request.AddListener(ManageRequestCallback);
    }
    private void AddListenToAndPush(EtherScanRequest request)
    {
        request.AddListener(ManageRequestCallback);
        m_requestSender.AddRequest(request);
    }

    private void ManageRequestCallback(EtherScanRequest requestReceived)
    {
        if (requestReceived is EthScanRequest_GasOracle)
        {
            ManageRequestCastedCallback((EthScanRequest_GasOracle)requestReceived);
        }
        else if (requestReceived is EthScanRequest_SupplyOfEther)
        {
            ManageRequestCastedCallback((EthScanRequest_SupplyOfEther)requestReceived);
        }
        else if (requestReceived is EthScanRequest_EtherLastPrice)
        {
            ManageRequestCastedCallback((EthScanRequest_EtherLastPrice)requestReceived);
        }
        else if (requestReceived is EthScanRequest_EtheriumNodeCount)
        {
            ManageRequestCastedCallback((EthScanRequest_EtheriumNodeCount)requestReceived);
        }
        else if (requestReceived is EthScanRequest_GetBlockNumberByTimestamp)
        {
            ManageRequestCastedCallback((EthScanRequest_GetBlockNumberByTimestamp)requestReceived);
        }
        else if (requestReceived is EthScanRequest_GetBalance)
        {
            ManageRequestCastedCallback((EthScanRequest_GetBalance)requestReceived);
        }
        else if (requestReceived is EthScanRequest_GetBalanceOfWallets)
        {
            ManageRequestCastedCallback((EthScanRequest_GetBalanceOfWallets)requestReceived);
        }

        else
        {

            Debug.Log("Received unable to cast:" + m_requestSender.ToString());
        }

    }
    private void ManageRequestCastedCallback(EthScanRequest_GetBalance requestReceived)
    {
        Debug.Log(">" + requestReceived.ToString());
        if (walletStateListener != null)
            walletStateListener(requestReceived.m_walletAddressTarget, requestReceived.m_wei);
    }
    private void ManageRequestCastedCallback(EthScanRequest_GetBalanceOfWallets requestReceived)
    {
        Debug.Log(">" + requestReceived.ToString());
        if (walletStateListener != null) {
            string[] wallets, balanceInWei;
            requestReceived.GetWalletsInformation(out wallets, out balanceInWei);
            for (int i = 0; i < wallets.Length; i++)
            {
                if (!string.IsNullOrEmpty(wallets[i]) && !string.IsNullOrEmpty(balanceInWei[i])){ 
                    walletStateListener(wallets[i], balanceInWei[i]);
                }
            }
        }
    }


    private void ManageRequestCastedCallback(EthScanRequest_GetBlockNumberByTimestamp requestReceived)
    {
        Debug.Log(">" + requestReceived.ToString());
        if (blockNumberListener != null)
            blockNumberListener(requestReceived.m_currentBlockNumber);
    }

    private void ManageRequestCastedCallback(EthScanRequest_EtheriumNodeCount requestReceived)
    {
        Debug.Log(">" + requestReceived.ToString());
        if (nodeCountListener != null)
            nodeCountListener(requestReceived.m_nodeCount);
    }

    private void ManageRequestCastedCallback(EthScanRequest_EtherLastPrice requestReceived)
    {
        Debug.Log(">" + requestReceived.ToString());
        if(lastPriceListener != null)
         lastPriceListener(requestReceived.m_ehterVsBitcoin, requestReceived.m_ehterVsDollard);
    }

    private void ManageRequestCastedCallback(EthScanRequest_SupplyOfEther requestReceived)
    {
        Debug.Log(">" + requestReceived.ToString());
        if (supplyOfEtherListener != null)
            supplyOfEtherListener(requestReceived.m_ether);

    }

    private void ManageRequestCastedCallback(EthScanRequest_GasOracle requestReceived)
    {
        Debug.Log("Gaz safe:" + requestReceived.m_gazSafeInGWei);
        Debug.Log("Gaz middle:" + requestReceived.m_gazProposeInGWei);
        Debug.Log("Gaz fast:" + requestReceived.m_gazFastInGWei);

        if (gasOracleListener != null)
            gasOracleListener(requestReceived.m_gazSafeInGWei, requestReceived.m_gazProposeInGWei, requestReceived.m_gazFastInGWei);
    }
}

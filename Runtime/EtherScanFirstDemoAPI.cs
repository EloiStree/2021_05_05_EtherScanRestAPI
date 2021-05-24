using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherScanFirstDemoAPI : MonoBehaviour
{
    public string m_etherScanApiToken;
    public Experiment_EtherRequestAntiSpamAPI m_requestSender;
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

    public string[] m_address20Max= new string[20];
    public EthScanRequest_GetBalanceOfWallets m_etherWallets;


    public string ercContractAddress;
    public string ercContractLinkedAddress;
    public EthScanRequest_ERC20TokenSupplyByContract m_contact;
    public EthScanRequest_ERC20TokenAccountBalanceForTokenContractAddress m_linkedContact;

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


        m_walletErc20 = new EthScanRequest_ERC20ByAddress(m_etherScanApiToken, m_addressWallet);
        m_requestSender.AddRequest(m_walletErc20);
        m_walletErc721 = new EthScanRequest_ERC721ByAddress(m_etherScanApiToken, m_addressWallet);
        m_requestSender.AddRequest(m_walletErc721);

        m_walletTransaction = new EthScanRequest_GetWalletTransaction(m_etherScanApiToken, m_addressWallet);
        m_requestSender.AddRequest(m_walletTransaction);

        m_etherWallets = new EthScanRequest_GetBalanceOfWallets(m_etherScanApiToken, m_address20Max);
        m_requestSender.AddRequest(m_etherWallets);






        yield return new WaitForSeconds(10);
        m_ehterGasEstimationTime = new EthScanRequest_EstimationOfConfirmationTIme(m_etherScanApiToken, m_etherGasOracle.m_gazProposeInGWei+ "000000000");
        m_requestSender.AddRequest(m_ehterGasEstimationTime);

        m_contact = new EthScanRequest_ERC20TokenSupplyByContract(m_etherScanApiToken, ercContractAddress);
        m_requestSender.AddRequest(m_contact);
        m_linkedContact = new EthScanRequest_ERC20TokenAccountBalanceForTokenContractAddress(m_etherScanApiToken, ercContractAddress, ercContractLinkedAddress);
        m_requestSender.AddRequest(m_linkedContact);

    }


}


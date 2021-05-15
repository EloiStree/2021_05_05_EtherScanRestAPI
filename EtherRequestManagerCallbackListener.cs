using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherRequestManagerCallbackListener : MonoBehaviour
{
    public EtherRequestManagerFacade m_requestManager;
    
    [Header("Debug")]
    public string m_currentBlockNumber;
    public string m_inDollarValue;
    public string m_inBitcointValue;
    public string m_nodeCountValue;
    public string m_supplyEther;
    public string m_safeGasGwei;
    public string m_proposedGasGwei;
    public string m_fastGasGwei;


    public Dictionary<string, string> m_walletsDictionnaryBalance = new Dictionary<string, string>();

    [TextArea(0,10)]
    public string m_walletNewStateDebuger;

    void Start()
    {
        m_requestManager.gasOracleListener += A;
        m_requestManager.blockNumberListener += B;
        m_requestManager.lastPriceListener += C;
        m_requestManager.nodeCountListener += D;
        m_requestManager.supplyOfEtherListener += E;
        m_requestManager.walletStateListener += F;
    }

    private void F(string address, string weiState)
    {
        if (!m_walletsDictionnaryBalance.ContainsKey(address))
            m_walletsDictionnaryBalance.Add(address, "0");
        m_walletsDictionnaryBalance[address] = weiState;
        m_walletNewStateDebuger = string.Format("{0}:\t{1}\n",address,weiState) + m_walletNewStateDebuger;
        if (m_walletNewStateDebuger.Length > 1000)
            m_walletNewStateDebuger = m_walletNewStateDebuger.Substring(0, 1000);
    }

    private void B(decimal currentBlockNumber)
    {
        m_currentBlockNumber = currentBlockNumber.ToString();
    }

    private void C(double lastPriceInBitcont, double lastPriceInDollar)
    {
        m_inDollarValue = lastPriceInDollar.ToString();
        m_inBitcointValue = lastPriceInBitcont.ToString();
    }

    private void D(decimal nodeCount)
    {
        m_nodeCountValue = nodeCount.ToString();
    }

    private void E(decimal supplyOfEther)
    {
        m_supplyEther = supplyOfEther.ToString();
    }

    private void A(decimal safeGWei, decimal proposedInGwei, decimal fastInGwei)
    {
        m_safeGasGwei = safeGWei.ToString();
        m_proposedGasGwei = proposedInGwei.ToString();
        m_fastGasGwei = fastInGwei.ToString();
    }

}

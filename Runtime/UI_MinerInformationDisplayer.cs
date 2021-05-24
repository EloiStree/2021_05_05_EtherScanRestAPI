using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MinerInformationDisplayer : MonoBehaviour
{
    public InputField m_address;
    public UI_WorkerFrameTextDisplay m_frameGeneral;
    public UI_WorkerFramesHistory m_history;
    public UI_HashRateEstimation m_hashRateEstimation;
    public string m_websiteUrlAddressFormat = "https://ethermine.org/miners/{0}";
    public Text m_unconfirmed;
    public Text m_unpaid;
    public Text m_dollarPerMinute;
    public Text m_bitcoinsPerMinute;
    public Text m_coinsPerMinute;
    public Text m_activeWorker;



    public void OpenUrlOfWallet()
    {

        if (m_address != null)
            Application.OpenURL(string.Format(m_websiteUrlAddressFormat, m_address.text));
    }

    public void SetWith(EtherMinerOrgMinerInfo info)
    {
        if (info == null)
        {
            if (m_frameGeneral != null)
                m_frameGeneral.SetWith(null);
            if (m_hashRateEstimation != null)
                m_hashRateEstimation.SetWith(0);
            if (m_history != null)
                m_history.SetWith(info.GetFameHistory());
            if (m_unconfirmed != null)
                m_unconfirmed.text = "";
            if (m_unpaid != null)
                m_unpaid.text = "";
            if (m_dollarPerMinute != null)
                m_dollarPerMinute.text = "";
            if (m_bitcoinsPerMinute != null)
                m_bitcoinsPerMinute.text = "";
            if (m_coinsPerMinute != null)
                m_coinsPerMinute.text = "";
            if (m_activeWorker != null)
                m_activeWorker.text = "";
            return;
        }

        if (m_hashRateEstimation != null)
            m_hashRateEstimation.SetWith(info.GetFrameStatistic().GetCurrentHashRate());
        if (m_frameGeneral != null)
            m_frameGeneral.SetWith(info.GetFrameStatistic());
        if(m_address!=null)
            m_address.text = info.GetWallet().GetAddress();
        if (m_dollarPerMinute != null)
            m_dollarPerMinute.text = "" + info.GetUsdPerMinutes()*(60*24)+ " $/day";
        if (m_coinsPerMinute != null)
            m_coinsPerMinute.text = "" + info.GetCoinsPerMinutes() * (60 * 24) + " ETH/day";
        if (m_bitcoinsPerMinute != null)
            m_bitcoinsPerMinute.text = "" + info.GetBitcoinPerMinute() * (60 * 24) + " BTC/day";
        if (m_activeWorker != null)
            m_activeWorker.text = "" + info.GetActiveWorkers()+" workers";
        if (m_unconfirmed != null)
            m_unconfirmed.text = "" + info.GetUnconfirmed()+ " unconfirmed";
        if (m_unpaid != null) {
            EthereumConverttion.ApproximateConvert(info.GetUnpaidWai(), out decimal value, EtherType.Wei, EtherType.Ether);
    
                m_unpaid.text = "" + value+ " ETH Unpaid";
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WorkerFullInformation : MonoBehaviour
{
    public InputField m_address;
    public InputField m_workerName;
    public UI_WorkerFrameTextDisplay m_frameCurrent;
    public UI_WorkerFrameTextDisplay m_frameGeneral;
    public UI_WorkerFramesHistory m_history;
    public string m_websiteUrlAddressFormat = "https://ethermine.org/miners/{0}";
    public string m_websiteUrlWorkerFormat = "https://ethermine.org/miners/{0}/worker/{1}";
    public void OpenUrlOfWallet() {

        if(m_address!=null)
           Application.OpenURL(string.Format(m_websiteUrlAddressFormat, m_address.text));
    }
    public void OpenUrlOfWorker() {

        if (m_address != null && m_workerName != null)
            Application.OpenURL(string.Format(m_websiteUrlWorkerFormat, m_address.text, m_workerName.text));
    }

    public void SetWith(EthermineOrgWorkerFullInformation fullWorkerInfo) {

        if (fullWorkerInfo == null) {
            SetToNone();
            return;
        }
        
        if(m_address != null && fullWorkerInfo.GetLinkedWorker()!=null)
            m_address.text = fullWorkerInfo.GetLinkedWorker().GetAddress();
        if (m_workerName != null && fullWorkerInfo.GetLinkedWorker() != null)
            m_workerName.text = fullWorkerInfo.GetLinkedWorker().GetWorkerName();
        fullWorkerInfo.GetCurrentInformation(out bool hasCurrent, out EtherMineOrgWorkerFrame frameCurrent);
        if (m_frameCurrent != null)
            m_frameCurrent.SetWith(frameCurrent);
        fullWorkerInfo.GetGeneralInformation(out bool hasGeneral, out EtherMineOrgWorkerFrame frameGeneral);
        if (m_frameGeneral != null)
            m_frameGeneral.SetWith(frameGeneral);
        fullWorkerInfo.GetHistoryKeyInformation(out bool hasHistory, out EtherMineOrgWorkerFrame [] framesHistory);
        if (m_history != null)
            m_history.SetWith(framesHistory);


    }

    private void SetToNone()
    {
        if (m_address != null)
            m_address.text = "";
        if (m_workerName != null)
            m_workerName.text = "";
    }
}



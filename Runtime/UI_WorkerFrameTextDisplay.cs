using UnityEngine;
using UnityEngine.UI;

public class UI_WorkerFrameTextDisplay : MonoBehaviour
{
    [Header("Time")]
    public Text m_frameCheckTime;
    public Text m_frameLastSeenTime;
    [Header("Hash Rate ")]
    public Text m_hashrateCurrent;
    public Text m_hashrateAverage;
    public Text m_hashrateHardwareReport;
    [Header("Shares ")]
    public Text m_shareValide;
    public Text m_shareInvalide;
    public Text m_shareStale;

    public UI_HashRateEstimation m_hardwareEstimation;


    public void SetWith(EtherMineOrgWorkerFrame frame) {

        if (frame == null)
        {
            if (m_hardwareEstimation)
                m_hardwareEstimation.SetWith(0);
            if (m_frameCheckTime != null)
                m_frameCheckTime.text       = "";
            if (m_frameLastSeenTime != null)
                m_frameLastSeenTime.text    = "";
            if (m_hashrateCurrent != null)
                m_hashrateCurrent.text      = "";
            if (m_hashrateAverage != null)
                m_hashrateAverage.text      = "";
            if (m_shareValide != null)
                m_shareValide.text          = "";
            if (m_shareInvalide != null)
                m_shareInvalide.text        = "";
            if (m_shareStale != null)
                m_shareStale.text = "";
            if (m_hashrateHardwareReport != null)
                m_hashrateHardwareReport.text = "" ;
            return;
        }
        if (m_hardwareEstimation)
            m_hardwareEstimation.SetWith(frame.GetCurrentHashRate());

        if (m_frameCheckTime != null)
            m_frameCheckTime.text = frame.GetTime().ToString();
        if (m_frameLastSeenTime != null)
            m_frameLastSeenTime.text = frame.GetTimeLastSeen().ToString();
        if (m_hashrateCurrent != null)
            m_hashrateCurrent.text =""+ frame.GetCurrentHashRate();
        if (m_hashrateAverage != null)
            m_hashrateAverage.text = "" + frame.GetAverageHashRate();
        if (m_hashrateHardwareReport != null)
            m_hashrateHardwareReport.text = "" + frame.GetReportedHashRate();
        if (m_shareValide != null)
            m_shareValide.text = "" + frame.GetValideShares();
        if (m_shareInvalide != null)
            m_shareInvalide.text = "" + frame.GetInvalideShares();
        if (m_shareStale != null)
            m_shareStale.text = "" + frame.GetStaleShares();

    }


}
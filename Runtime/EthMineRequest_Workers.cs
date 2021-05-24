using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EthMineRequest_Workers
: PublicRestRequest
{
    public string m_addressTarget;
    public bool isConverted;
    public Json_Result result;
    public WalletAddressRef m_resultAddress;
    public WorkerLinkedData<EtherMineOrgWorkerFrame>[] m_resultWorkers;

    public EthMineRequest_Workers(string addressTarget) : base(EthMineUrl.GetWorkersUrl(addressTarget))
    {
        SetAddress(addressTarget);
    }

    public void SetAddress(string addressTarget)
    {
        m_addressTarget = addressTarget;
        SetUrl( EthMineUrl.GetWorkersUrl(addressTarget));
    }

    protected override void NotifyToChildrenAsChanged()
    {

        //https://api.ethermine.org/miner/ee328a992046570f45970e06155b87e813361a4a/workers
        //{"status":"OK","data":[{"worker":"3090-2","time":1621352400,"lastSeen":1621352367,"reportedHashrate":433797908,"currentHashrate":411547659.92833334,"validShares":343,"invalidShares":0,"staleShares":3,"averageHashrate":415222961.9435879},{"worker":"3090-5li","time":1621352400,"lastSeen":1621352367,"reportedHashrate":707161209,"currentHashrate":661196443.3458333,"validShares":549,"invalidShares":0,"staleShares":8,"averageHashrate":692508591.4666438},{"worker":"melih-kktc","time":1621352400,"lastSeen":1621352366,"reportedHashrate":1260949519,"currentHashrate":1247647384.7508333,"validShares":1036,"invalidShares":0,"staleShares":15,"averageHashrate":1220588015.0897803},{"worker":"melih-rigrig-1","time":1621352400,"lastSeen":1621352361,"reportedHashrate":406673131,"currentHashrate":460880884.27416664,"validShares":385,"invalidShares":0,"staleShares":2,"averageHashrate":397732136.68556714},{"worker":"melih-rigrig-1-redminer","time":1621352400,"lastSeen":1621352365,"reportedHashrate":258040846,"currentHashrate":265635850.07416666,"validShares":222,"invalidShares":0,"staleShares":1,"averageHashrate":245787065.04707173}]}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            if (result.status.ToLower() == "ok") {
                WalletAddress address = new DefaultWalletAddress(m_addressTarget);
                m_resultWorkers = new WorkerLinkedData<EtherMineOrgWorkerFrame>[result.data.Length];
                for (int i = 0; i < result.data.Length; i++)
                {
                    Json_Data d = result.data[i];
                    DefaultWorkerFromWalletID worker = new DefaultWorkerFromWalletID(address, result.data[i].worker);
                    string workerFullId = worker.GetWorkerId();


                    EtherMineOrgWorkerFrame frame = new EtherMineOrgWorkerFrame();
                    frame.SetTime((ulong)d.time);
                    frame.SetTimeLastSeen((ulong)d.time);
                    frame.SetHashRate((double)d.currentHashrate, (double)d.reportedHashrate, (double)d.averageHashrate ) ;
                    frame.SetShares(d.validShares, d.invalidShares, d.staleShares);
                    m_resultWorkers[i] = new WorkerLinkedData<EtherMineOrgWorkerFrame>(worker, frame);
                }

            }
        }
        else isConverted = false;

    }

   

    public string [] GetAllWorkersName()
    {
        if (result == null || result.data == null)
            return new string[0];

        return result.data.Select(k => k.worker).ToArray();
    }
    public WorkerLinkedData<EtherMineOrgWorkerFrame>[] GetAllWorkersLastReceivedInformation() {
        return m_resultWorkers;
    }

    public void SetUrlWithAddress(string walletAddess)
    {
        m_addressTarget = walletAddess;
    }

    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public Json_Data [] data;
    }



    [System.Serializable]
    public class Json_Data
    {
        //worker : "3090-2"
        public string worker;
        //time : 1621352400
        public ulong time;
        //lastSeen : 1621352367
        public ulong lastSeen;
        //reportedHashrate : 433797908
        public double reportedHashrate;
        //currentHashrate : 411547659.92833334
        public double currentHashrate;
        //validShares : 343
        public ulong validShares;
        //invalidShares : 0
        public ulong invalidShares;
        //staleShares : 3
        public ulong staleShares;
        //averageHashrate : 415222961.9435879
        public double averageHashrate;
    }
}

public class WalletAddressRef {
    string m_addressTarget;

    public WalletAddressRef(string addressTarget)
    {
        m_addressTarget = addressTarget;
    }
    public string GetAddress() { return m_addressTarget; }

    public void ChangeAddress(string address) {
        m_addressTarget = address;
    }
    public bool IsDefine() {
        return !string.IsNullOrEmpty(m_addressTarget);

    }
}

public class UnixTime { 
    public static DateTime GetFromSecondsUnix(ulong seconds)
    { 
        return new DateTime(1970,1,1).AddSeconds(seconds); 
    }
}



public class EthermineOrgWorkerFullInformation
{
    public DefaultWorkerID m_worker= new DefaultWorkerID("","");
    public EtherMineOrgWorkerFrame m_currentFrameInformation;
    public EtherMineOrgWorkerFrame m_generalStatistics;
    public EtherMineOrgWorkerFrame [] m_historyKeyFrames;

    public bool HasCurrentInformation() { return m_currentFrameInformation != null && m_currentFrameInformation.IsDefined(); }
    public bool HasGeneralStatistics() { return m_generalStatistics != null && m_generalStatistics.IsDefined() ; }
    public bool HasHistory() { return m_historyKeyFrames != null && m_historyKeyFrames.Length > 0; }

    public DefaultWorkerID GetLinkedWorker() { return m_worker; }
    public void GetCurrentInformation(out bool hasInfo, out EtherMineOrgWorkerFrame frame) { hasInfo = HasCurrentInformation(); frame = m_currentFrameInformation; }
    public void GetGeneralInformation(out bool hasInfo, out EtherMineOrgWorkerFrame frame) { hasInfo = HasGeneralStatistics(); frame = m_generalStatistics; }
    public void GetHistoryKeyInformation(out bool hasInfo, out  EtherMineOrgWorkerFrame[] frames ) { hasInfo = HasHistory(); frames = m_historyKeyFrames; }

    public void SetCurrentFrame(EtherMineOrgWorkerFrame frame)
    {
        m_currentFrameInformation = frame;
    }
    public void SetStatistics(EtherMineOrgWorkerFrame frame)
    {
        m_generalStatistics = frame;
    }
    public void SetKeyFrameHistory(params EtherMineOrgWorkerFrame[] frames)
    {
        m_historyKeyFrames = frames;
    }
    public void SetKeyFrameHistory(IEnumerable< EtherMineOrgWorkerFrame> frames)
    {
        SetKeyFrameHistory(frames.ToArray());
    }
   
    public void SetWorkerRef(WorkerAddress workerAddress)
    {
        m_worker.SetWith(workerAddress);
    }

    public string GetShortOneLiner()
    {
        return string.Format("{0} {1}: C{2} G{3} H{4}", m_worker.GetAddress(), m_worker.GetWorkerName(), HasCurrentInformation() , HasGeneralStatistics(), HasHistory());
    }
}



public class EhterMineWorkerRef : WorkerAddress
{

    public WorkerAddress m_workerTargeted;

    public EhterMineWorkerRef(WorkerAddress ethermineOrgWorker)
    {
        this.m_workerTargeted = ethermineOrgWorker;
    }

    public string GetWorkerName() { return m_workerTargeted.GetWorkerName(); }
    public string GetAddress() { return m_workerTargeted.GetAddress(); }
    public WorkerAddress GetReference() { return m_workerTargeted; }
    public bool HasReference() { return m_workerTargeted != null; }

    public string GetWorkerId()
    {
        return m_workerTargeted.GetWorkerId();
    }


}

/// <summary>
/// Representation of a wallet in time
/// </summary>
public class EtherMineOrgWorkerFrame
{
    public EhterMineWorkerRef m_sourceRef;

    //time : 1621352400
    [SerializeField] DateTime m_time;

    //lastSeen : 1621352367
    [SerializeField] DateTime m_lastSeen;

    //reportedHashrate : 433797908
    [SerializeField] double m_reportedHashrate;

    //currentHashrate : 411547659.92833334
    [SerializeField] double m_currentHashrate;

    //validShares : 343
    [SerializeField] ulong m_validShares;

    //invalidShares : 0
    [SerializeField] ulong m_invalidShares;

    //staleShares : 3
    [SerializeField] ulong m_staleShares;

    //averageHashrate : 415222961.9435879
    [SerializeField] double m_averageHashrate;


    public string GetWorkerName() { return m_sourceRef.GetWorkerName(); }
    public DateTime GetTimeLastSeen() { return m_lastSeen; }
    public double GetAverageHashRate() { return m_averageHashrate; }
    public double GetCurrentHashRate() { return m_currentHashrate; }
    public double GetReportedHashRate() { return m_reportedHashrate; }
    public ulong GetValideShares() { return m_validShares ;    }
    public ulong GetInvalideShares() { return m_invalidShares; }
    public ulong GetStaleShares() { return m_staleShares;  }


    //SET 
    public void SetTime(ulong unixTimeInSecond)
    {
        m_time = UnixTime.GetFromSecondsUnix(unixTimeInSecond);
    }
    public void SetTimeLastSeen(ulong unixTimeInSecond)
    {
        m_lastSeen = UnixTime.GetFromSecondsUnix(unixTimeInSecond);
    }
    public void SetHashRate(double current, double reported, double average)
    {
        m_reportedHashrate = reported;
        m_currentHashrate = current;
        m_averageHashrate = average;
    }
    public void SetShares(ulong valide, ulong invalide, ulong stable)
    {
        m_validShares = valide;
        m_invalidShares = invalide;
        m_staleShares = stable;
    }

    public bool IsDefined()
    {
        return m_time!=null;
    }

    public DateTime GetTime()
    {
        return m_time;
    }
}




public class WorkerLinkedData <T> {

    public WorkerAddress m_linkedWorker;
    public T m_linkedData;

    public WorkerLinkedData(WorkerAddress linkedWorker, T linkedData)
    {
        m_linkedWorker = linkedWorker;
        m_linkedData = linkedData;
    }
    public WorkerAddress GetWorker() { return m_linkedWorker; }
    public T GetLinkedData() { return m_linkedData; }
    public void SetLinkedData(T data) { m_linkedData=data; }


}

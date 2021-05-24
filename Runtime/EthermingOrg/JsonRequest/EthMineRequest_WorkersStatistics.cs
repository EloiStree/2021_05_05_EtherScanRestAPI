
using System;
using UnityEngine;

[System.Serializable]
public class EthMineRequest_WorkersStatistics
: PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public string m_addressTarget;
    public string m_workerName;
    public EthMineRequest_WorkersStatistics(string addressTarget, string workerName) : base(EthMineUrl.GetWorkerCurrentStatesUrl(addressTarget, workerName))
    {
        m_addressTarget = addressTarget;
        m_workerName = workerName;
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

    public bool IsValide() { return isConverted && result != null && result.data != null; }

    public void GetStatistics(out WorkerAddress worker, out EtherMineOrgWorkerFrame workerFrame) {

        worker = new DefaultWorkerID(m_addressTarget, m_workerName);
        workerFrame = new EtherMineOrgWorkerFrame();
        workerFrame.SetTime(result.data.time);
        workerFrame.SetTimeLastSeen(result.data.lastSeen);
        workerFrame.SetHashRate(result.data.currentHashrate, result.data.reportedHashrate, result.data.averageHashrate);
        workerFrame.SetShares(result.data.validShares, result.data.invalidShares, result.data.staleShares);


    }

    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public Json_Data data;

        //public decimal GetSupplyInWei()
        //{
        //    decimal value = 0;
        //    decimal.TryParse(result, out value);
        //    return value;
        //}

    }



    [System.Serializable]
    public class Json_Data
    {

        //        time : 1621357800
        public uint time;
        //lastSeen : 1621357694
        public uint lastSeen;
        //reportedHashrate : 212278074
        public uint reportedHashrate;
        //currentHashrate : 227040207.81083333
        public double currentHashrate;
        //validShares : 189
        public uint validShares;
        //invalidShares : 0
        public uint invalidShares;
        //staleShares : 2
        public uint staleShares;
        //averageHashrate : 209388650.22846067
        public double averageHashrate;

        //public ulong GetSupplyInEtherUlong()
        //{
        //    decimal value = 0;
        //    decimal.TryParse(result, out value);
        //    return (ulong)(value / 1000000000000000000);
        //}
    }

    public string GetAddressWorkerId()
    {
        throw new NotImplementedException();
    }


    // public class Json_
}


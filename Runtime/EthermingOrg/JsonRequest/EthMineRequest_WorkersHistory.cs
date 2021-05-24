

using System;
using UnityEngine;

[System.Serializable]
public class EthMineRequest_WorkersHistory
: PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public string m_address;
    public string m_workerName;
    public EthMineRequest_WorkersHistory(string addressTarget, string workerName) : base(EthMineUrl.GetWorkerHistoryUrl(addressTarget, workerName))
    {
        m_address = addressTarget;
        this.m_workerName = workerName; 
    }
    public bool IsValide()
    {
        return !HasError() && HasText();
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


    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public Json_Data[] data;

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
        // time : 1621315200
        public uint time;
        //reportedHashrate : 212304777
        public uint reportedHashrate;
        //currentHashrate : 198406655.59083334
        public double currentHashrate;
        //validShares : 165
        public uint validShares;
        //invalidShares : 0
        public uint invalidShares;
        //staleShares : 2
        public uint staleShares;
        //averageHashrate : 198406655.59083334
        public double averageHashrate;

        //public ulong GetSupplyInEtherUlong()
        //{
        //    decimal value = 0;
        //    decimal.TryParse(result, out value);
        //    return (ulong)(value / 1000000000000000000);
        //}
    }
    public void GetHistory(out WorkerAddress worker, out EtherMineOrgWorkerFrame[] frames)
    {
        worker = new DefaultWorkerID(m_address, m_workerName);
        frames = new EtherMineOrgWorkerFrame[result.data.Length];
        for (int i = 0; i < result.data.Length; i++)
        {
            frames[i] = new EtherMineOrgWorkerFrame();
            frames[i].SetTime(result.data[i].time);
            frames[i].SetTimeLastSeen(result.data[i].time);
            frames[i].SetHashRate(result.data[i].currentHashrate, result.data[i].reportedHashrate, result.data[i].averageHashrate);
            frames[i].SetShares(result.data[i].validShares , result.data[i].invalidShares ,  result.data[i].staleShares);
        }
    }

  


    // public class Json_
}





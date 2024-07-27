using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EthermineWorkersToFileSave : MonoBehaviour
{
    public Experiment_ObserverWorkersAPIUpdated m_workersObserved;
    public AbstractPoolSaveAndLoad m_poolSaveAndLoad;

    public uint year=2021;
    public uint month=05;
    public uint day=28;
    public uint hour=16;
    public uint minute=00;

    public void PushLastSaveToFile() {

        if (m_workersObserved == null)
            return;
        if (m_workersObserved.m_workersInfo == null)
            return;
        if (m_workersObserved.m_workersInfo.HasError() ||  !m_workersObserved.m_workersInfo.HasText())
            return;
        List<AbstractWorkerInfo> resultWorkers= new List<AbstractWorkerInfo>();
      WorkerLinkedData<EtherMineOrgWorkerFrame>[] workers =  m_workersObserved.m_workersInfo.GetAllWorkersLastReceivedInformation();
       
        for (int i = 0; i < workers.Length; i++)
        {
            EtherMineOrgWorkerFrame wFrame = workers[i].GetLinkedData();
            AbstractWorkerInfo workerInfo = new AbstractWorkerInfo();
            workerInfo.m_workerName = wFrame.GetWorkerName();
            workerInfo.m_address = wFrame.GetAddress();
            workerInfo.m_timestampInSeconds = wFrame.GetTimestamp();
            workerInfo.m_averageHashrate = wFrame.GetAverageHashRate();
            workerInfo.m_hardwareHashrate = wFrame.GetReportedHashRate();
            workerInfo.m_serverHashrate = wFrame.GetCurrentHashRate();
            workerInfo.m_validShares = wFrame.GetValideShares();
            workerInfo.m_invalidShares = wFrame.GetInvalideShares();
            workerInfo.m_staleShares = wFrame.GetStaleShares();
            resultWorkers.Add(workerInfo);

        }
        m_poolSaveAndLoad.SaveWorkerState(PoolManageByThisAPI.Ethermine, resultWorkers.ToArray());
     }


    [ContextMenu("Save All the files as an excel file")]
    public void SaveAsExcelsFiles()
    {
     string[] workerList;
     string csvLog="";
        string[] columns = new string[24];
    m_poolSaveAndLoad.ImportAllKeys(PoolManageByThisAPI.Ethermine.ToString(), m_workersObserved.GetFocusAddress(),
            out AbstractWorkerBasicInfo[]  workersDebug);

        for (int i = 0; i < 24; i++)
        {
            columns[i] = "H-" +i;

        }
        AbstractWorkerBasicInfo[] temp;
        workerList =  workersDebug.Select(k => k.m_address.m_workerName).Distinct().ToArray();

        DateTime from, to;
        ulong vFrom, vTo;
        from = to = DateTime.UtcNow;
        vFrom = vTo = UnixTime.GetFromDate(from);

        Csv2DArray<long> sharedByRig = new Csv2DArray<long>();
        sharedByRig.SetSize(columns, workerList);

        for (uint i = 0; i < 24; i++)
        {
            from= from.AddHours(-1);
            vFrom = UnixTime.GetFromDate(from);
            temp = workersDebug.Where(k => k.m_timestampSeconds >= vFrom && k.m_timestampSeconds < vTo).ToArray();
            ulong[] value = new ulong[workerList.Length]; 
            for (uint j = 0; j < workerList.Length; j++)
            {
                long sum =  temp.Where(k => k.m_address.m_workerName== workerList[j]).Sum(k=>k.m_valideShare);
                sharedByRig.SetValue(i, j, sum);

            }


            to = to.AddHours(-1);
            vTo = UnixTime.GetFromDate(to);
        }



        StringBuilder sb = new StringBuilder();
        sb.Append(" Hour/Workers ;" + string.Join(";", columns) + "\n");
        for (uint j = 0; j < workerList.Length; j++)
        {
            sb.Append(workerList[j] + ";");
            for (uint i = 0; i < 24; i++)
            {
                if(i<23)
                    sb.Append(sharedByRig.Get(i, j) + ";");
                else
                    sb.Append(sharedByRig.Get(i, j) + "\n");

            }
        }
        m_poolSaveAndLoad.SaveAsLogFile(PoolManageByThisAPI.Ethermine, m_workersObserved.GetFocusAddress(), "last24H.csv", sb.ToString());

    }
}


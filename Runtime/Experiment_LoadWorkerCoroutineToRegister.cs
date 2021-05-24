using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PublicRequestUnityEvent : UnityEvent<PublicRestRequest>{ 

}

public class Experiment_LoadWorkerCoroutineToRegister : MonoBehaviour
{
    public EtherMineWorkersRegisterMono m_register;
    public Experiment_EtherRequestAntiSpamAPI m_antiSpamRequest;

    public PublicRequestUnityEvent m_fetched;

    public void CheckThatWorkerToExist(WorkerAddress worker) {
        m_register.CheckThatWorkerExist(worker);
    }

    public void Push(PublicRestRequest toPush) {
        toPush.AddListener(Callback);
        m_antiSpamRequest.AddRequest(toPush);
    }
    public void FetchHistoryOf(WorkerAddress worker)
    {
        if (worker == null)
            return;
        Push( new  EthMineRequest_WorkersHistory(worker.GetAddress(), worker.GetWorkerName()));
    }

    public void FetchStatisticsOf(WorkerAddress worker)
    {
        if (worker == null)
            return;
        Push(new EthMineRequest_WorkersStatistics(worker.GetAddress(), worker.GetWorkerName()));
    }
    //public void FetchCurrentInfoOf(WorkerAddress worker)
    //{
    //    if (worker == null)
    //        return;
    //   // Push(new EthMineRequest_Workers(worker.GetAddress(), worker.GetWorkerName()));


    //}
    public void FetchWorkersOf(WalletAddress wallet)
    {
        if (wallet == null)
            return;
        Push(new EthMineRequest_Workers(wallet.GetAddress()));
    }


    private void Callback(PublicRestRequest arg0)
    {
        if (arg0 == null)
            return;
        if (arg0 is EthMineRequest_WorkersHistory) {

            RecoverWorkHistory((EthMineRequest_WorkersHistory) arg0);
        }
        else if (arg0 is EthMineRequest_WorkersStatistics)
        {
            RecoverWorkStatistics((EthMineRequest_WorkersStatistics) arg0);
        }
        else if (arg0 is EthMineRequest_Workers)
        {
            RecovertListOfWorkers((EthMineRequest_Workers)arg0);
        }
        m_fetched.Invoke(arg0);
    }

    private void RecovertListOfWorkers(EthMineRequest_Workers received)
    {
        if (!received.HasError())
        {
            WorkerLinkedData<EtherMineOrgWorkerFrame>[] m_workersLoadedDebug = received.GetAllWorkersLastReceivedInformation();
            for (int i = 0; i < m_workersLoadedDebug.Length; i++)
            {
                WorkerAddress worker = m_workersLoadedDebug[i].GetWorker();
                CheckThatWorkerToExist(worker);

                m_register.GetWorkerInformation(worker, out bool found, out EthermineOrgWorkerFullInformation info);
                if (found)
                {
                    info.SetWorkerRef(worker);
                    info.SetCurrentFrame(m_workersLoadedDebug[i].GetLinkedData());
                }
            }

        }
    }


    private void RecoverWorkHistory(EthMineRequest_WorkersHistory received)
    {

        if (!received.IsValide())
            return;
        received.GetHistory(out WorkerAddress worker, out EtherMineOrgWorkerFrame[] frames);
        CheckThatWorkerToExist(worker);
        m_register.GetWorkerInformation(worker, out bool found, out EthermineOrgWorkerFullInformation fullinfo);
        if (found)
        {
            fullinfo.SetKeyFrameHistory(frames);
        }

    }

    public void FetchWorkersFurtherInformation()
    {
        WorkerAddress[] workers =  m_register.GetAllWorkersRegistered();
        for (int i = 0; i < workers.Length; i++)
        {
            FetchStatisticsOf(workers[i]);
            FetchHistoryOf(workers[i]);
        }
    }
    internal void FetchWorkersFurtherHistoryStatistics()
    {
        WorkerAddress[] workers = m_register.GetAllWorkersRegistered();
        for (int i = 0; i < workers.Length; i++)
        {
            FetchHistoryOf(workers[i]);
        }
    }

    internal void FetchWorkersFurtherStatistics()
    {
        WorkerAddress[] workers = GetWorkersAddress();
        for (int i = 0; i < workers.Length; i++)
        {
            FetchStatisticsOf(workers[i]);
        }
    }

    private WorkerAddress[] GetWorkersAddress()
    {
        return m_register.GetAllWorkersRegistered();
    }

    private void RecoverWorkStatistics(EthMineRequest_WorkersStatistics received)
    {

        if (!received.IsValide())
            return;
        received.GetStatistics(out WorkerAddress worker, out EtherMineOrgWorkerFrame frame);
        CheckThatWorkerToExist(worker);
        m_register.GetWorkerInformation(worker, out bool found, out EthermineOrgWorkerFullInformation fullinfo);
        if (found)
        {
            fullinfo.SetStatistics(frame);
        }


    }

    
}

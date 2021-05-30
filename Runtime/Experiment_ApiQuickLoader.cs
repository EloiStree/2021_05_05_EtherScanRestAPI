using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_ApiQuickLoader : MonoBehaviour
{

    [Header("Init")]
    public string m_addressAtStart;

    [Header("Getter")]
    public Experiment_ObserverMinerAPIUpdated m_minerObserver;
    public Experiment_ObserverWorkersAPIUpdated m_workersObserver;

    [Header("Displayer")]
    public UI_EtherMiningGauge m_objectifOfProject;
    public UI_MinerInformationDisplayer m_minerDisplay;
    public UI_WorkerFullInformation m_currentWorkerDisplay;


    [Header("Register")]
    public EtherMineWorkersRegisterMono m_workersRegister;
    public Experiment_LoadWorkerCoroutineToRegister m_workersLoader;

    private void Start()
    {
        AddListener();
        SetAddress(m_addressAtStart);
        ForceReload();
    }
    public void AddListener()
    {

        m_minerObserver.AddListener(MinerHasReloaded);
        m_workersObserver.AddListener(WorkersHasReloaded);
    }

    private void WorkersHasReloaded()
    {
        Debug.Log("Workers Update");
        WorkerLinkedData<EtherMineOrgWorkerFrame> [] workersInfo =m_workersObserver.m_workersInfo.GetAllWorkersLastReceivedInformation();
        for (int i = 0; i < workersInfo.Length; i++)
        {
            if (workersInfo[i] != null) { 
                m_workersRegister.CheckThatWorkerExist(workersInfo[i].GetWorker());
                m_workersRegister.GetWorkerInformation(workersInfo[i].GetWorker(), out bool found, out EthermineOrgWorkerFullInformation fullinfo);
                if (found) {
                    fullinfo.SetCurrentFrame(workersInfo[i].GetLinkedData());
                }
            }
        }
        m_workersRegister.GetRandomWorker(out EthermineOrgWorkerFullInformation info);
        m_currentWorkerDisplay.SetWith(info);
    }

    private void MinerHasReloaded()
    {
        Debug.Log("Miner Update");
        m_minerDisplay.SetWith(m_minerObserver.m_minerInfo.GetMinerInfo());
        EthereumDoubleValue value = new EthereumDoubleValue(m_minerObserver.m_minerInfo.GetMinerInfo().GetUnpaidWai(), EtherType.Wei);
        m_objectifOfProject.SetWith(value);

    }

    public void SetAddress(string minerAddress) {

        m_minerObserver.SetAddress(minerAddress);
        m_workersObserver.SetAddress(minerAddress);
    }
    public void ForceReload() {
        m_minerObserver.ForceReload();
        m_workersObserver.ForceReload();

    }




    [ContextMenu("Fetch All workers basic information")]
    public void FetchWorkerBasicInformation()
    {
        if (m_workersLoader)
            m_workersLoader.FetchWorkersOf(new DefaultWalletAddress(m_addressAtStart));
    }
    [ContextMenu("Fetch Futher workers information")]
    public void FetchCompleteInformationOfRegisteredWorkers()
    {
        if (m_workersLoader)
            m_workersLoader.FetchWorkersFurtherInformation();
    }
    [ContextMenu("Fetch Futher workers history")]
    public void FetchHistorynOfRegisteredWorkers()
    {
        if(m_workersLoader)
        m_workersLoader.FetchWorkersFurtherHistoryStatistics();
    }
    [ContextMenu("Fetch Futher workers statistics")]
    public void FetchStatisticsOfRegisteredWorkers()
    {
        if (m_workersLoader)
            m_workersLoader.FetchWorkersFurtherStatistics();
    }

}

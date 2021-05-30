using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtheremineMinerToFileSave : MonoBehaviour
{
    public Experiment_ObserverMinerAPIUpdated m_minerObserved;
    public AbstractPoolSaveAndLoad m_poolSaveAndLoad;

    public uint year = 2021;
    public uint month = 05;
    public uint day = 28;
    public uint hour = 16;
    public uint minute = 00;

    public void PushLastSaveToFile()
    {

        if (m_minerObserved == null)
            return;
        if (m_minerObserved.m_minerInfo == null)
            return;
        if (m_minerObserved.m_minerInfo.HasError() || !m_minerObserved.m_minerInfo.HasText())
            return;
        if (!m_minerObserved.m_minerInfo.isConverted)
            return;
        EtherMinerOrgMinerInfo miner = m_minerObserved.m_minerInfo.GetMinerInfo();

            AbstractMinerInfo minerinfo = new AbstractMinerInfo();
             minerinfo.m_address = miner.GetWallet().GetAddress();
            minerinfo.m_timestampInSeconds = miner.GetFrameStatistic().GetTimestamp();
            minerinfo.m_averageHashrate = miner.GetFrameStatistic().GetAverageHashRate();
            minerinfo.m_hardwareHashrate = miner.GetFrameStatistic().GetReportedHashRate();
            minerinfo.m_serverHashrate = miner.GetFrameStatistic().GetCurrentHashRate();
            minerinfo.m_validShares = miner.GetFrameStatistic().GetValideShares();
            minerinfo.m_invalidShares = miner.GetFrameStatistic().GetInvalideShares();
            minerinfo.m_staleShares = miner.GetFrameStatistic().GetStaleShares();
       
            minerinfo.m_unpaidWei = miner.GetUnpaidWai();
            minerinfo.m_bitcoinPerMinute = miner.GetBitcoinPerMinute();
            minerinfo.m_coinsPerMinute = miner.GetCoinsPerMinutes();
            minerinfo.m_usdPerMinute = miner.GetUsdPerMinutes();
            minerinfo.m_workerCount = miner.GetActiveWorkers();


        m_poolSaveAndLoad.SaveMinerState(PoolManageByThisAPI.Ethermine, minerinfo);
        // AbstractMinerBasicInfo[] m_debugMinerKeys;
        //m_poolSaveAndLoad.ImportAllKeys(PoolManageByThisAPI.Ethermine.ToString(), miner.GetWallet().GetAddress(), new DateTime((int)year, (int)month, (int)day, (int)hour, (int)minute, 0, 0, DateTimeKind.Utc), DateTime.UtcNow, out m_debugMinerKeys);
    }
}

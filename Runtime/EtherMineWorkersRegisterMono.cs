using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EtherMineWorkersRegisterMono : MonoBehaviour
{

    public WorkersRegistered<EthermineOrgWorkerFullInformation> m_fullInfoByWorkerRegister = new WorkersRegistered<EthermineOrgWorkerFullInformation>();
    public Experiment_EtherRequestAntiSpamAPI m_antiSpammerRequest;

    public void Clear() {
        m_fullInfoByWorkerRegister.Clear();
    }

    public uint GetWorkersCount()
    {
       return  m_fullInfoByWorkerRegister.GetCount();
    }

    public void GetRandomWorker(out EthermineOrgWorkerFullInformation info)
    {
        if (GetWorkersCount() <= 0)
            info = null;
        else { 
            m_fullInfoByWorkerRegister.GetRandomWorker(out WorkerLinkedData<EthermineOrgWorkerFullInformation> found);
            info = found.GetLinkedData();
        }
    }


    public void GetWorkerInformation(WorkerAddress worker, out bool found, out EthermineOrgWorkerFullInformation workerInfo) {

        m_fullInfoByWorkerRegister.Get(worker, out  found, out WorkerLinkedData<EthermineOrgWorkerFullInformation> fullinfo);
        workerInfo = fullinfo.GetLinkedData();
    }
    public void CheckThatWorkerExist(string address, string workername)
    {
        CheckThatWorkerExist(new DefaultWorkerID(address, workername));
    
    }
    public void CheckThatWorkerExist(WorkerAddress worker)
    {
        m_fullInfoByWorkerRegister.CheckThatWorkerExist(worker);
        m_fullInfoByWorkerRegister.Get(worker, out bool found, out WorkerLinkedData<EthermineOrgWorkerFullInformation> pointer);
        if (pointer.GetLinkedData() == null) {
            EthermineOrgWorkerFullInformation full= new EthermineOrgWorkerFullInformation();
            full.SetWorkerRef(worker);
            pointer.SetLinkedData(full);
        }
        if (!found)
            throw new Exception("Humm");
    }

    public WorkerAddress[] GetAllWorkersRegistered()
    {
        return m_fullInfoByWorkerRegister.m_workersFullInfoGathering.Values.Select(k => k.GetWorker()).ToArray();
    }
    public EthermineOrgWorkerFullInformation[] GetAllWorkersInformationRegistered()
    {
        return m_fullInfoByWorkerRegister.m_workersFullInfoGathering.Values.Select(k => k.GetLinkedData()).ToArray();
    }
}

public class WorkersRegistered<T> where T:class
{

    public Dictionary<string, WorkerLinkedData<T>> m_workersFullInfoGathering = new Dictionary<string, WorkerLinkedData<T>>();
    
    public void CreateMemorySpaceFor(string address, string worker)
    {
        CheckThatWorkerExist(new DefaultWorkerID(address,worker));
    }
    public void CheckThatWorkerExist(WorkerAddress worker)
    {
        if (worker == null)
            return;
        string id = worker.GetWorkerId();
        CreateIfNotRegistered(worker);
        WorkerLinkedData<T> info = m_workersFullInfoGathering[id];
        info.SetLinkedData(null);


    }
    private void CreateIfNotRegistered(WorkerAddress worker)
    {
        if (!m_workersFullInfoGathering.ContainsKey(worker.GetWorkerId()))
            m_workersFullInfoGathering.Add(worker.GetWorkerId(), new WorkerLinkedData<T>(worker, null));
    }

    public void Get(WorkerAddress workerId, out bool found, out WorkerLinkedData<T> fullInfo) {
        found = m_workersFullInfoGathering.ContainsKey(workerId.GetWorkerId());
        if (found)
            fullInfo = m_workersFullInfoGathering[workerId.GetWorkerId()];
        else 
            fullInfo = null;
    }
    public bool Has(WorkerAddress workerId) {
        return m_workersFullInfoGathering.ContainsKey(workerId.GetWorkerId()); 
    }

    public void Set(WorkerAddress worker, T info)
    {
        bool found = m_workersFullInfoGathering.ContainsKey(worker.GetWorkerId());
        if (!found)
            CheckThatWorkerExist(worker);
        m_workersFullInfoGathering[worker.GetWorkerId()].SetLinkedData(info);    
    }

    public uint GetCount()
    {
        return (uint) m_workersFullInfoGathering.Count;
    }

    public void GetRandomWorker(out WorkerLinkedData<T> info)
    {
        WorkerLinkedData<T>[] array = m_workersFullInfoGathering.Values.ToArray();
        info = array[UnityEngine.Random.Range(0, array.Length)];
    }

    public void Clear()
    {
        m_workersFullInfoGathering.Clear();
    }
}

public interface WalletAddress
{
    string GetAddress();
}
public interface WorkerAddress: WalletAddress {
     string GetWorkerName();
    string GetWorkerId();
}

public interface WalletAddressPointer
{
    WalletAddress GetPointer();
}
public interface WorkerAddressPointer
{
    WorkerAddress GetPointer();
}


public class DefaultWorkerID : WorkerAddress{
    public string m_addressName;
    public string m_workerName;

    public DefaultWorkerID(string addressName, string workerName)
    {
        m_addressName = addressName;
        m_workerName = workerName;
    }

    public string GetAddress()
    {
        return m_addressName;
    }

    public string GetWorkerName()
    {
        return m_workerName;
    }
    public string GetWorkerId() { return string.Format("{0}.{1}", m_addressName, m_workerName);}

    public void SetWith(WorkerAddress workerAddress)
    {
        if (workerAddress == null)
            return;
        m_workerName = workerAddress.GetWorkerName();
        m_addressName = workerAddress.GetAddress();
    }
}
public class DefaultWalletAddress : WalletAddress {

    public string m_address;

    public DefaultWalletAddress(string address)
    {
        m_address = address;
    }

    public string GetAddress()
    {
        return m_address;
    }
}

public class DefaultWorkerFromWalletID : WorkerAddress
{
    public WalletAddress m_addressName;
    public string m_workerName;

    public DefaultWorkerFromWalletID(WalletAddress addressName, string workerName)
    {
        m_addressName = addressName;
        m_workerName = workerName;
    }

    public string GetAddress()
    {
        return m_addressName.GetAddress();
    }

    public string GetWorkerName()
    {
        return m_workerName;
    }
    public string GetWorkerId() { return string.Format("{0}.{1}", m_addressName.GetAddress(), m_workerName); }

    public void SetWith(WalletAddress addressName, string workerName)
    {
        if (addressName == null || string.IsNullOrEmpty(addressName.GetAddress()) || string.IsNullOrEmpty(workerName))
            return;
        m_workerName = workerName;
        m_addressName = addressName;
    }
}
 
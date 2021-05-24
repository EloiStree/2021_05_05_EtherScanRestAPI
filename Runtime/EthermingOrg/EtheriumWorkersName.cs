using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://api.ethermine.org/docs/
public class EtheriumWorkersName : MonoBehaviour
{
    public string m_walletTarget = "0x5bdF2ec43F76A296D063Ad85efA8112B07C0597B";
    public string m_walletWorker = "minertwo";
    public string m_payoutTargetTest = "0x3d4b4d182f6a9d250b800f8c36ea19752892c9aa";

    public RequestMiner m_minerPayout = new RequestMiner("", "https://api.ethermine.org/miner/{0}/payouts");
    public RequestWorkers m_targetworkerState = new RequestWorkers("", "https://api.ethermine.org/miner/{0}/worker/{1}/currentStats", "");
    public RequestWorkers m_targetWorkerHistory = new RequestWorkers("", "https://api.ethermine.org/miner/{0}/worker/{1}/history", "");
    public ObserveMinerEtherium m_minerTargeted = new ObserveMinerEtherium("");
    //60F6A0Dc848eD1D0a27dE73630eFdF46A6a11039
    //60F6A0Dc848eD1D0a27dE73630eFdF46A6a11039
    [System.Serializable]
    public class ObserveMinerEtherium {

        public string m_walletUsed;
        public MonoBehaviour m_coroutineCreator;

        public ObserveMinerEtherium(string walletUsed)
        {
            m_minerInfo.Add(m_minerWorkerMonitoring);
            m_minerInfo.Add(m_minerWorkerActualState);
            m_minerInfo.Add(m_minerHisotry);
            m_minerInfo.Add(m_minerCurrentState);
            m_minerInfo.Add(m_minerRounds);
            m_minerInfo.Add(m_minerPayout);
            SetWalletTarget(walletUsed);
        }
        public void SetWalletTarget(string walletUsed)
        {
            m_walletUsed = walletUsed;
            for (int i = 0; i < m_minerInfo.Count; i++)
            {
                m_minerInfo[i].SetWallet(m_walletUsed);
            }
        }


        public void RefreshAllInformation()
        {
            RefreshMinerInformation();
            m_workersNameAll = GetAllWorkers();
            m_workersNameAllCurrent = GetAllWorkersCurrently();
            // add x workers in the list;
            // m_workersAll.Add(new RequestMiner());
            RefreshAllWorkersRegisteredInformation();
        }

        private List<string> GetAllWorkersCurrently()
        {
            List<string> l = new List<string>();
            l.Add("minerone");
            l.Add("minertwo");
            //for (int i = 0; i < l.Count; i++)
            //{
            //    //l.Add()
            //}
            return l;
        }

        private List<string> GetAllWorkers()
        {
            return new List<string>();
        }

        public void RefreshMinerInformation()
        {
            for (int i = 0; i < m_minerInfo.Count; i++)
            {
                m_coroutineCreator.StartCoroutine(m_minerInfo[i].RefreshFromAPI());
            }
        }
        public void OpenUrlInformation()
        {
            for (int i = 0; i < m_minerInfo.Count; i++)
            {
               m_minerInfo[i].OpenUrl();
            }
        }
        public void RefreshAllWorkersRegisteredInformation()
        {
            //m_workersCurrentAll.Clear();
            //for (int i = 0; i < m_workersNameAllCurrent.Count; i++)
            //{
            //    m_workersCurrentAll.Add(new RequestMiner(m_walletUsed, m_workersNameAllCurrent[i]));
            //    m_coroutineCreator.StartCoroutine(m_workersCurrentAll[i].RefreshFromAPI());
            //}
            //m_workersAll.Clear();
            //for (int i = 0; i < m_workersNameAll.Count; i++)
            //{
            //    m_workersNameAll.Add(new RequestWorkers(m_walletUsed, m_workerStateUrl[i] , m_workersNameAll[i]));
            //    m_coroutineCreator.StartCoroutine(m_workersCurrentAll[i].RefreshFromAPI());
            //}
        }
     

        public RequestMiner     m_minerWorkerMonitoring = new RequestMiner("", "https://api.ethermine.org/miner/{0}/workers/monitor");
        public RequestMiner     m_minerWorkerActualState = new RequestMiner("", "https://api.ethermine.org/miner/{0}/workers");
        public RequestMiner     m_minerHisotry = new RequestMiner("", "https://api.ethermine.org/miner/{0}/history");
        public RequestMiner     m_minerCurrentState = new RequestMiner("", "https://api.ethermine.org/miner/{0}/currentStats");
        public RequestMiner     m_minerRounds = new RequestMiner("", "https://api.ethermine.org/miner/{0}/rounds");
        public RequestMiner     m_minerPayout = new RequestMiner("", "https://api.ethermine.org/miner/{0}/payouts");
        public string m_workerStateUrl = "https://api.ethermine.org/miner/{0}/worker/{1}/currentStats";
        public string m_workerHistoryUrl= "https://api.ethermine.org/miner/{0}/worker/{1}/history";


        [Header("Debug")]
         List<RequestMiner> m_minerInfo = new List<RequestMiner>();
        public List<string> m_workersNameAll = new List<string>();
        public List<string> m_workersNameAllCurrent = new List<string>();
        public List<RequestMiner> m_workersCurrentAll = new List<RequestMiner>();
        public List<RequestMiner> m_workersAll = new List<RequestMiner>();
    }

    [System.Serializable]
    public class RequestMiner
    {
        public string m_walletTarget;
        public string m_apiUrl;
        public string m_resultInJson;
        public string m_error;

        public RequestMiner(string walletTarget, string apiUrl)
        {
            m_walletTarget = walletTarget;
            m_apiUrl = apiUrl;
        }

        public  IEnumerator RefreshFromAPI()
        {
            string url = GetUrlToUse();
            if (string.IsNullOrEmpty(url))
                yield return null;
            WWW www = new WWW(url);
            yield return www;
            m_error = www.error;
            m_resultInJson = www.text;
        }
        public void SetWallet(string addresse) {
            m_walletTarget = addresse;
        }
        public virtual string GetUrlToUse() { return string.Format(m_apiUrl, m_walletTarget); }

        public void OpenUrl()
        {
            Application.OpenURL(GetUrlToUse());
        }
    }
    [System.Serializable]
    public class RequestWorkers : RequestMiner
    {
        public string m_workerName;

        public RequestWorkers(string walletTarget, string apiUrl, string workerName):base(walletTarget, apiUrl)
        {
            m_workerName = workerName;
        }

        public void SetWorkerName(string workerName)
        {
            m_workerName = workerName;
        }
        public override string GetUrlToUse() { return string.Format(m_apiUrl, m_walletTarget, m_workerName); }

    }

    List<RequestMiner> torefresh = new List<RequestMiner>();
    void Start()
    {
        torefresh.Add(m_minerPayout);
        torefresh.Add(m_targetworkerState);
        torefresh.Add(m_targetWorkerHistory);
        m_targetworkerState.SetWorkerName(m_walletWorker);
        m_targetWorkerHistory.SetWorkerName(m_walletWorker);
        for (int i = 0; i < torefresh.Count; i++)
        {
            torefresh[i].SetWallet(m_walletTarget);
        }
        m_minerPayout.SetWallet(m_payoutTargetTest);
        for (int i = 0; i < torefresh.Count; i++)
        {
            StartCoroutine(torefresh[i].RefreshFromAPI()); 
        }


        m_minerTargeted.m_coroutineCreator=this;
        m_minerTargeted.SetWalletTarget(m_walletTarget);
        m_minerTargeted.RefreshAllInformation();
        m_minerTargeted.OpenUrlInformation();
    }


    //https://ethpool.org/api/miner/:miner/workersr
}
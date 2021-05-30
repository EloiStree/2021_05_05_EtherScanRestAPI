using System;
using UnityEngine;

[System.Serializable]
public class EthMineRequest_MinerCurrentStats

: PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public EtherMinerOrgMinerInfo m_minerInformation;
    public string m_address;
    public EthMineRequest_MinerCurrentStats(string addressTarget) : base(EthMineUrl.GetMinerCurrentStatesUrl(addressTarget))
    {
        SetAddress(addressTarget);
    }

    public void SetAddress(string addressTarget)
    {
        m_address = addressTarget;
        SetUrl( EthMineUrl.GetMinerCurrentStatesUrl(addressTarget));
    }
    protected override void NotifyToChildrenAsChanged()
    {

        //https://api.ethermine.org//miner/:miner/currentStats
        //{"status":"OK","data":[{"worker":"3090-2","time":1621352400,"lastSeen":1621352367,"reportedHashrate":433797908,"currentHashrate":411547659.92833334,"validShares":343,"invalidShares":0,"staleShares":3,"averageHashrate":415222961.9435879},{"worker":"3090-5li","time":1621352400,"lastSeen":1621352367,"reportedHashrate":707161209,"currentHashrate":661196443.3458333,"validShares":549,"invalidShares":0,"staleShares":8,"averageHashrate":692508591.4666438},{"worker":"melih-kktc","time":1621352400,"lastSeen":1621352366,"reportedHashrate":1260949519,"currentHashrate":1247647384.7508333,"validShares":1036,"invalidShares":0,"staleShares":15,"averageHashrate":1220588015.0897803},{"worker":"melih-rigrig-1","time":1621352400,"lastSeen":1621352361,"reportedHashrate":406673131,"currentHashrate":460880884.27416664,"validShares":385,"invalidShares":0,"staleShares":2,"averageHashrate":397732136.68556714},{"worker":"melih-rigrig-1-redminer","time":1621352400,"lastSeen":1621352365,"reportedHashrate":258040846,"currentHashrate":265635850.07416666,"validShares":222,"invalidShares":0,"staleShares":1,"averageHashrate":245787065.04707173}]}
        if (!HasError() && HasText())
        {
            if (m_address != null && m_address.Length > 0) {
                DefaultWalletAddress wallet = new DefaultWalletAddress(m_address);
                result = JsonUtility.FromJson<Json_Result>(GetText());
                isConverted = true;
                m_minerInformation = new EtherMinerOrgMinerInfo(wallet);
                m_minerInformation.SetPaid(result.data.unpaid, result.data.unconfirmed);
                m_minerInformation.SetEstimationWin(result.data.coinsPerMin, result.data.usdPerMin, result.data.btcPerMin);
                m_minerInformation.SetActiveWorker(result.data.activeWorkers);
                EtherMineOrgWorkerFrame frame = new EtherMineOrgWorkerFrame();
                frame.SetWorkerRef(new EhterMineWorkerRef(new DefaultWorkerFromWalletID(wallet, "")));
                frame.SetTime(result.data.time);
                frame.SetTimeLastSeen(result.data.time);
                frame.SetShares(result.data.validShares, result.data.invalidShares, result.data.staleShares);
                frame.SetHashRate(result.data.currentHashrate, result.data.reportedHashrate, result.data.averageHashrate);
                m_minerInformation.SetCurrentStateAsFrame(frame);
            }
        }
        else isConverted = false;
    }

    public EtherMinerOrgMinerInfo GetMinerInfo()
    {
        return m_minerInformation;
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

        //        time : 1621355400
        public ulong time;
        //lastSeen : 1621355368
        public ulong lastSeen;
        //reportedHashrate : 383639311
        public double reportedHashrate;
        //currentHashrate : 386135382.3333334
        public double currentHashrate;
        //averageHashrate : 373697683.08746505
        public double averageHashrate;
        //validShares : 323
        public uint validShares;
        //invalidShares : 0
        public uint invalidShares;
        //staleShares : 1
        public uint staleShares;

        //activeWorkers : 2
        public uint activeWorkers;
        //unpaid : 53655074925887480
        public ulong unpaid;
        //unconfirmed : null
        public ulong unconfirmed;
        //coinsPerMin : 0.000008515582509185719
        public double coinsPerMin;
        //usdPerMin : 0.02858348940615788
        public double usdPerMin;
        //btcPerMin : 6.612349818382711e-7
        public double btcPerMin;


    }


    // public class Json_
}



public class EtherMinerOrgMinerInfo {

     WalletAddress m_address;
    EtherMineOrgWorkerFrame m_state;
    EtherMineOrgWorkerFrame [] m_history;
    //activeWorkers : 2
    uint m_activeWorkers;
    //unpaid : 53655074925887480
     ulong m_unpaid;
    //unconfirmed : null
     ulong m_unconfirmed;
    //coinsPerMin : 0.000008515582509185719
     double m_coinsPerMin;
    //usdPerMin : 0.02858348940615788
     double m_usdPerMin;
    //btcPerMin : 6.612349818382711e-7
     double m_btcPerMin;

    public WalletAddress GetWallet() { return m_address; }
    public EtherMineOrgWorkerFrame GetFrameStatistic()
    {
        return m_state;
    }
    //activeWorkers : 2
    public uint GetActiveWorkers()
    {
        return m_activeWorkers;
    }
    //unpaid : 53655074925887480
    public ulong GetUnpaidWai()
    {
        return m_unpaid;
    }
    //unconfirmed : null
    public ulong GetUnconfirmed()
    {
        return m_unconfirmed;
    }
    //coinsPerMin : 0.000008515582509185719
    public double GetCoinsPerMinutes()
    {
        return m_coinsPerMin;
    }
    //usdPerMin : 0.02858348940615788
    public double GetUsdPerMinutes()
    {
        return m_usdPerMin;
    }
    //btcPerMin : 6.612349818382711e-7
    public double GetBitcoinPerMinute()
    {
        return m_btcPerMin;
    }

    public EtherMinerOrgMinerInfo(WalletAddress address)
    {
        m_address = address;
        m_state = new EtherMineOrgWorkerFrame();

    }

    public void SetCurrentStateAsFrame(EtherMineOrgWorkerFrame current) {
        m_state = current;
    }
    public void SetActiveWorker(uint activeWorkers)
    {
        this.m_activeWorkers = activeWorkers;
    }
    public void SetPaid(ulong unpaid, ulong unconfirmed)
    {
        this.m_unpaid = unpaid;
        this.m_unconfirmed = unconfirmed;

    }
    public void SetEstimationWin(double coins, double usd, double btc) {

        m_coinsPerMin = coins;
        m_usdPerMin = usd;
        m_btcPerMin = btc;
    }

    public EtherMineOrgWorkerFrame[] GetFameHistory()
    {
        return m_history;
    }
}
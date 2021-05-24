using UnityEngine;

[System.Serializable]
public class EthMineRequest_MinerDashboard

: PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    WalletAddress address;
    public EthMineRequest_MinerDashboard(string addressTarget) : base(EthMineUrl.GetMinerDashboardUrl(addressTarget))
    {
        address = new DefaultWalletAddress(addressTarget);
    }

    protected override void NotifyToChildrenAsChanged()
    {

        //https://api.ethermine.org//miner/:miner/currentStats
        //{"status":"OK","data":[{"worker":"3090-2","time":1621352400,"lastSeen":1621352367,"reportedHashrate":433797908,"currentHashrate":411547659.92833334,"validShares":343,"invalidShares":0,"staleShares":3,"averageHashrate":415222961.9435879},{"worker":"3090-5li","time":1621352400,"lastSeen":1621352367,"reportedHashrate":707161209,"currentHashrate":661196443.3458333,"validShares":549,"invalidShares":0,"staleShares":8,"averageHashrate":692508591.4666438},{"worker":"melih-kktc","time":1621352400,"lastSeen":1621352366,"reportedHashrate":1260949519,"currentHashrate":1247647384.7508333,"validShares":1036,"invalidShares":0,"staleShares":15,"averageHashrate":1220588015.0897803},{"worker":"melih-rigrig-1","time":1621352400,"lastSeen":1621352361,"reportedHashrate":406673131,"currentHashrate":460880884.27416664,"validShares":385,"invalidShares":0,"staleShares":2,"averageHashrate":397732136.68556714},{"worker":"melih-rigrig-1-redminer","time":1621352400,"lastSeen":1621352365,"reportedHashrate":258040846,"currentHashrate":265635850.07416666,"validShares":222,"invalidShares":0,"staleShares":1,"averageHashrate":245787065.04707173}]}
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
        public Json_Data data;
        

    }

    public class Json_Statistic
    {
        //        time : 1621472400
        //reportedHashrate : 164115267512
        //currentHashrate : 154141331375.17828
        //validShares : 128150
        //invalidShares : 0
        //staleShares : 1612
        //activeWorkers : 767
    }
    public class Json_Worker
    {
//        worker : "asus"
//time : 1621558200
//lastSeen : 1621558123
//reportedHashrate : 156012112
//currentHashrate : 147940019.80333334
//validShares : 124
//invalidShares : 0
//staleShares : 0
    }
    public class Json_CurrentStat {
//        time : 1621558200
//lastSeen : 1621558169
//reportedHashrate : 163939812304
//currentHashrate : 152080133188.04498
//validShares : 126787
//invalidShares : 0
//staleShares : 1051
//activeWorkers : 763
//unpaid : 4213860261747547600
    }

    public class Json_Setting {
//        email : "***ing_report@highreso.jp"
//monitor : 1
//minPayout : 1000000000000000000
    }

    [System.Serializable]
    public class Json_Data
    {
        public Json_Statistic[] statistics;
        public Json_Worker[] workers;
        public Json_CurrentStat currentStatistics ;
        public Json_Setting settings;


    }


    // public class Json_
}


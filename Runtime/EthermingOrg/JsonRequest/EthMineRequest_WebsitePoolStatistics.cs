
using UnityEngine;

[System.Serializable]
public class EthMineRequest_WebsitePoolStatistics
: PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public EthMineRequest_WebsitePoolStatistics() : base(EthMineUrl.GetWebsiteBasicPoolStats())
    { }

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
        public Json_MinedBlocks[] minedBlocks;
        public Json_PoolStats poolStats;
        public Json_Price price;
        //public decimal GetSupplyInWei()
        //{
        //    decimal value = 0;
        //    decimal.TryParse(result, out value);
        //    return value;
        //}

    }


    [System.Serializable]
    public class Json_MinedBlocks
    {
        //number : 12459846
        public ulong number;
        //miner : "005e552b8299db50a869264929ba37c516e042a5"
        public string miner;
        //time : 1621360459
        public ulong time;

    }
    [System.Serializable]
    public class Json_PoolStats
    {
        //hashRate : 120250382057474.86
        public double hashRate;

        //miners : 305219
        public ulong miners;

        //workers : 781681
        public ulong workers;

        //blocksPerHour : 51.67
        public double blocksPerHour;
    }
    [System.Serializable]
    public class Json_Price
    {
        //usd : 3418.25
        public double usd;

        //btc : 0.0786
        public double btc;

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


    // public class Json_
}


using UnityEngine;

[System.Serializable]
public class EthMineRequest_WebsiteNetworkStatistics
: PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public EthMineRequest_WebsiteNetworkStatistics() : base(EthMineUrl.GetWebsiteNetworkStatistics())
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
        //        time : 1621360305
        public uint time;
        //blockTime : 13.511785549222
        public double blockTime;
        //difficulty : 7961392005004599
        public uint difficulty;
        //hashrate : 621062292982998
        public uint hashrate;
        //usd : 3418.25
        public double usd;
        //btc : 0.0786
        public double btc;


        //public ulong GetSupplyInEtherUlong()
        //{
        //    decimal value = 0;
        //    decimal.TryParse(result, out value);
        //    return (ulong)(value / 1000000000000000000);
        //}
    }


    // public class Json_
}


//EthMineRequest_WebsiteServersHistory
using UnityEngine;

[System.Serializable]
public class EthMineRequest_WebsiteServersHistory
: PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public EthMineRequest_WebsiteServersHistory() : base(EthMineUrl.GetWebsiteServersHistory())
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
        public Json_Data[] data;

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
        //time : 1620756000
        public ulong time;
        //server : "asia1"
        public string server;
        //hashrate : 22829447193222.04
        public double hashrate;


        //public ulong GetSupplyInEtherUlong()
        //{
        //    decimal value = 0;
        //    decimal.TryParse(result, out value);
        //    return (ulong)(value / 1000000000000000000);
        //}
    }


    // public class Json_
}


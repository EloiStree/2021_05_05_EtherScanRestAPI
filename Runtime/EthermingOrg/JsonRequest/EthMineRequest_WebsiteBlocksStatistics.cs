
using UnityEngine;

[System.Serializable]
public class EthMineRequest_WebsiteBlocksStatistics
: PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public EthMineRequest_WebsiteBlocksStatistics() : base(EthMineUrl.GetWebsiteMinedBlocksStats())
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
        // time : 1620755997
        public ulong time;
        //nbrBlocks : 10
        public uint nbrBlocks;
        //difficulty : 7676418596978986
        public ulong difficulty;

        //public ulong GetSupplyInEtherUlong()
        //{
        //    decimal value = 0;
        //    decimal.TryParse(result, out value);
        //    return (ulong)(value / 1000000000000000000);
        //}
    }


    // public class Json_
}


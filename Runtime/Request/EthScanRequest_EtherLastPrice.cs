using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EthScanRequest_EtherLastPrice : PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public double m_ehterVsBitcoin;
    public double m_ehterVsDollard;
    public EthScanRequest_EtherLastPrice(string apiToken) : base(EthScanUrl.GetETHERLastPrice(apiToken))
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
        //{"status":"1","message":"OK","result":{"ethbtc":"0.06149","ethbtc_timestamp":"1620179532","ethusd":"3370.85","ethusd_timestamp":"1620179533"}}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_ehterVsBitcoin = result.result.GetEtherAsBitcoinValue();
            m_ehterVsDollard = result.result.GetEtherAsDollarValue();
        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public Json_ResultInput result;
    }
    [System.Serializable]
    public class Json_ResultInput
    {
        public string ethbtc;
        public string ethbtc_timestamp;
        public string ethusd;
        public string ethusd_timestamp;

        public double GetEtherAsBitcoinValue()
        {
            double value = 0;
            double.TryParse(ethbtc, out value);
            return value;
        }
        public double GetEtherAsDollarValue()
        {
            double value = 0;
            double.TryParse(ethusd, out value);
            return value;
        }
    }
    // public class Json_
}
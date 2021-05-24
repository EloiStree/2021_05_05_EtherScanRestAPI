using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EthScanRequest_EstimationOfConfirmationTIme : PublicRestRequest
{
    public bool isConverted;
    public Json_Result result;
    public double m_gazProposeEstimateTimeInSeconds;
    public EthScanRequest_EstimationOfConfirmationTIme(string apiToken, string gazInWei) : base(EthScanUrl.GetEstimationOfConfirmationTime(apiToken, gazInWei))
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
        //{ "status":"1","message":"OK","result":"975"}
        if (!HasError() && HasText())
        {
            isConverted = true;
            result = JsonUtility.FromJson<Json_Result>(GetText());
            m_gazProposeEstimateTimeInSeconds = result.GetSecondsEstimation();

        }
        else isConverted = false;
    }
    [System.Serializable]
    public class Json_Result
    {
        public string status;
        public string message;
        public string result;

        public uint GetSecondsEstimation()
        {
            uint value = 0;
            uint.TryParse(result, out value);
            return value;
        }
    }
}

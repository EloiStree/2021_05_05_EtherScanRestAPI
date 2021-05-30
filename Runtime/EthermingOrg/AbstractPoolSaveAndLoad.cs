using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AbstractPoolSaveAndLoad : MonoBehaviour
{

    [Tooltip("Let's the user choose where to store. Else it is in persistance data")]
    public string m_absolutePathCustom;
    [SerializeField] string m_absolutePathByDefault;



    public void SaveWorkerState(PoolManageByThisAPI poolName,params AbstractWorkerInfo [] workerInfo)
    {
        if (workerInfo == null)
            return;
        for (int i = 0; i < workerInfo.Length; i++)
        {
            if(workerInfo[i]!=null)
                SaveWorkerState(poolName.ToString(), workerInfo[i]);
        }
    }
    public void SaveWorkerState(PoolManageByThisAPI poolName, AbstractWorkerInfo workerInfo)
    {
        SaveWorkerState(poolName.ToString(), workerInfo);
    }
    public void SaveWorkerState(string poolName, AbstractWorkerInfo workerInfo) {

        //Debug.Log("T "+workerInfo.m_timestampInSeconds);
        DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
            .AddSeconds(
            Convert.ToDouble(workerInfo.m_timestampInSeconds)
            );
        string fileName = string.Format("{0:0000}_{1:00}_{2:00}_{3:00}_{4:00}_{5}_{6}.workerkey", d.Year,d.Month, d.Day, d.Hour,d.Minute, workerInfo.m_validShares, (int) workerInfo.m_averageHashrate);
        string text = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
          workerInfo.m_address,
          workerInfo.m_workerName,
          workerInfo.m_timestampInSeconds,
          workerInfo.m_hardwareHashrate,
          workerInfo.m_serverHashrate, 
          workerInfo.m_averageHashrate,
          workerInfo.m_validShares,
          workerInfo.m_invalidShares,
          workerInfo.m_staleShares);

        string pathFolder = string.Format("{0}/{1}/{2}/{3}", GetRootPath(), poolName, workerInfo.m_address, workerInfo.m_workerName);
        string pathFile = string.Format("{0}/{1}/{2}/{3}/{4}", GetRootPath(), poolName, workerInfo.m_address, workerInfo.m_workerName, fileName);

        if(!Directory.Exists(pathFolder))
            Directory.CreateDirectory(pathFolder);
        File.WriteAllText(pathFile, text);

    }






    public double m_multiplicator= 1000000000;  
    public void SaveMinerState(PoolManageByThisAPI poolName, AbstractMinerInfo minerinfo)
    {
        SaveMinerState(poolName.ToString(), minerinfo);
    }
    public void SaveMinerState(string poolName, AbstractMinerInfo minerinfo)
    {

        DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
            .AddSeconds(
            Convert.ToDouble(minerinfo.m_timestampInSeconds)
            );
        string fileNameMiner = string.Format("{0:0000}_{1:00}_{2:00}_{3:00}_{4:00}_{5}_{6:0}_{7}_{8}.minerkey", d.Year, d.Month, d.Day, d.Hour, d.Minute, minerinfo.m_validShares, minerinfo.m_averageHashrate, minerinfo.m_workerCount, minerinfo.m_unpaidWei);
        string fileNameMetaInfo = string.Format("{0:0000}_{1:00}_{2:00}_{3:00}_{4:00}_{5:0}_{6:0}_{7:0}.markertstatekey", d.Year, d.Month, d.Day, d.Hour, d.Minute, minerinfo.m_usdPerMinute* m_multiplicator, minerinfo.m_coinsPerMinute * m_multiplicator, minerinfo.m_bitcoinPerMinute * m_multiplicator);
        string text = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}",
          minerinfo.m_address,
          minerinfo.m_timestampInSeconds,
          minerinfo.m_hardwareHashrate,
          minerinfo.m_serverHashrate,
          minerinfo.m_averageHashrate,
          minerinfo.m_validShares,
          minerinfo.m_invalidShares,
          minerinfo.m_staleShares,
          minerinfo.m_workerCount,
          minerinfo.m_unpaidWei,
          minerinfo.m_coinsPerMinute,
          minerinfo.m_usdPerMinute,
          minerinfo.m_bitcoinPerMinute

          );

        Debug.Log(">>" + minerinfo.m_address);
        string root = GetRootPath() + "/" + poolName + "/" + minerinfo.m_address + "/miner/";
        string pathFolder = root;
        string pathFileMiner = root + fileNameMiner;
        string pathFileMarket = root + fileNameMetaInfo;
        if (!Directory.Exists(pathFolder))
            Directory.CreateDirectory(pathFolder);
        File.WriteAllText(pathFileMiner, text);
        File.WriteAllText(pathFileMarket, " '| (- O -) |' ");

    }

    public void SaveAsLogFile(PoolManageByThisAPI ethermine,string mineraddress, string filenameWithExtension, string csvLog)
    {
        string root = GetRootPath() + "/" + ethermine.ToString() + "/" + mineraddress + "/log/";
        string file = root + filenameWithExtension;
        Directory.CreateDirectory(root);
        File.WriteAllText(file, csvLog);
    }

    public void BuildReportBasedOnName(PoolManageByThisAPI poolname, string address)
    {
        BuildReportBasedOnName(poolname.ToString(), address);
    }
    public void BuildReportBasedOnName(string poolname, string address)
    {
        BuildReportBasedOnName(poolname, address, new DateTime(1970, 1, 1), DateTime.Now);
    }
    public void BuildReportBasedOnName(string poolname, string address, DateTime start, DateTime end)
    {
        ImportAllKeys(poolname, address,start, end, out AbstractWorkerBasicInfo [] workerAllKeys);
    }
    public void ImportAllKeys(string poolname, string address, out AbstractWorkerBasicInfo[] workerAllKeys)
    {
        ImportAllKeys(poolname, address, new DateTime(1970, 1, 1), DateTime.UtcNow, out workerAllKeys);
    }
    public void ImportAllKeys(string poolname, string address, DateTime from , DateTime to, out AbstractWorkerBasicInfo[] workerAllKeys)
    {

        List<AbstractWorkerBasicInfo> result = new List<AbstractWorkerBasicInfo>();
        workerAllKeys = null;
        string subRootPath = string.Format("{0}/{1}/{2}", GetRootPath(), poolname, address);
    
        string [] paths = Directory.GetFiles(subRootPath, "*.workerkey", SearchOption.AllDirectories);
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = paths[i].Replace(subRootPath, "").Replace(".workerkey", "").Substring(1);
            //"/rigname/yyyy_mm_dd_hh_mm_share_hashrateaverage"

        }
        paths = paths.OrderBy(k=>k).ToArray();
        Dictionary<string, WorkerPoolAddress> workersRegister = new Dictionary<string, WorkerPoolAddress>();
        
        for (int i = 0; i < paths.Length; i++)
        {
            string[] tokens = paths[i].Split(new char[] { '/', '\\' });
            if (tokens.Length == 2)
            {
                string name = tokens[0];
                string[] meta = tokens[1].Split('_');

                if (!workersRegister.ContainsKey(name))
                    workersRegister.Add(name, new WorkerPoolAddress(poolname, address, name));

                if (meta.Length == 7)
                {
                    GetDate(meta[0], meta[1], meta[2], meta[3], meta[4], out DateTime time);
                    if (time > from && time < to) { 
                        AbstractWorkerBasicInfo workerKey = new AbstractWorkerBasicInfo();
                        workerKey.m_address = workersRegister[name];
                        workerKey.SetValideShare(meta[5]);
                        workerKey.SetHashRate(meta[6]);
                        workerKey.SetTimestamp(time);
                        //workerKey.m_address = address;
                        result.Add(workerKey);
                    }
                }
            }

        }
        workerAllKeys = result.ToArray();
    }


    public string [] t;
    public void ImportAllKeys(string poolname, string address, DateTime from, DateTime to, out AbstractMinerBasicInfo[] minerInfo)
    {

        List<AbstractMinerBasicInfo> result = new List<AbstractMinerBasicInfo>();
        minerInfo = null;
        string subRootPath = string.Format("{0}/{1}/{2}/miner/", GetRootPath(), poolname, address);

        string[] paths = Directory.GetFiles(subRootPath, "*.minerkey", SearchOption.AllDirectories);
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = paths[i].Replace(subRootPath, "").Replace(".minerkey", "");
            //"/rigname/yyyy_mm_dd_hh_mm_share_hashrateaverage"

        }
        paths = paths.OrderBy(k => k).ToArray();
        t = paths;

        for (int i = 0; i < paths.Length; i++)
        {

                string[] meta = paths[i].Split('_');

            if (meta.Length == 9)
            {
                GetDate(meta[0], meta[1], meta[2], meta[3], meta[4], out DateTime time);
                if (time > from && time < to)
                {
                    Debug.Log("Start: " + from + "\n Current: " + time+ "\n End: " + to);
                    AbstractMinerBasicInfo workerKey = new AbstractMinerBasicInfo();
                    workerKey.m_address = new PoolAddress(poolname, address);
                    workerKey.SetValideShare(meta[5]);
                    workerKey.SetHashRate(meta[6]);
                    workerKey.SetTimestamp(time);
                    workerKey.SetPlayerCount(meta[7]);
                    workerKey.SetUnpaidWei(meta[8]);
                    //workerKey.m_address = address;
                    result.Add(workerKey);
                }
            }
            

        }
        minerInfo = result.ToArray();
    }


    public void OpenRootPath() {
        Application.OpenURL(GetRootPath());
    }
    public string GetRootPath() {
        if (m_absolutePathCustom.Length > 0)
            return m_absolutePathCustom;
        if (m_absolutePathCustom.Length == 0)
            m_absolutePathByDefault = Application.persistentDataPath;
        return m_absolutePathByDefault;
    }

    public void GetDate(string year, string month, string day, string hour, string minute, out DateTime time)
    {
        int.TryParse(year, out int vyear);
        int.TryParse(month, out int vmonth);
        int.TryParse(day, out int vday);
        int.TryParse(hour, out int vhour);
        int.TryParse(minute, out int vminute);
        GetDate(vyear, vmonth, vday, vhour, vminute, out time);
    }
    public void GetDate(int year, int month, int day, int hour, int minute, out DateTime time)
    {
        time = new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc);
    }
}


public enum PoolManageByThisAPI { Ethermine }
[System.Serializable]
public class PoolAddress
{
    public string m_poolName;
    public string m_poolAddress;

    public PoolAddress(string poolName, string poolAddress)
    {
        m_poolName = poolName;
        m_poolAddress = poolAddress;
    }
}

[System.Serializable]
public class WorkerPoolAddress
{
    public string m_poolName;
    public string m_poolAddress;
    public string m_workerName;

    public WorkerPoolAddress(string poolName, string poolAddress, string workerName)
    {
        m_poolName = poolName;
        m_poolAddress = poolAddress;
        m_workerName = workerName;
    }
}

[System.Serializable]
public class AbstractWorkerBasicInfo
{
    public WorkerPoolAddress m_address;
    public double m_hashRate;
    public uint m_valideShare;
    public ulong m_timestampSeconds;

    public void SetPoolAddres(ref WorkerPoolAddress poolAddress) {
        m_address = poolAddress;
    }

    public void SetHashRate(string hashrate)
    {
        double.TryParse(hashrate, out double value);
        SetHashRate(value);
    }
    public void SetHashRate(double hashrate)
    {
        m_hashRate = hashrate;
    }
    public void SetValideShare(string share)
    {
        uint.TryParse(share, out uint value);
        SetValideShare(value);
    }
    public void SetValideShare(uint share)
    {
        m_valideShare = share;
    }
    public void SetTimestamp(string year, string month, string day, string hour, string minute)
    {
        int.TryParse(year, out int vyear);
        int.TryParse(month, out int vmonth);
        int.TryParse(day, out int vday);
        int.TryParse(hour, out int vhour);
        int.TryParse(minute, out int vminute);
        SetTimestamp(vyear, vmonth, vday, vhour, vminute);
    }
    public void SetTimestamp(int year, int month, int day, int hour, int minute)
    {
        SetTimestamp(new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc));
    }

    public void SetTimestamp(DateTime time)
    {
        m_timestampSeconds = (ulong)(time - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }
}

[System.Serializable]
public class AbstractWorkerInfo
{

    public string m_address;
    public string m_workerName;
    //time : 1621352400
    public ulong m_timestampInSeconds;
    //reportedHashrate : 433797908
    public double m_hardwareHashrate;
    //currentHashrate : 411547659.92833334
    public double m_serverHashrate;
    //averageHashrate : 415222961.9435879
    public double m_averageHashrate;
    //validShares : 343
    public ulong m_validShares;
    //invalidShares : 0
    public ulong m_invalidShares;
    //staleShares : 3
    public ulong m_staleShares;


}



[System.Serializable]
public class AbstractMinerBasicInfo
{
    public PoolAddress m_address;
    public double m_hashRate;
    public uint m_valideShare;
    public ulong m_timestampSeconds;

    public decimal m_unpaidWei;
    public uint m_workerCount;

    public void SetUnpaidWei(string unpaidwei)
    {
        decimal.TryParse(unpaidwei, out decimal wei);
        SetUnpaidWei(wei);
    }
    public void SetUnpaidWei(decimal unpaidwei) {
        m_unpaidWei = unpaidwei;
    }
    public void SetPlayerCount(string workerCount)
    {
        uint.TryParse(workerCount, out uint wCount);
        m_workerCount = wCount;

    }
    public void SetPlayerCount(uint workerCount) {
        m_workerCount = workerCount;
    
    }
    public void SetPoolAddres(ref PoolAddress poolAddress)
    {
        m_address = poolAddress;
    }

    public void SetHashRate(string hashrate)
    {
        double.TryParse(hashrate, out double value);
        SetHashRate(value);
    }
    public void SetHashRate(double hashrate)
    {
        m_hashRate = hashrate;
    }
    public void SetValideShare(string share)
    {
        uint.TryParse(share, out uint value);
        SetValideShare(value);
    }
    public void SetValideShare(uint share)
    {
        m_valideShare = share;
    }
    public void SetTimestamp(string year, string month, string day, string hour, string minute)
    {
        int.TryParse(year, out int vyear);
        int.TryParse(month, out int vmonth);
        int.TryParse(day, out int vday);
        int.TryParse(hour, out int vhour);
        int.TryParse(minute, out int vminute);
        SetTimestamp(vyear, vmonth, vday, vhour, vminute);
    }
    public void SetTimestamp(int year, int month, int day, int hour, int minute)
    {
        SetTimestamp(new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc));
    }

    public void SetTimestamp(DateTime time)
    {
        m_timestampSeconds = (ulong)(time - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }
}

[System.Serializable]
public class AbstractMinerInfo
{

    public string m_address;
    //time : 1621352400
    public ulong m_timestampInSeconds;
    //reportedHashrate : 433797908
    public double m_hardwareHashrate;
    //currentHashrate : 411547659.92833334
    public double m_serverHashrate;
    //averageHashrate : 415222961.9435879
    public double m_averageHashrate;
    //validShares : 343
    public ulong m_validShares;
    //invalidShares : 0
    public ulong m_invalidShares;
    //staleShares : 3
    public ulong m_staleShares;

    public uint m_workerCount;
    public ulong m_unpaidWei;
    public double m_coinsPerMinute;
    public double m_usdPerMinute;
    public double m_bitcoinPerMinute;




}

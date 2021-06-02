using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class WorkersMinersHistoryKeys : MonoBehaviour
{
    public AbstractPoolSaveAndLoad m_poolLoader;
    public string m_address;
    public PoolManageByThisAPI m_pool;

    public string[] m_filesObserved;
    public TimeUsedDebugger m_timeToImportFiles;
    public string[] m_filesOfThisMonth;
    public TimeUsedDebugger m_timeToImportFilesThisMonth;

    private AbstractWorkerBasicInfo [] m_works;
    public TimeUsedDebugger m_timeForCreateWorkers;
    public TimeUsedDebugger m_test;
    public List<string> l = new List<string>();

    void Start()
    {

        DateTime start, end;
        double result;

       m_timeToImportFilesThisMonth.DoTheThing(() =>
       {
           m_poolLoader.ImportAllFiles(m_pool.ToString(), m_address, out m_filesObserved, "2020_06_01_*", "workerkey");

       });
       m_timeToImportFiles.DoTheThing(() =>
       {
           m_poolLoader.ImportAllFiles(m_pool.ToString(), m_address, out m_filesObserved,"*","workerkey");

       });
       
       m_timeForCreateWorkers.DoTheThing(() =>
       {
           m_poolLoader.ImportAllKeys(m_pool.ToString(), m_address, out m_works);

       });
    }


    //public IEnumerator ImportFiles() { 
    
    //}

    //public IEnumerable<string> ImportAllFiles(string poolname, string address, string fileFormat = "*", string fileExtension = "txt")
    //{
    //    string subRootPath = string.Format("{0}/{1}/{2}", m_poolLoader.GetRootPath(), poolname, address);
    //    string fileEnd = "." + fileExtension;
    //    yield return Directory.EnumerateDirectories(subRootPath, fileFormat + "." + fileExtension, SearchOption.AllDirectories);
    //    //for (int i = 0; i < filesObserved.Length; i++)
    //    //{
    //    //    filesObserved[i] = filesObserved[i].Replace(subRootPath, "").Replace("." + fileExtension, "").Substring(1);
    //    //}
    //    //filesObserved = filesObserved.OrderBy(k => k).ToArray();
    //}

}


[System.Serializable]
public class TimeUsedDebugger {

    public DateTime m_start, m_end;
    public ulong m_millisecondsUsed;

    public void DoTheThing(Action toDO) {

        m_start = DateTime.Now;
        toDO.Invoke();
        m_end = DateTime.Now;
        m_millisecondsUsed = (ulong) (m_end - m_start).TotalMilliseconds;
    }

    public ulong GetMillisecondsUsed()
    {
        return m_millisecondsUsed;
    }
}
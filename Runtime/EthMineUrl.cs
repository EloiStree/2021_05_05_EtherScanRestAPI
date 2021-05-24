
public class EthMineUrl
{
    public static string GetWorkersUrl(string addressTarget)
    {
        return "https://api.ethermine.org/miner/" + addressTarget + "/workers";
    }
    public static string GetWorkersUrl(string addressTarget, string workerName)
    {
        return "https://api.ethermine.org/miner/" + addressTarget + "/workers";
    }

    public static string GetMinerCurrentStatesUrl(string addressTarget)
    {
        return "https://api.ethermine.org/miner/" + addressTarget + "/currentStats";
    }
    public static string GetMinerDashboardUrl(string addressTarget) {
        return "https://api.ethermine.org/miner/"+ addressTarget + "/dashboard";
    }

    public static string GetWorkerHistoryUrl(string addressTarget, string workerName)
    {
        return "https://api.ethermine.org/miner/" + addressTarget + "/worker/" + workerName + "/history";
    }
    public static string GetWorkerCurrentStatesUrl(string addressTarget, string workerName)
    {
        return "https://api.ethermine.org/miner/" + addressTarget + "/worker/" + workerName + "/currentStats";
    }
    public static string GetWorkerMonitorsUrl(string addressTarget, string workerName)
    {
        return "https://api.ethermine.org/miner/" + addressTarget + "/worker/" + workerName + "/monitor";
    }



    public static string GetWebsiteServersHistory()
    {

        return "https://api.ethermine.org/servers/history";
    }
    public static string GetWebsiteNetworkStatistics()
    {

        return "https://api.ethermine.org/networkStats";
    }
    public static string GetWebsiteMinedBlocksStats()
    {

        return "https://api.ethermine.org/blocks/history";
    }
    public static string GetWebsiteBasicPoolStats()
    {

        return "https://api.ethermine.org/poolStats";
    }

}
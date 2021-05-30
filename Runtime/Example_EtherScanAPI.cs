using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Example_EtherScanAPI : MonoBehaviour
{
    public Experiment_EtherRequestAntiSpamAPI m_etherScanAntiSpam;
    public string m_etherApiToken;
    public string m_walletObserved;
    public List<PublicRestRequest> m_debugList = new List<PublicRestRequest>();
    private void Start()
    {
        PublicRestRequest request;
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetTotalSupplyOfEther(m_etherApiToken), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetETHERLastPrice(m_etherApiToken), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.ReturnsTheNumberOfMostRecentBlock(m_etherApiToken), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.ReturnsTheCurrentPricePerGasInWei(m_etherApiToken), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetTotalNodesCount(m_etherApiToken), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetGasOracle(m_etherApiToken), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetListOfERC20(m_etherApiToken, m_walletObserved), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetListOfERC721(m_etherApiToken, m_walletObserved), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetMinedByAddress(m_etherApiToken, m_walletObserved), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetMinedByAddress(m_etherApiToken, m_walletObserved), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetListOfClassicTransactionsByAddress(m_etherApiToken, m_walletObserved), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetEtherBalanceForSingleAddress(m_etherApiToken, m_walletObserved), out request, m_debugList);
        m_etherScanAntiSpam.AddFromUrl(EthScanUrl.GetEthereumNodesSize(m_etherApiToken, new DateTime(2021, 01, 01), new DateTime(2021, 08, 01)), out request, m_debugList);
    }

}
public enum EtherServerTarget { Mainnet, Ropsten }
public class EthScanUrl
{
    public static bool m_useRapstenServer = false;
    public static string GetServerUrl() { return m_useRapstenServer ? "https://api-ropsten.etherscan.io/api" : "https://api.etherscan.io/api"; }
    public static string GetServerUrl(EtherServerTarget server) { return server== EtherServerTarget.Ropsten ? "https://api-ropsten.etherscan.io/api" : "https://api.etherscan.io/api"; }
    public static string GetTotalSupplyOfEther(string apiToken)
    {
        return string.Format(GetServerUrl()+"?module=stats&action=ethsupply&apikey={0}", apiToken);
    }
    public static string GetETHERLastPrice(string apiToken)
    {
        return string.Format(GetServerUrl()+"?module=stats&action=ethprice&apikey={0}", apiToken);
    }
    public static string ReturnsTheNumberOfMostRecentBlock(string apiToken)
    {
        return string.Format(GetServerUrl()+"?module=proxy&action=eth_blockNumber&apikey={0}", apiToken);
    }
    public static string ReturnsTheCurrentPricePerGasInWei(string apiToken)
    {
        return string.Format(GetServerUrl()+"?module=proxy&action=eth_gasPrice&apikey={0}", apiToken);
    }
    public static string GetTotalNodesCount(string apiToken)
    {
        return string.Format(GetServerUrl()+"?module=stats&action=nodecount&apikey={0}", apiToken);
    }
    public static string GetGasOracle(string apiToken) {
        return GetServerUrl()+"?module=gastracker&action=gasoracle&apikey=" + apiToken;
        //
        //(SafeGasPrice, ProposeGasPrice And FastGasPrice returned in Gwei)
    }

    public static string GetListOfERC20(string apiToken, string address)
    {
        return string.Format(GetServerUrl()+"?module=account&action=tokentx&address={1}&startblock=0&endblock=999999999&sort=asc&apikey={0}", apiToken, address);
    }
    public static string GetListOfERC721(string apiToken, string address)
    {
        return string.Format(GetServerUrl()+"?module=account&action=tokennfttx&address={1}&startblock=0&endblock=999999999&sort=asc&apikey={0}", apiToken, address);
    }
    public static string GetMinedByAddress(string apiToken, string address)
    {
        return string.Format(GetServerUrl()+"?module=account&action=getminedblocks&address={1}&blocktype=blocks&apikey={0}", apiToken, address);
    }
    public static string GetListOfClassicTransactionsByAddress(string apiToken, string address)
    {
        //[Optional Parameters] startblock: starting blockNo to retrieve results, endblock: ending blockNo to retrieve results
        return string.Format(GetServerUrl()+"?module=account&action=txlist&address={1}&startblock=0&endblock=99999999&sort=asc&apikey={0}", apiToken, address);
    }

    public static string GetServerUrlWebsite(EtherServerTarget serverType) {

        if(serverType == EtherServerTarget.Ropsten )
        return "https://ropsten.etherscan.io";
        return "https://etherscan.io";
    }

    public static string GetBlockUrl(EtherServerTarget serverType, string blockid)
    {
        return GetServerUrlWebsite(serverType)+"/block/" + blockid;
    }
    public static string  GetWalletUrl(EtherServerTarget serverType, string walletAddress)
    {
        return GetServerUrlWebsite(serverType) + "/address/" + walletAddress;
    }
    public static string GetRawBlockInformation( string apiToken, ulong blockId)
    {
        return GetRawBlockInformation(m_useRapstenServer ? EtherServerTarget.Ropsten : EtherServerTarget.Mainnet, apiToken, blockId);
    
    }
        public static string GetRawBlockInformation(EtherServerTarget serverType, string apiToken, ulong blockId)
    {
        //0x10d4
        return string.Format( "{0}?module=proxy&action=eth_getBlockByNumber&tag={2}&boolean=true&apikey={1}", GetServerUrl(serverType), apiToken,  string.Format("0x{0:X}", blockId));
       
    }

    public static string GetTransactionUrl(EtherServerTarget serverType, string transactionHash)
    {
        return GetServerUrlWebsite(serverType) + "/tx/" +transactionHash;
    }

    internal static string GetListOfClassicTransactionsByAddress(string apiToken, string address, string startBlock, string endBlock)
    {
        return string.Format(GetServerUrl() + "?module=account&action=txlist&address={1}&startblock={2}&endblock={3}&sort=asc&apikey={0}", apiToken, address, startBlock, endBlock);
    }

   

    public static string GetEtherBalanceForSingleAddress(string apiToken, string address)
    {
        return string.Format(GetServerUrl()+"?module=account&action=balance&address={1}&tag=latest&apikey={0}", apiToken, address);
    }

    public static string GetEtherBalanceForMultipleAddresses(string apiToken, params string[] addresses)
    {
        return string.Format(GetServerUrl()+"?module=account&action=balancemulti&address={1}&tag=latest&apikey={0}", apiToken, string.Join(",", addresses.Where(k=>!string.IsNullOrEmpty(k)).Select(k=>k.Trim())));
    }
    public static string GetBlockAndUncleRewardsByBlockNo(string apiToken, string blockNumber)
    {
        return string.Format(GetServerUrl()+"?module=block&action=getblockreward&blockno={1}&apikey={0}", apiToken, blockNumber);
    }
    public static string GetEthereumNodesSize(string apiToken, DateTime start, DateTime end)
    {
        //        return string.Format("https://api.etherscan.io/api?module=stats&action=chainsize&startdate=2019-02-01&enddate=2019-02-28&clienttype=geth&syncmode=default&sort=asc&apikey={0}", apiToken);
        return string.Format(GetServerUrl()+"?module=stats&action=chainsize&startdate={0}&enddate={1}&clienttype=geth&syncmode=default&sort=asc&apikey={0}", apiToken,
            start.ToString("yyyy-mm-dd"), end.ToString("yyyy-mm-dd"));
    }
    public static string GetEstimationOfConfirmationTime(string apiToken, string gasPriceInWei) {
        return string.Format(GetServerUrl()+"?module=gastracker&action=gasestimate&gasprice={1}&apikey={0}", apiToken, gasPriceInWei);
    }
    public static string EstimateGasPriceOfTransaction(string apiToken, string data, string to, string gasPrice, string gas)
    {
        //Makes a call or transaction, which won't be added to the blockchain and returns the used gas, which can be used for estimating the used gas

        return string.Format(GetServerUrl()+"?" +
            "module=proxy&action=eth_estimateGas&" +
            "data={1}&to={2}&value={3}&gasPrice={4}&gas={5}&apikey={0}"
            , apiToken, data, to, gasPrice, gas);
    }


    //public string Get "Internal Transactions" by Block Range(string apiToken){}
    //https://api.etherscan.io/api?module=account&action=txlistinternal&startblock=0&endblock=2702578&page=1&offset=10&sort=asc&apikey=YourApiKeyToken

    //public string Get "Internal Transactions" by Transaction Hash(string apiToken){}
    //https://api.etherscan.io/api?module=account&action=txlistinternal&txhash=0x40eb908387324f2b575b4879cd9d7188f69c8fc9d87c901b9e2daaea4b442170&apikey=YourApiKeyToken

    //public string Get a list of 'Internal' Transactions by Address(string apiToken){}
    //[Optional Parameters] startblock: starting blockNo to retrieve results, endblock: ending blockNo to retrieve results
    //https://api.etherscan.io/api?module=account&action=txlistinternal&address=0x2c1ba59d6f58433fb1eaee7d20b26ed83bda51a3&startblock=0&endblock=2702578&sort=asc&apikey=YourApiKeyToken


    //public string Get Estimated Block Countdown Time by BlockNo(string apiToken){}
    //https://api.etherscan.io/api?module=block&action=getblockcountdown&blockno=9100000&apikey=YourApiKeyToken

    //[Parameters] timestamp format: Unix timestamp(supports Unix timestamps in seconds), closest value: 'before' or 'after'
    public enum SideType { Before, After}
    public static  string GetBlockNumberByTimestamp(string apiToken , string unixTimestampsInSeconds, SideType side) 
    {
        return string.Format(GetServerUrl()+"?module=block&action=getblocknobytime&timestamp={1}&closest={2}&apikey={0}", apiToken, unixTimestampsInSeconds,side==SideType.Before? "before":"after");
    }

    //Note: isError":"0" = Pass , isError":"1" = Error during Contract Execution
    public static string CheckContractExecutionStatus(string apiToken, string addressOfTransation)
    {
        return string.Format(GetServerUrl()+"?module=transaction&action=getstatus&txhash={1}&apikey={0}", apiToken, addressOfTransation);
    }
    //Note: isError":"0" = Pass , isError":"1" = Error during Contract Execution
    public static string CheckTransactionReceiptStatus(string apiToken, string addressOfTransation)
    {
        return string.Format(GetServerUrl()+"?module=transaction&action=gettxreceiptstatus&txhash={1}&apikey={0}", apiToken, addressOfTransation);
    }


    #region geth not tested yet

    public string ReturnsInformationAboutBlockByBlockNumber(string apiToken){
        return GetServerUrl()+"?module=proxy&action=eth_getBlockByNumber&tag=0x10d4f&boolean=true&apikey=" + apiToken;
    }

    public string Returnsinformationaboutaunclebyblocknumber(string apiToken){
        return GetServerUrl()+"?module=proxy&action=eth_getUncleByBlockNumberAndIndex&tag=0x210A9B&index=0x0&apikey=" + apiToken;
    }

    public string Returnsthenumberoftransactionsinablockfromablockmatchingthegivenblocknumber(string apiToken){
        return GetServerUrl()+"?module=proxy&action=eth_getBlockTransactionCountByNumber&tag=0x10FB78&apikey=" + apiToken;
    }

    public string Returnsinformationaboutatransactionbyblocknumberandtransactionindexposition(string apiToken){
        return GetServerUrl()+"?module=proxy&action=eth_getTransactionByBlockNumberAndIndex&tag=0x10d4f&index=0x0&apikey=" + apiToken;
    }

    public string Createsnewmessagecalltransactioncontractcreationforsignedtransactions(string apiToken){
        return GetServerUrl()+"?module=proxy&action=eth_sendRawTransaction&hex=0xf904808000831cfde080&apikey=" + apiToken;
    }

    public string Returnstheinformationaboutatransactionrequestedbytransactionhash(string apiToken, string transactionAddress){
        return GetServerUrl()+"?module=proxy&action=eth_getTransactionByHash&txhash="+ transactionAddress + "&apikey=" + apiToken;
    }


    public string Returnsthenumberoftransactionssentfromanaddress(string apiToken, string addressWallet){
        return " https://api.etherscan.io/api?module=proxy&action=eth_getTransactionCount&address="+addressWallet+"&tag=latest&apikey=" + apiToken;
    }

    public string Returnsthereceiptofatransactionbytransactionhash(string apiToken,string transactionAddress)
    {
        return GetServerUrl()+"?module=proxy&action=eth_getTransactionReceipt&txhash=" + transactionAddress + "&apikey=" + apiToken;
    }

    public string Executesanewmessagecallimmediatelywithoutcreatingatransactionontheblockchain(string apiToken, string data, string address){
        return GetServerUrl()+"?module=proxy&action=eth_call&to="+ address + "&data="+ data + "&tag=latest&apikey=" + apiToken;
    }
    public string Returnscodeatgivenaddress(string apiToken, string address)
    {
        return GetServerUrl()+"?module=proxy&action=eth_getCode&address="+ address + "&tag=latest&apikey=" + apiToken; 
        //(**experimental)
    }
    public string Returnsthevaluefromastoragepositionatagivenaddress(string apiToken, string address)
    {
    return GetServerUrl()+"?module=proxy&action=eth_getStorageAt&address="+address+"&position=0x0&tag=latest&apikey="+apiToken;
    }

    #endregion



    //https://api.etherscan.io/api?module=stats&action=tokensupply&contractaddress=0x26d5bd2dfeda983ecd6c39899e69dae6431dffbb&apikey=DUH7HG8BV9A2C3M7K991ECDPD5IMCMIVWG
    public static string GetERC20TokenTotalSupplyByContractAddress(string apiToken, string contractAddress)
    {
        return string.Format(GetServerUrl()+"?module=stats&action=tokensupply&contractaddress={1}&apikey={0}", apiToken, contractAddress);
    }
    public static string GetERC20TokenAccountBalanceForTokenContractAddress(string apiToken, string contractAddress, string address)
    {
        return string.Format(GetServerUrl()+"?module=account&action=tokenbalance&contractaddress={1}&address={2}&tag=latest&apikey={0}", apiToken, contractAddress,address);
    }

    public static bool IsRequestingRapsten()
    {
        return m_useRapstenServer;
    }
    public static void SetAsUsingRopsten(bool useRapsten) { m_useRapstenServer = useRapsten; }

    public static long GetTimestamp()
    {
        return (long)((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
    }

    public static long GetTimestampFrom(string year, string month, string day, string hour, string minute, string second)
    {
        return GetTimestampFrom(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), int.Parse(second));
    }
    public static long GetTimestampFrom(int year, int month, int day, int hour, int minute, int second)
    {

        return (long)(((new DateTime(year, month, day, hour, minute, second)).Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
    }

    public static string GetRawTransactionInformation(string apiToken, string transactionHash)
    {
        return string.Format(GetServerUrl()+"?module=proxy&action=eth_getTransactionByHash&txhash={1}&apikey={0}"
, apiToken, transactionHash);
    }
}

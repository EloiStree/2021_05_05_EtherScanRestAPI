using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_EtherScanAPI : MonoBehaviour
{
    public Experiment_EtherScanAPI m_etherScanAntiSpam;
    public string m_etherApiToken;
    public string m_walletObserved;
    public List<EtherScanRequest> m_debugList = new List<EtherScanRequest>();
    private void Start()
    {
        EtherScanRequest request;
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
public class EthScanUrl
{

    public static string GetTotalSupplyOfEther(string apiToken)
    {
        return string.Format("https://api.etherscan.io/api?module=stats&action=ethsupply&apikey={0}", apiToken);
    }
    public static string GetETHERLastPrice(string apiToken)
    {
        return string.Format("https://api.etherscan.io/api?module=stats&action=ethprice&apikey={0}", apiToken);
    }
    public static string ReturnsTheNumberOfMostRecentBlock(string apiToken)
    {
        return string.Format("https://api.etherscan.io/api?module=proxy&action=eth_blockNumber&apikey={0}", apiToken);
    }
    public static string ReturnsTheCurrentPricePerGasInWei(string apiToken)
    {
        return string.Format("https://api.etherscan.io/api?module=proxy&action=eth_gasPrice&apikey={0}", apiToken);
    }
    public static string GetTotalNodesCount(string apiToken)
    {
        return string.Format("https://api.etherscan.io/api?module=stats&action=nodecount&apikey={0}", apiToken);
    }
    public static string GetGasOracle(string apiToken) {
        return "https://api.etherscan.io/api?module=gastracker&action=gasoracle&apikey=" + apiToken;
        //
        //(SafeGasPrice, ProposeGasPrice And FastGasPrice returned in Gwei)
    }

    public static string GetListOfERC20(string apiToken, string address)
    {
        return string.Format("https://api.etherscan.io/api?module=account&action=tokentx&address={1}&startblock=0&endblock=999999999&sort=asc&apikey={0}", apiToken, address);
    }
    public static string GetListOfERC721(string apiToken, string address)
    {
        return string.Format("https://api.etherscan.io/api?module=account&action=tokennfttx&address={1}&startblock=0&endblock=999999999&sort=asc&apikey={0}", apiToken, address);
    }
    public static string GetMinedByAddress(string apiToken, string address)
    {
        return string.Format("https://api.etherscan.io/api?module=account&action=getminedblocks&address={1}&blocktype=blocks&apikey={0}", apiToken, address);
    }
    public static string GetListOfClassicTransactionsByAddress(string apiToken, string address)
    {
        //[Optional Parameters] startblock: starting blockNo to retrieve results, endblock: ending blockNo to retrieve results
        return string.Format("https://api.etherscan.io/api?module=account&action=txlist&address={1}&startblock=0&endblock=99999999&sort=asc&apikey={0}", apiToken, address);
    }
    public static string GetEtherBalanceForSingleAddress(string apiToken, string address)
    {
        return string.Format("https://api.etherscan.io/api?module=account&action=balance&address={1}&tag=latest&apikey={0}", apiToken, address);
    }

    public static string GetEtherBalanceForMultipleAddresses(string apiToken, params string[] addresses)
    {
        return string.Format("https://api.etherscan.io/api?module=account&action=balancemulti&address={1}&tag=latest&apikey={0}", apiToken, string.Join(",", addresses));
    }
    public static string GetBlockAndUncleRewardsByBlockNo(string apiToken, string blockNumber)
    {
        return string.Format("https://api.etherscan.io/api?module=block&action=getblockreward&blockno={1}&apikey={0}", apiToken, blockNumber);
    }
    public static string GetEthereumNodesSize(string apiToken, DateTime start, DateTime end)
    {
        //        return string.Format("https://api.etherscan.io/api?module=stats&action=chainsize&startdate=2019-02-01&enddate=2019-02-28&clienttype=geth&syncmode=default&sort=asc&apikey={0}", apiToken);
        return string.Format("https://api.etherscan.io/api?module=stats&action=chainsize&startdate={0}&enddate={1}&clienttype=geth&syncmode=default&sort=asc&apikey={0}", apiToken,
            start.ToString("yyyy-mm-dd"), end.ToString("yyyy-mm-dd"));
    }
    public static string GetEstimationOfConfirmationTime(string apiToken, string gasPriceInWei) {
        return string.Format("https://api.etherscan.io/api?module=gastracker&action=gasestimate&gasprice={1}&apikey={0}", apiToken, gasPriceInWei);
    }
    public static string EstimateGasPriceOfTransaction(string apiToken, string data, string to, string gasPrice, string gas)
    {
        //Makes a call or transaction, which won't be added to the blockchain and returns the used gas, which can be used for estimating the used gas

        return string.Format("https://api.etherscan.io/api?" +
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
    public static  string GetBlockNumberByTimestamp(string apiToken , string unixTimestampsInSeconds) 
    {
        return string.Format("https://api.etherscan.io/api?module=block&action=getblocknobytime&timestamp={1}&closest=before&apikey={0}", apiToken, unixTimestampsInSeconds);
    }

    //Note: isError":"0" = Pass , isError":"1" = Error during Contract Execution
    public static string CheckContractExecutionStatus(string apiToken, string addressOfTransation)
    {
        return string.Format("https://api.etherscan.io/api?module=transaction&action=getstatus&txhash={1}&apikey={0}", apiToken, addressOfTransation);
    }
    //Note: isError":"0" = Pass , isError":"1" = Error during Contract Execution
    public static string CheckTransactionReceiptStatus(string apiToken, string addressOfTransation)
    {
        return string.Format("https://api.etherscan.io/api?module=transaction&action=gettxreceiptstatus&txhash={1}&apikey={0}", apiToken, addressOfTransation);
    }



    //public string Returns information about a block by block number(string apiToken){}
    //https://api.etherscan.io/api?module=proxy&action=eth_getBlockByNumber&tag=0x10d4f&boolean=true&apikey=YourApiKeyTokeneth_getUncleByBlockNumberAndIndex

    //public string Returns information about a uncle by block number(string apiToken){}
    //https://api.etherscan.io/api?module=proxy&action=eth_getUncleByBlockNumberAndIndex&tag=0x210A9B&index=0x0&apikey=YourApiKeyTokeneth_getBlockTransactionCountByNumber

    //public string Returns the number of transactions in a block from a block matching the given block number(string apiToken){}
    //https://api.etherscan.io/api?module=proxy&action=eth_getBlockTransactionCountByNumber&tag=0x10FB78&apikey=YourApiKeyTokeneth_getTransactionByHash

    //public string Returns the information about a transaction requested by transaction hash(string apiToken){}
    //https://api.etherscan.io/api?module=proxy&action=eth_getTransactionByHash&txhash=0x1e2910a262b1008d0616a0beb24c1a491d78771baa54a33e66065e03b1f46bc1&apikey=YourApiKeyTokeneth_getTransactionByBlockNumberAndIndex

    //public string Returns information about a transaction by block number and transaction index position(string apiToken){}
    //https://api.etherscan.io/api?module=proxy&action=eth_getTransactionByBlockNumberAndIndex&tag=0x10d4f&index=0x0&apikey=YourApiKeyTokeneth_getTransactionCount

    //public string Returns the number of transactions sent from an address(string apiToken){}
    //https://api.etherscan.io/api?module=proxy&action=eth_getTransactionCount&address=0x2910543af39aba0cd09dbb2d50200b3e800a63d2&tag=latest&apikey=YourApiKeyTokeneth_sendRawTransaction

    //public string Creates new message call transaction or a contract creation for signed transactions(string apiToken){}
    //https://api.etherscan.io/api?module=proxy&action=eth_sendRawTransaction&hex=0xf904808000831cfde080&apikey=YourApiKeyTokeneth_getTransactionReceipt

    //public string Returns the receipt of a transaction by transaction hash(string apiToken){}
    //https://api.etherscan.io/api?module=proxy&action=eth_getTransactionReceipt&txhash=0x1e2910a262b1008d0616a0beb24c1a491d78771baa54a33e66065e03b1f46bc1&apikey=YourApiKeyTokeneth_call

    //public string Executes a new message call immediately without creating a transaction on the block chain(string apiToken){
    //https://api.etherscan.io/api?module=proxy&action=eth_call&to=0xAEEF46DB4855E25702F8237E8f403FddcaF931C0&data=0x70a08231000000000000000000000000e16359506c028e51f16be38986ec5746251e9724&tag=latest&apikey=YourApiKeyTokeneth_getCode

    //public string Returns code at a given address(string apiToken){
    //https://api.etherscan.io/api?module=proxy&action=eth_getCode&address=0xf75e354c5edc8efed9b59ee9f67a80845ade7d0c&tag=latest&apikey=YourApiKeyTokeneth_getStorageAt (**experimental)

    //public string Returns the value from a storage position at a given address(string apiToken){}
    //https://api.etherscan.io/api?module=proxy&action=eth_getStorageAt&address=0x6e03d9cce9d60f3e9f2597e13cd4c54c55330cfd&position=0x0&tag=latest&apikey=YourApiKeyTokeneth_gasPrice






    //public string Get ERC20-Token TotalSupply by ContractAddress(string apiToken){}
    //https://api.etherscan.io/api?module=stats&action=tokensupply&contractaddress=0x57d90b64a1a57749b0f932f1a3395792e12e7055&apikey=YourApiKeyToken
    //public string Get ERC20-Token Account Balance for TokenContractAddress(string apiToken){}
    //https://api.etherscan.io/api?module=account&action=tokenbalance&contractaddress=0x57d90b64a1a57749b0f932f1a3395792e12e7055&address=0xe04f27eb70e025b78871a2ad7eabe85e61212761&tag=latest&apikey=YourApiKeyToken



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class GenerateQrCodeForReceiveing : MonoBehaviour
{
    public string m_walletAddress = "0x60F6A0Dc848eD1D0a27dE73630eFdF46A6a11039";
    [Range(0,1024)]
    public int m_qrSize = 512;
    public void GenerateLinkedWalletQR(int size = 512)
    {

        GenerateWalletQR(m_walletAddress, size);
    }
    [ContextMenu("Generate Linked QR")]
    public void GenerateLinkedWalletQR()
    {

        GenerateWalletQR(m_walletAddress, m_qrSize);
    }
    public void GenerateWalletQR(string addressOfWallet, int size=512) {

        //
        Application.OpenURL(string.Format("https://chart.googleapis.com/chart?chs={1}x{1}&cht=qr&chl={0}&choe=UTF-8",addressOfWallet, size));
    }
}

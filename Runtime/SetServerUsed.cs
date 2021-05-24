using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetServerUsed : MonoBehaviour
{
    public bool m_useRapstenServer;
    void Awake()
    {
        EthScanUrl.SetAsUsingRapsten (m_useRapstenServer);
    }
    private void OnValidate()
    {

        EthScanUrl.SetAsUsingRapsten(m_useRapstenServer);
    }
}

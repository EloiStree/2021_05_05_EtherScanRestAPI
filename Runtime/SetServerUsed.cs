using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetServerUsed : MonoBehaviour
{
    public bool m_useRopstenServer;
    void Awake()
    {
        EthScanUrl.SetAsUsingRopsten (m_useRopstenServer);
    }
    private void OnValidate()
    {

        EthScanUrl.SetAsUsingRopsten(m_useRopstenServer);
    }
    public void SetCurrentServerTarget(bool useRopsten) {

        m_useRopstenServer = useRopsten;
    }
}

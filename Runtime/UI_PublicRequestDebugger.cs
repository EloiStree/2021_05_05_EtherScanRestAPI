using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_PublicRequestDebugger : MonoBehaviour
{
    public StringEvent m_url;
    public StringEvent m_json;
    public StringEvent m_error;

    [System.Serializable]
    public class StringEvent: UnityEvent<string> { }

    public void Debug( PublicRestRequest request)
    {
        m_url.Invoke(request.GetUsedUrl());
        m_json.Invoke(request.GetText());
        m_error.Invoke(request.GetError());
    }
}

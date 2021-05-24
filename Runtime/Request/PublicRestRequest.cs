using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]

public class PublicRestRequestDefault : PublicRestRequest
{
    public PublicRestRequestDefault(string urlOfRequest) : base(urlOfRequest)
    {
    }

    protected override void NotifyToChildrenAsChanged()
    {
    }
}

[System.Serializable]
public abstract class PublicRestRequest
{
    [System.Serializable]
    public class HidableText
    {
        public string m_text;

    }

    public string GetError()
    {
        return m_error;
    }

    public string m_url;
    public HidableText m_text = new HidableText();
    public string m_error;
    public bool m_hasBeenDownload;
  
    public EtherScanRequestEvent m_loadedListener= new EtherScanRequestEvent();

    [System.Serializable]
    public class EtherScanRequestEvent : UnityEvent<PublicRestRequest> { }
    public void AddListener(UnityAction<PublicRestRequest> toDoOnLoaded)
    {
        m_loadedListener.AddListener(toDoOnLoaded);
    }
    public void RemoveListener(UnityAction<PublicRestRequest> toDoOnLoaded)
    {
        m_loadedListener.RemoveListener(toDoOnLoaded);
    }

    public PublicRestRequest(string urlOfRequest)
    {
        this.m_url = urlOfRequest;
    }

    public IEnumerator SendRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(m_url))
        {
            m_hasBeenDownload = false;
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            m_hasBeenDownload = true;
            m_text.m_text = webRequest.downloadHandler.text;
            m_error = webRequest.error;
            NotifyAsChanged();
        }
        yield return null;
    }

    public string GetText() { return m_text.m_text; }
    public string GetUsedUrl()
    {
        return this.m_url;
    }
    public void SetUrl(string url)
    {
        m_url = url;
    }
    public bool HasBeenDownloaded()
    {
        return m_hasBeenDownload;
    }

    public bool HasError()
    {
        return !string.IsNullOrEmpty(m_error);
    }
    public bool HasText()
    {
        return !string.IsNullOrEmpty(m_text.m_text);
    }

    public void NotifyAsChanged()
    {
        NotifyToChildrenAsChanged();
        if (m_loadedListener != null)
            m_loadedListener.Invoke(this);

    }

    protected abstract void NotifyToChildrenAsChanged();

}
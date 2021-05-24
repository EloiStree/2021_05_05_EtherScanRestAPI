using System;
using UnityEngine;
public class UI_WorkerFramesHistory : MonoBehaviour
{
    public UI_WorkerFrameTextDisplay[] m_displayers;


    public void SetWith(EtherMineOrgWorkerFrame[] framesHistory)
    {
        if (framesHistory != null) { 
            for (int i = 0; i < m_displayers.Length; i++)
            {
                if (i < framesHistory.Length)
                {

                    m_displayers[i].SetWith(framesHistory[i]);
                }
                else {
                    m_displayers[i].SetWith(null);
                }

            }
        }
    }

    public void SetToEmpty()
    {
        for (int i = 0; i < m_displayers.Length; i++)
        {
            if(m_displayers[i]!=null)
                m_displayers[i].SetWith(null);
        }
    }
}
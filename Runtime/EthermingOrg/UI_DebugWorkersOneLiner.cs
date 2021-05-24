using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UI_DebugWorkersOneLiner : MonoBehaviour
{
    public EtherMineWorkersRegisterMono m_register;


    public InputField m_textDebug;

    public void Refresh() {

        StringBuilder sb = new StringBuilder();
        foreach (var item in m_register.GetAllWorkersInformationRegistered())
        {

            sb.Append(item.GetShortOneLiner()).Append("\n");

        }

        m_textDebug.text = sb.ToString();
    
    }
}

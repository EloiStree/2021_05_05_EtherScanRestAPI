using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_HashRateConverter : MonoBehaviour
{
    public double m_in;
    public HashRateQuantity m_inType;
    public HashRateQuantity m_outType;
    public double m_out;


    private void OnValidate()
    {
        HashRateConvertion.ApproximateConvert(m_in, out m_out, m_inType, m_outType);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EtherMiningGauge : MonoBehaviour
{


    public EthereumDoubleValue m_gaugeSize = new EthereumDoubleValue(4f, EtherType.Ether);
    public EthereumDoubleValue m_maxWanted = new EthereumDoubleValue(1f, EtherType.Ether);
    public EthereumDoubleValue m_minimumWanted = new EthereumDoubleValue(0.1f, EtherType.Ether);
    public EthereumDoubleValue m_setState = new EthereumDoubleValue(0, EtherType.Ether);


    public Image m_maxWantedImage;
    public Image m_minimumWantedImage;
    public Image m_setStateImage;
    public Text m_minValueText;
    public Text m_maxValueText;
    public Text m_currentValueText;

    public void SetWith(IEtherValue value) {
        m_setState.SetWith(value);
        RefreshUI();
    }

    private void OnValidate()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (m_maxWantedImage != null)
            m_maxWantedImage.fillAmount = (float)EthereumConverttion.GetPourcentOf(m_maxWanted, m_gaugeSize);
        if (m_minimumWantedImage != null)
            m_minimumWantedImage.fillAmount = (float)EthereumConverttion.GetPourcentOf(m_minimumWanted, m_gaugeSize);
        if (m_setStateImage != null)
            m_setStateImage.fillAmount = (float)EthereumConverttion.GetPourcentOf(m_setState, m_gaugeSize);
        if (m_minValueText != null)
        {
            float pct = (float)EthereumConverttion.GetPourcentOf(m_minimumWanted, m_gaugeSize);
            m_minValueText.text = m_minimumWanted.ToString().ToUpper();
            m_minValueText.rectTransform.anchorMin = new Vector2(0, pct);
            m_minValueText.rectTransform.anchorMax = new Vector2(0, pct);
        }
        if (m_maxValueText != null)
        {
            float pct = (float)EthereumConverttion.GetPourcentOf(m_maxWanted, m_gaugeSize);
            m_maxValueText.text = m_maxWanted.ToString().ToUpper();
            m_maxValueText.rectTransform.anchorMin = new Vector2(0, pct);
            m_maxValueText.rectTransform.anchorMax = new Vector2(0, pct);
        }
        if (m_currentValueText != null)
        {
            float pct = (float)EthereumConverttion.GetPourcentOf(m_setState, m_gaugeSize);
            m_currentValueText.text = m_setState.ToString().ToUpper();
            m_currentValueText.rectTransform.anchorMin = new Vector2(0, pct);
            m_currentValueText.rectTransform.anchorMax = new Vector2(0, pct);
        }
    }
}


[System.Serializable]
public class EthereumDoubleValue : IEtherValue
{
    public double m_value = 0;
    public EtherType m_sourceType;

    public EthereumDoubleValue(double value, EtherType sourecType)
    {
        m_value = value;
        m_sourceType = sourecType;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public void Get(EtherType type, out decimal approximation)
    {
        EthereumConverttion.ApproximateConvert((decimal)m_value, out approximation, m_sourceType, type);
    }
    public void Get(EtherType type, out double approximation)
    {
        EthereumConverttion.ApproximateConvert(m_value, out approximation, m_sourceType, type);
    }

    public void Get(out EtherType typeUsedToStore, out decimal storedValue)
    {
        typeUsedToStore = m_sourceType;
        storedValue = (decimal)m_value;
    }

    public void Get(out EtherType typeUsedToStore, out double storedValue)
    {
        typeUsedToStore = m_sourceType;
        storedValue = m_value;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public void SetWith(IEtherValue value)
    {
        value.Get(out EtherType t, out decimal v);
        this.m_sourceType = t;
        this.m_value =(double) v;
    }

    public override string ToString()
    {
        return EthereumConverttion.ToString(this);
    }
}
public class EthereumDecimalValue : IEtherValue
{
    public decimal m_value = new decimal();
    public EtherType m_sourceType;

    public EthereumDecimalValue(decimal value, EtherType sourceType)
    {
        m_value = value;
        m_sourceType = sourceType;
    }

    public void Get(EtherType type, out decimal approximation)
    {
        EthereumConverttion.ApproximateConvert(m_value, out approximation, m_sourceType, type);
    }
    public void Get(EtherType type, out double approximation)
    {
        EthereumConverttion.ApproximateConvert((double)m_value, out approximation, m_sourceType, type);
    }

    public void Get(out EtherType typeUsedToStore, out decimal storedValue)
    {
        typeUsedToStore = m_sourceType;
        storedValue = m_value;
    }

    public void Get(out EtherType typeUsedToStore, out double storedValue)
    {
        typeUsedToStore = m_sourceType;
        storedValue = (double)m_value;
    }
    public override string ToString()
    {
        return EthereumConverttion.ToString(this);
    }
}

public interface IEtherValue {

    void Get(EtherType wantedType, out decimal value);
    void Get(EtherType wantedType, out double approximation);
    void Get(out EtherType typeUsedToStore, out decimal storedValue);
    void Get(out EtherType typeUsedToStore, out double storedValue);
}

public enum EtherType { Wei, GWei, Ether}
public class EthereumConverttion {


    public static double GetPourcentOf(IEtherValue value, IEtherValue max) {
        value.Get(EtherType.Wei, out decimal v);
        max.Get(EtherType.Wei, out decimal m);
        if (m == 0)
            return 0;
        return (double)(v / m);

    }

    public static string GetStringCompressionOf(EtherType type)
    {
        switch (type)
        {
            case EtherType.Wei:
                return "Wei";
            case EtherType.GWei:
                return "GWei";
            case EtherType.Ether:
                return "ETH";
            default:
                break;
        }
        return "";
    }

    public static void ApproximateConvert(double valueGiven, out double resultValue, EtherType givenType, EtherType wantedType)
    {
        HashValue(givenType, out double multiGiven);
        double given = valueGiven * multiGiven;

        HashValue(wantedType, out double multiTo);
        resultValue = given / multiTo;
    }
    public static void ApproximateConvert(decimal valueGiven, out decimal resultValue, EtherType givenType, EtherType wantedType)
    {
        HashValue(givenType, out decimal multiGiven);
        decimal given = (valueGiven) * multiGiven;

        HashValue(wantedType, out decimal multiTo);
        resultValue = given / multiTo;
    }

    public static void HashValue(EtherType type, out double value)
    {
        switch (type)
        {
            case EtherType.Wei: value = 1; return;
            case EtherType.GWei: value = 1000000000; return;
            case EtherType.Ether: value = 1000000000000000000; return;
            default:
                break;
        }
        throw new Exception("Should not be reach");
    }

    public static void HashValue(EtherType type, out decimal value)
    {
        switch (type)
        {

            case EtherType.Wei: value = 1; return;
            case EtherType.GWei: value =  1000000000; return;
            case EtherType.Ether: value = 1000000000000000000; return;
            default:
                break;
        }
        throw new Exception("Should not be reach");
    }

    public static string ToString(IEtherValue ethereumDoubleValue, string decimalFormat = "{0:0.00} {1}")
    {
        ethereumDoubleValue.Get(out EtherType t, out double d);
        return string.Format(decimalFormat, d, t);
    }
    public static string ToString(IEtherValue ethereumDoubleValue, EtherType wantedType, string decimalFormat= "{0:0.00} {1}")
    {
        ethereumDoubleValue.Get(out EtherType currentValueType, out double currentvalue);
        ApproximateConvert(currentvalue, out double resultvalue, currentValueType, wantedType);
        return string.Format(decimalFormat, resultvalue, wantedType);
    }

    public static void TryParse(string inValue, out string outValue, EtherType inType, EtherType outType)
    {
        decimal.TryParse(inValue, out decimal inValueDeci);
        ApproximateConvert(inValueDeci, out decimal outValueDeci, inType, outType);
        if (outType == EtherType.Wei)
            outValue = string.Format("{0:0}", outValueDeci);
        else
            outValue = string.Format("{0}", outValueDeci);

    }
}
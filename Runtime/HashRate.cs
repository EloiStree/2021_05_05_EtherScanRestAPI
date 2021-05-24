using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HashRateBean
{
    [SerializeField] double m_hashRate;
    [SerializeField] HashRateQuantity m_hashRateType;
    
    public HashRateBean(double hashRate, HashRateQuantity type = HashRateQuantity.HashPerSecond)
    {
        SetHashRate(hashRate, type);
    }

    public void SetHashRate(double hashRate, HashRateQuantity type = HashRateQuantity.HashPerSecond)
    {
        if (hashRate < 0)
            hashRate *= -1.0;
        m_hashRate = hashRate;
        m_hashRateType = type;
    }
    public void GetHashRate(out double hashrateValue, out HashRateQuantity hashRateType)
    {
        hashrateValue = m_hashRate;
        hashRateType = m_hashRateType;
    }

    public void GetAsHashPerSecond(out double result)
    {
        HashRateConvertion.ApproximateConvert(m_hashRate, out result, m_hashRateType, HashRateQuantity.HashPerSecond);
    }
    public void GetAsHashPerSecond(out decimal result)
    {
        HashRateConvertion.ApproximateConvert(m_hashRate, out result, m_hashRateType, HashRateQuantity.HashPerSecond);

    }
    public static bool ALessThenB(HashRateBean a, HashRateBean b)
    {

        a.GetAsHashPerSecond(out decimal ad);
        b.GetAsHashPerSecond(out decimal bd);
        return ad < bd;
    }
    public static bool AMoreThenB(HashRateBean a, HashRateBean b)
    {

        a.GetAsHashPerSecond(out decimal ad);
        b.GetAsHashPerSecond(out decimal bd);
        return ad > bd;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("{0:0.00} {1}", m_hashRate, HashRateConvertion.GetStringCompressionOf(m_hashRateType));
    }
}

public enum HashRateQuantity : int { HashPerSecond = 0, KiloHs = 1, MegaHs = 2, GigaHs = 3, TeraHs = 4, PetaHs = 5, ExaHs = 6 }
public class HashRateConvertion
{

    public static string GetStringCompressionOf(HashRateQuantity type)
    {
        switch (type)
        {
            case HashRateQuantity.HashPerSecond:
                return "Hs";
            case HashRateQuantity.KiloHs:
                return "KHs";
            case HashRateQuantity.MegaHs:
                return "MHs";
            case HashRateQuantity.GigaHs:
                return "GHs";
            case HashRateQuantity.TeraHs:
                return "THs";
            case HashRateQuantity.PetaHs:
                return "PHs";
            case HashRateQuantity.ExaHs:
                return "ExaHs";
            default:
                break;
        }
        return "";
    }

    public static void ApproximateConvert(double valueGiven, out double resultValue, HashRateQuantity givenType, HashRateQuantity wantedType)
    {
        HashValue(givenType, out double multiGiven);
        double given = valueGiven * multiGiven;

        HashValue(wantedType, out double multiTo);
        resultValue = given / multiTo;
    }
    public static void ApproximateConvert(double valueGiven, out decimal resultValue, HashRateQuantity givenType, HashRateQuantity wantedType)
    {
        HashValue(givenType, out decimal multiGiven);
        decimal given = ((decimal)valueGiven) * multiGiven;

        HashValue(wantedType, out decimal multiTo);
        resultValue = given / multiTo;
    }

    public static void HashValue(HashRateQuantity type, out double value)
    {
        switch (type)
        {
            case HashRateQuantity.HashPerSecond: value = 1; return;
            case HashRateQuantity.KiloHs: value = 1000; return;
            case HashRateQuantity.MegaHs: value = 1000000; return;
            case HashRateQuantity.GigaHs: value = 1000000000; return;
            case HashRateQuantity.TeraHs: value = 1000000000000; return;
            case HashRateQuantity.PetaHs: value = 1000000000000000; return;
            case HashRateQuantity.ExaHs: value = 1000000000000000000; return;
            default:
                break;
        }
        throw new Exception("Should not be reach");
    }

    public static void HashValue(HashRateQuantity type, out decimal value)
    {
        switch (type)
        {
            case HashRateQuantity.HashPerSecond: value = 1; return;
            case HashRateQuantity.KiloHs: value = 1000; return;
            case HashRateQuantity.MegaHs: value = 1000000; return;
            case HashRateQuantity.GigaHs: value = 1000000000; return;
            case HashRateQuantity.TeraHs: value = 1000000000000; return;
            case HashRateQuantity.PetaHs: value = 1000000000000000; return;
            case HashRateQuantity.ExaHs: value = 1000000000000000000; return;
            default:
                break;
        }
        throw new Exception("Should not be reach");
    }

    public static string GetCloseTypeStringCompressionOf(HashRateBean dispalyRate)
    {
        dispalyRate.GetAsHashPerSecond(out decimal hashPerSecond);
        HashRateQuantity q= GetHashRateQuantityForHashValue(hashPerSecond);
        ApproximateConvert((double)hashPerSecond, out decimal toDisplay, HashRateQuantity.HashPerSecond, q);

        return string.Format(toDisplay + " " + q);



    }

    private static HashRateQuantity GetHashRateQuantityForHashValue(decimal hashPerSecond)
    {
        if (hashPerSecond >= 1000000000000000000) return HashRateQuantity.ExaHs;
        if (hashPerSecond >= 1000000000000000) return HashRateQuantity.PetaHs;
        if (hashPerSecond >= 1000000000000) return HashRateQuantity.TeraHs;
        if (hashPerSecond >= 1000000000) return HashRateQuantity.GigaHs;
        if (hashPerSecond >= 1000000) return HashRateQuantity.MegaHs;
        if (hashPerSecond >= 1000) return HashRateQuantity.KiloHs;
         return HashRateQuantity.HashPerSecond;
    }
}
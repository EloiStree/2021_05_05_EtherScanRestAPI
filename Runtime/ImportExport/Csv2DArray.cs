using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class Csv2DArray<T>
{

    //Max 2000
    public string[] m_columnsLabel;
    public string[] m_rawsLabel;
    public T[,] m_valueArray;

    public void SetSize(string[] columnsLabel, string[] rawsLabel)
    {
        m_columnsLabel = columnsLabel;
        m_rawsLabel = rawsLabel;
        m_valueArray = new T[m_columnsLabel.Length, m_rawsLabel.Length];
    }

    public void SetValue(uint columnIndex, uint rawIndex, T Value)
    {
        m_valueArray[columnIndex, rawIndex] = Value;
    }

    public T Get(uint columnIndex, uint rawIndex)
    {
        return m_valueArray[columnIndex, rawIndex];
    }
}
public class Csv1DArray<T>
{

    //Max 2000
    public string[] m_columnsLabel;
    public string[] m_rawsLabel;
    public T[] m_valueArray;

    public int GetWidth() { return m_columnsLabel.Length; }
    public int GetHeight() { return m_rawsLabel.Length; }

    public virtual void SetSize(string[] columnsLabel, string[] rawsLabel)
    {
        m_columnsLabel = columnsLabel;
        m_rawsLabel = rawsLabel;
        m_valueArray = new T[m_columnsLabel.Length* m_rawsLabel.Length];
    }

    public void SetValue(uint columnIndex, uint rawIndex, T Value)
    {
        m_valueArray[columnIndex + rawIndex* GetWidth()] = Value;
    }

    public T Get(uint columnIndex, uint rawIndex)
    {
        return m_valueArray[columnIndex + rawIndex * GetWidth()];
    }
    public string GetAsCsvText()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("#;");
        sb.Append( string.Join(";",m_columnsLabel) );
        sb.Append("\n");
        for (uint r = 0; r < GetHeight(); r++)
        {
            sb.Append(m_rawsLabel[r]);
            for (uint c = 0; c < GetWidth(); c++)
            {
                sb.Append(";");
                sb.Append(Get(c, r));
            }
            sb.Append("\n");
        }
        return sb.ToString();
    }
}

public class Csv1DArrayBufferableDouble : Csv1DArray<double>
{

    public ComputeBuffer m_buffer;

    public override void SetSize(string[] columnsLabel, string[] rawsLabel)
    {
        base.SetSize(columnsLabel, rawsLabel);
        m_buffer = new ComputeBuffer(columnsLabel.Length * rawsLabel.Length, sizeof(double), ComputeBufferType.Default);
    }
    public ulong GetMemorySizeUsed()
    {
        return ((ulong)m_columnsLabel.Length) * ((ulong)m_rawsLabel.Length) * ((ulong)sizeof(double));
    }

}
public class Csv1DArrayBufferableUint : Csv1DArray<uint>
{

    public ComputeBuffer m_buffer;

    public new void SetSize(string[] columnsLabel, string[] rawsLabel)
    {
        base.SetSize(columnsLabel, rawsLabel);
        m_buffer = new ComputeBuffer(columnsLabel.Length * rawsLabel.Length, sizeof(int), ComputeBufferType.Default);
    }
    public void ArrayToBuffer()
    {
        m_buffer.SetData(m_valueArray);
    }
    public void BufferToArray()
    {
        m_buffer.GetData(m_valueArray);
    }

    public ulong GetMemorySizeUsed()
    {
       return  ((ulong)m_columnsLabel.Length)* ((ulong)m_rawsLabel.Length) *((ulong)sizeof(int));
    }
}

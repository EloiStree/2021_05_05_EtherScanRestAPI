using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionsRangeMono : MonoBehaviour, ITransactionsRangeId
{

    public TransactionCursorMono m_from;
    public TransactionCursorMono m_to;

    public void GetFrom(out ITransactionId from)
    {
        from = m_from;
    }

    public void GetTo(out ITransactionId to)
    {
        to = m_to;
    }

    public void GetTransactions(out ITransactionId from, out ITransactionId to)
    {
        to = m_to;
        from = m_from;
    }

    public bool HasEndPoint()
    {
        return m_to!= null && m_to.IsDefined();
    }

    public bool HasStartEndPoint()
    {
        return HasStartPoint() && HasEndPoint();
    }

    public bool HasStartPoint()
    {
        return m_from != null && m_from.IsDefined();
    }

    public void SetFrom(ITransactionId from)
    {
         m_from.SetWith(from);
    }

    public void SetTo(ITransactionId to)
    {
        m_to.SetWith(to);
    }

}

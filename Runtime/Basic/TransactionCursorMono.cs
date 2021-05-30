using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionCursorMono : MonoBehaviour , ITransactionId
{
    public TransactionCursor m_cursor;
    public void GetBlockId(out string id)
    {
        m_cursor.GetBlockId(out id);
    }

    public void GetBlockId(out ulong id)
    {
        m_cursor.GetBlockId(out id);
    }

    public void GetTransactionId(out string id)
    {
        m_cursor.GetTransactionId(out id);
    }

    public bool IsDefined()
    {
        return m_cursor.IsDefined();
    }

    public void SetWith(ITransactionId toCopy)
    {
        m_cursor.SetWith(toCopy);
    }

    public void SetWithBlockId(string blockId)
    {
        m_cursor.SetWithBlockId(blockId);
    }

    public void SetWithTransactionId(string transactionId)
    {
        m_cursor.SetWithTransactionId(transactionId);
    }

}


public interface ITransactionsRangeId
{

    
    void GetTransactions(out ITransactionId from, out ITransactionId to);
    void GetFrom(out ITransactionId from);
    void GetTo(out ITransactionId to);
    void SetFrom(ITransactionId from);
    void SetTo(ITransactionId to);
    bool HasStartPoint();
    bool HasEndPoint();
    bool HasStartEndPoint();
}

[System.Serializable]
public class TransactionsRange : ITransactionsRangeId
{

    [SerializeField] TransactionCursor m_fromTransaction;
    [SerializeField] TransactionCursor m_toTransaction;

    public TransactionsRange(ITransactionId fromTransaction, ITransactionId toTransaction)
    {
        SetFrom(fromTransaction);
        SetTo(toTransaction);
    }

    public void GetTransactions(out ITransactionId from, out ITransactionId to)
    {
        from = m_fromTransaction;
        to = m_toTransaction;
    }
    public void GetFrom(out ITransactionId from)
    {
        from = m_fromTransaction;
    }
    public void GetTo(out ITransactionId to)
    {
        to = m_toTransaction;
    }

    public void SetFrom(ITransactionId from) => m_fromTransaction.SetWith(from);
    public void SetTo(ITransactionId to) => m_toTransaction .SetWith(to);

    public bool HasStartPoint() { return m_fromTransaction != null; }
    public bool HasEndPoint() { return m_toTransaction != null; }
    public bool HasStartEndPoint() { return HasStartPoint() && HasEndPoint(); }

}


public interface ITransactionId {

    void GetBlockId(out string id);
    void GetBlockId(out ulong id);
    void GetTransactionId(out string id);
    void SetWith(ITransactionId toCopy);
    void SetWithBlockId(string blockId);
    void SetWithTransactionId(string transactionId);
    bool IsDefined();
}



[System.Serializable]
public class TransactionCursor : ITransactionId {

    [SerializeField] string m_blockId;
    [SerializeField] string m_transcationId;

    public TransactionCursor(string blockId, string transcationId)
    {
        m_blockId = blockId;
        m_transcationId = transcationId;
    }

    public void GetBlockId(out string id)
    {
        id = m_blockId;
    }

    public void GetBlockId(out ulong id)
    {
        ulong.TryParse(m_blockId, out id);
    }

    public void GetTransactionId(out string id)
    {
        id = m_transcationId;
    }

    public bool IsDefined()
    {
        return !string.IsNullOrEmpty(m_blockId) && !string.IsNullOrEmpty(m_transcationId);
    }

    public void SetWith(ITransactionId toCopy)
    {
        if(toCopy!=null)
        {
            toCopy.GetBlockId(out string bid);
            SetWithBlockId(bid);
            toCopy.GetTransactionId(out string tid);
            SetWithBlockId(tid);

        }
    }

    public void SetWithBlockId(string blockId)
    {
        m_blockId = blockId;
    }

    public void SetWithTransactionId(string transactionId)
    {
        m_transcationId = transactionId;
    }
}

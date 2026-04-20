using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IItemReceiver
{
    private int _itemCount;

    public event Action<int> OnItemCountChanged;

    void IItemReceiver.OnCollect()
    {
        _itemCount++;
        OnItemCountChanged?.Invoke(_itemCount);
    }
}

internal interface IItemReceiver
{
    void OnCollect();
}

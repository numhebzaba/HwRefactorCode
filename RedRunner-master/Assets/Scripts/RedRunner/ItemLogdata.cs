using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ItemLogdata<T> : MonoBehaviour
{
    public string itemName;
    public int itemPrice;
    public string itemDescription;
    public int itemlevel;

    protected T data;
    public void SetItemData(T data)
    {
        this.data = data;
        UpdateLog();
    }

    protected abstract void UpdateLog();


}
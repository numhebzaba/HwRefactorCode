using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class gloveItem : ItemLogdata<GloveItemData>
{
    public GloveItemData glove;
    protected override void UpdateLog()
    {

        itemName = data.GloveName;
        itemDescription = "price : " + data.price + " description : " + data.description;

        Debug.Log(itemName);
        Debug.Log(itemDescription);
    }
    private void Start()
    {
        SetItemData(glove);
    }
}

[System.Serializable]
public class GloveItemData
{
    public string GloveName;
    public int price;
    public string description;
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class HatItem : ItemLogdata<HatItemData>
{
   public HatItemData hat;
    protected override void UpdateLog()
    {
       
        itemName = data.HatName;
        itemDescription = "price : " + data.price + " description : " + data.description;

        Debug.Log(itemName);
        Debug.Log(itemDescription);
    }
    private void Start()
    {
        SetItemData(hat);
    }
}

[System.Serializable]
public class HatItemData
{
    public string HatName;
    public int price;
    public string description;
}
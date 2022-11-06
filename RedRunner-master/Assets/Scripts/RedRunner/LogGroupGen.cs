using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class LogGroupGen<T> : MonoBehaviour
{
    
    [SerializeField] protected List<ItemLogdata<T>> infoItemList = new List<ItemLogdata<T>>();
    [SerializeField] protected T[] datas;
    public ItemLogdata<T> info;

    void Start()
    {
        foreach (var data in datas)
        {

            info.SetItemData(data);
            infoItemList.Add(info);
            
        }
    }

}
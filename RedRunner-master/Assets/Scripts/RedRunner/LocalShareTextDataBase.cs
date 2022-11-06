using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;
using System.Linq;

public class LocalShareTextDataBase : ShareTextDataBase
{
    [SerializeField] TextAsset jsonFile;
    private void Start()
    {
        SortByLowestOrder();
        SortByHighestOrder();
        SortByAToZ();
        FilterActiveOnly();
        FilterNonActiveOnly();
    }
    public override void PrepareDatas()
    {
        var jsonObject = new JSONObject(jsonFile.text);
        foreach (var json in jsonObject.list)
        {
            var platformName = "";
            json.GetField(ref platformName, "platform");
            var link = "";
            json.GetField(ref link, "link");
            var order = 0;
            json.GetField(ref order, "order");
            var active = "";
            json.GetField(ref active, "active");

            var newShareTextData = new ShareTextData();
            newShareTextData.name = platformName;
            newShareTextData.link = link;
            newShareTextData.order = order;
            newShareTextData.active = active;

            ShareTextDataList.Add(newShareTextData);

        }

        Debug.Log(ShareTextDataList[0]);
        Debug.Log(ShareTextDataList[1]);
        Debug.Log(ShareTextDataList[2]);
    }
    void LogData(ShareTextData[] datas)
    {
        for(int i = 0; i < datas.Length; i++)
        {
            Debug.Log(datas[i].name + " order " + datas[i].order);
        }
    }
    public void SortByLowestOrder()
    {
        ShareTextData[] sortedShareTextDatas = ShareTextDataList.OrderBy(Data => Data.order).ToArray();
        LogData(sortedShareTextDatas);
        Debug.Log("SortByLowestOrder");
        Debug.Log("-----------------------------------------------------");
    }
    public void SortByHighestOrder()
    {
        ShareTextData[] sortedShareTextDatas = ShareTextDataList.OrderByDescending(Data => Data.order).ToArray();
        LogData(sortedShareTextDatas);
        Debug.Log("SortByHighestOrder");
        Debug.Log("-----------------------------------------------------");

    }
    public void SortByAToZ()
    {
        ShareTextData[] sortedShareTextDatas = ShareTextDataList
        .OrderBy(data => data.name)
        .ThenBy(data => data.active)
        .ToArray();
        LogData(sortedShareTextDatas);
        Debug.Log("SortByAToZ");
        Debug.Log("-----------------------------------------------------");
    }
    public void FilterActiveOnly()
    {
        ShareTextData[] sortedShareTextDatas = ShareTextDataList.Where(data => data.active == "yes").ToArray();
        LogData(sortedShareTextDatas);
        Debug.Log("FilterActiveOnly");
        Debug.Log("-----------------------------------------------------");
    }

    public void FilterNonActiveOnly()
    {
        ShareTextData[] sortedShareTextDatas = ShareTextDataList.Where(data => data.active == "no").ToArray();
        LogData(sortedShareTextDatas);
        Debug.Log("FilterNonActiveOnly");
        Debug.Log("-----------------------------------------------------");
    }
}

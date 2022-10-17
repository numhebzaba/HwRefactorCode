using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public class LocalShareTextDataBase : ShareTextDataBase
{
    [SerializeField] TextAsset jsonFile;

    public override void PrepareDatas()
    {
        var jsonObject = new JSONObject(jsonFile.text);
        foreach (var json in jsonObject.list)
        {
            var platformName = "";
            json.GetField(ref platformName, "platform");

            var link = "";
            json.GetField(ref link, "link");

            var newShareTextData = new ShareTextData();
            newShareTextData.name = platformName;
            newShareTextData.link = link;

            ShareTextDataList.Add(newShareTextData);

        }

        Debug.Log(ShareTextDataList[0]);
        Debug.Log(ShareTextDataList[1]);
        Debug.Log(ShareTextDataList[2]);
    }
}

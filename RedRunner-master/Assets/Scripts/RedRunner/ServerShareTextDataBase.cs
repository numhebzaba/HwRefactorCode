using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;

public class ServerShareTextDataBase : ShareTextDataBase
{
    [SerializeField] string url;
    public override void PrepareDatas()
    {
        StartCoroutine(DownloadShareTextDataBase());
    }
    IEnumerator DownloadShareTextDataBase()
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        var dowloadedText = webRequest.downloadHandler.text;

        var jsonObject = new JSONObject(dowloadedText);
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

    }
}

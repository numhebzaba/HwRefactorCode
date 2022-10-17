using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public abstract class ShareTextDataBase : MonoBehaviour
{
    [SerializeField] protected List<ShareTextData> ShareTextDataList = new List<ShareTextData>();
    protected ShareTextData defaultText;

    private void Awake()
    {
        PrepareDatas();
    }
    public abstract void PrepareDatas();

    public ShareTextData GetDefaultData()
    {
        return defaultText;
    }
    public ShareTextData GetShareTextDataByName(string name)
    {
        foreach (var ShareTextData in ShareTextDataList)
        {
            if (name.Contains(ShareTextData.name))
                return ShareTextData;
        }
        return null;
    }
}

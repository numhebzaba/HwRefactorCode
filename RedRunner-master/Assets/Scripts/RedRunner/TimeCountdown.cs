using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using System;

[Serializable]
public class TimeCountdown : MonoBehaviour
{
    public TimeSpan TimeCount = new TimeSpan(1, 0, 0, 0);
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (TimeCount.TotalSeconds > 0)
            TimeCount -= TimeSpan.FromSeconds(Time.deltaTime);
        Debug.Log(TimeCount);
    }
}

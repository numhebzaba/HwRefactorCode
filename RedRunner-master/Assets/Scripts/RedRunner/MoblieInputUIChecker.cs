using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoblieInputUIChecker : InputUIChecker
{
    public override bool IsCancle()
    {
        return Input.touchCount == 3;
    }

    public override bool IsHold()
    {
        return Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    public override bool IsRelease()
    {
        return Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
    }

  
}

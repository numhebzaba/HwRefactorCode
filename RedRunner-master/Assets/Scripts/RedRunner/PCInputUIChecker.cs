using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputUIChecker : InputUIChecker
{
    public override bool IsCancle()
    {
        return Input.GetButtonDown("Cancel");
    }
    public override bool IsHold()
    {
        return Input.GetMouseButtonDown(0);
    }
    public override bool IsRelease()
    {
        return Input.GetMouseButtonUp(0);
    }
}

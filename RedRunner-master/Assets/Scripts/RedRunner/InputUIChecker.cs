using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputUIChecker : MonoBehaviour
{
    public abstract bool IsCancle();
    public abstract bool IsHold();
    public abstract bool IsRelease();

}

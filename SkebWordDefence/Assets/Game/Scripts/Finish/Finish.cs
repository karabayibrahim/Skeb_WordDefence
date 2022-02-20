using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Finish : MonoBehaviour, ICollectable
{
    public static Action FinishAction;
    public void DoCollect()
    {
        FinishAction?.Invoke();
    }
}

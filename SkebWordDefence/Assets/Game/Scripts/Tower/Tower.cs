using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tower : MonoBehaviour
{
   // public GameObject LeftTower;
    public List<NpcShotController> MyGuards = new List<NpcShotController>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GuardPositionFixed()
    {
        foreach (var item in gameObject.GetComponentsInChildren<NpcShotController>())
        {
            MyGuards.Add(item);
        }
        foreach (var item in MyGuards)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, 2.2f, item.transform.localPosition.z);
        }
    }
}

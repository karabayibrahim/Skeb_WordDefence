using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSystem : MonoBehaviour
{
    public List<NpcShotController> Shooters = new List<NpcShotController>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveNpc(NpcController _npc)
    {
        foreach (var item in Shooters)
        {
            item.Enemys.Remove(_npc);
            if (item.Enemys.Count>0)
            {
                item.TargetMethod();
            }
        }
    }
}

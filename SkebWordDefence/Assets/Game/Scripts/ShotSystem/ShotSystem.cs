using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSystem : MonoBehaviour
{
    public List<NpcShotController> Shooters = new List<NpcShotController>();
    public List<LaserControl> LaserShooters = new List<LaserControl>();
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
            if (item!=null)
            {
                item.Enemys.Remove(_npc);
                if (item.Enemys.Count > 0)
                {
                    item.TargetMethod();
                }
            }
            
        }
        foreach (var item in LaserShooters)
        {
            if (item != null)
            {
                item.Enemys.Remove(_npc);
                if (item.Enemys.Count > 0)
                {
                    item.TargetMethod();
                }
            }

        }

    }
}

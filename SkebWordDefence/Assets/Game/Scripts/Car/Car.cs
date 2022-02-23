using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject Particle1;
    public GameObject Particle2;
    public GameObject RRight;
    public GameObject RLeft;
    public bool Turn = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Turn==true)
        {
            RLeft.transform.Rotate(1000f*Time.deltaTime, 0, 0);
            RRight.transform.Rotate(1000f*Time.deltaTime, 0, 0);
        }
    }
}

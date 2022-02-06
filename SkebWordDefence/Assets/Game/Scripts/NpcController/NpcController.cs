using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private float _speed = 5f;


    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            if (Speed == value)
            {
                return;
            }
            _speed = value;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveSystem();
    }

    private void MoveSystem()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }
}

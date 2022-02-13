using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TowerG : MonoBehaviour
{
    private float _healt = 100f;


    public float Healt
    {
        get
        {
            return _healt;
        }
        set
        {
            if (Healt == value)
            {
                return;
            }
            _healt = value;
            HealtChanhed();
        }
    }


    private void HealtChanhed()
    {
        if (Healt <= 0)
        {
            transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

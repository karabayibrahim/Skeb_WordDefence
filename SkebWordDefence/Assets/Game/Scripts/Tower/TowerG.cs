using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TowerG : MonoBehaviour
{
    private float _healt = 100f;
    private float _speed = 7f;
    public TowerType MyType;
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
            Destroy(gameObject);
            //transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
            //{
            //});
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -_speed * Time.deltaTime);
    }
}

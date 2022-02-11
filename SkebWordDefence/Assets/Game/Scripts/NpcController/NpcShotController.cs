using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NpcShotController : MonoBehaviour
{
    private NpcState _npcState;
    private Animator _anim;
    private Transform _targetObj;
    public List<NpcController> Enemys = new List<NpcController>();
    public GameObject Bullet;



    public NpcState NpcState
    {
        get
        {
            return _npcState;
        }
        set
        {
            if (NpcState == value)
            {
                return;
            }
            _npcState = value;
            OnStateChange();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NpcController>()!=null)
        {
            Debug.Log("Düşman");
            Enemys.Add(other.gameObject.GetComponent<NpcController>());
            TargetMethod();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<NpcController>() != null)
        {
            Enemys.Remove(other.gameObject.GetComponent<NpcController>());
            //TargetMethod();
        }
    }


    private void OnStateChange()
    {
        switch (NpcState)
        {
            case NpcState.IDLE:
                _anim.CrossFade("Idle", 0.05f);
                break;
            case NpcState.RUN:
                _anim.CrossFade("Run", 0.05f);
                break;
            case NpcState.SHOT:
                _anim.CrossFade("ShotRifle", 0.05f);
                break;
            default:
                break;
        }
    }
    void Start()
    {
        _anim = GetComponent<Animator>();
        GameManager.Instance.ShotSystem.Shooters.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemys.Count==0)
        {
            NpcState = NpcState.IDLE;
        }
    }

    public void TargetMethod()
    {
        var index = Random.Range(0, Enemys.Count);
        var targetObj = Enemys[index];
        _targetObj = targetObj.transform;
        gameObject.transform.DOLookAt(targetObj.transform.position, 0.1f);
        NpcState = NpcState.SHOT;
    }

    public void ShotMethod()
    {
        var newBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
        newBullet.transform.DOLookAt(_targetObj.transform.position, 0.01f);
        newBullet.transform.DOMove(_targetObj.transform.position, 0.5f);
    }
}

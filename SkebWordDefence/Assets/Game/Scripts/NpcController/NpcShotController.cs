using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NpcShotController : MonoBehaviour
{
    private NpcState _npcState;
    private int _enemyCount;
    private Animator _anim;
    private Transform _targetObj;
    private Tween BulletTween;
    public List<NpcController> Enemys = new List<NpcController>();
    public GameObject Bullet;
    public Transform StartShootPoz;
    public bool AttackStatus = false;
    private float fireRate = 0.1f;
    private float lastShot = 0;

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
        if (other.gameObject.GetComponent<NpcController>() != null)
        {
            Enemys.Add(other.gameObject.GetComponent<NpcController>());
            TargetMethod();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<NpcController>() != null)
        {
            Enemys.Remove(other.gameObject.GetComponent<NpcController>());
            BulletTween.Kill(true);
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
        transform.localPosition = new Vector3(transform.localPosition.x, 2.2f, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemys.Count <= 0)
        {
            NpcState = NpcState.IDLE;
        }
    }

    public void TargetMethod()
    {
        if (gameObject!=null)
        {
            NpcRemoveList();
            var index = Random.Range(0, Enemys.Count);
            var targetObj = Enemys[index];
            _targetObj = targetObj.transform;
            //gameObject.transform.DOLookAt(new Vector3(targetObj.transform.position.x, transform.position.y, targetObj.transform.position.z), 0.1f);
            transform.LookAt(new Vector3(targetObj.transform.position.x, transform.position.y, targetObj.transform.position.z));
            NpcState = NpcState.SHOT;
        }
    }

    public void ShotMethod()
    {
        if (Time.time > fireRate + lastShot)
        {
            lastShot = Time.time;
            var newBullet = Instantiate(Bullet, StartShootPoz.position, Quaternion.identity);
            newBullet.transform.LookAt(_targetObj.transform.position);
            BulletTween = newBullet.transform.DOMove(_targetObj.transform.position, 0.5f);
        }
        
    }

    public void NpcRemoveList()
    {
        for (var i = Enemys.Count - 1; i > -1; i--)
        {
            if (Enemys[i] == null)
                Enemys.RemoveAt(i);
        }
    }
}

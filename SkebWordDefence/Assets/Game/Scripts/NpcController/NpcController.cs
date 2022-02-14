using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NpcController : MonoBehaviour
{
    public float _speed = 8f;
    private NpcState _npcState;
    private Animator _anim;
    private bool _attackControl = false;
    private bool _deadStatus = false;

    public TowerG AttackTower;
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

    private void OnStateChange()
    {
        switch (NpcState)
        {
            case NpcState.IDLE:
                _anim.CrossFade("Idle", 0.05f);
                break;
            case NpcState.RUN:
                 Speed = 8;
                _anim.CrossFade("Run", 0.05f);
                break;
            case NpcState.ATTACK:
                _attackControl = true;
                Speed = 6.5f;
                _anim.CrossFade("Attack", 0.05f);
                _anim.SetLayerWeight(1, 1);
                break;
            case NpcState.DEAD:
                _deadStatus = true;
                Speed = 0f;
                _anim.CrossFade("Dead", 0.05f);
                break;
            default:
                break;
        }
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetFloat("Offset", UnityEngine.Random.Range(0, 1f));
        NpcState = NpcState.RUN;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSystem();
        PlayerControl();
        if (AttackTower==null&&!_deadStatus)
        {
            NpcState = NpcState.RUN;
            _anim.SetLayerWeight(1, 0);
        }
        //Debug.Log(Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position));
    }

    private void MoveSystem()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    public void AttackTowerDamage()
    {
        switch (AttackTower.MyType)
        {
            case TowerType.WOOD:
                if (AttackTower != null)
                {
                    AttackTower.Healt -= 20f;
                }
                else
                {
                    NpcState = NpcState.RUN;
                }
                break;
            case TowerType.STONE:
                if (AttackTower!=null)
                {
                    AttackTower.Healt -= 10f;
                }
                else
                {
                    NpcState = NpcState.RUN;
                }
                break;
            case TowerType.CONCREATE:
                if (AttackTower != null)
                {
                    AttackTower.Healt -= 7f;
                }
                else
                {
                    NpcState = NpcState.RUN;
                }
                break;
            case TowerType.LASER:
                if (AttackTower != null)
                {
                    AttackTower.Healt -= 5f;
                }
                else
                {
                    NpcState = NpcState.RUN;
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            AttackTower = other.GetComponent<TowerG>();
            NpcState = NpcState.ATTACK;
        }
    }

    private void PlayerControl()
    {
        if (!_attackControl&&!_deadStatus)
        {
            if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) < 5f)
            {
                Speed = 7f;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) > 20f&& Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) < 30f)
            {
                Speed = 10f;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) > 30f)
            {
                Speed = 10f;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) < 10f)
            {
                Speed = 8f;
                //transform.DOLookAt(GameManager.Instance.Player.transform.position, 0.5f);
                transform.LookAt(GameManager.Instance.Player.transform.position);
            }
            //else
            //{
            //    Speed = 8f;
            //}
            
        }

    }
}

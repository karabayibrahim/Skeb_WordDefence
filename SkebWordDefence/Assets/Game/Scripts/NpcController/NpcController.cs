﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NpcController : MonoBehaviour
{
    public float _speed = 8f;
    private NpcState _npcState;
    private Animator _anim;
    private float rnd;
    [SerializeField] private bool lineControl = false;
    private bool _attackControl = false;
    public bool _deadStatus = false;

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
            if (Speed==0)
            {
                _deadStatus = true;
            }
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
                _deadStatus = true;
                _attackControl = false;
                Speed = 0;
                _anim.CrossFade("Idle", 0.05f);
                _anim.SetLayerWeight(1, 0);
                break;
            case NpcState.RUN:
                 Speed = 14;
                _anim.CrossFade("Run", 0.05f);
                _anim.SetLayerWeight(1, 0);
                break;
            case NpcState.ATTACK:
                _attackControl = true;
                Speed = 12f;
                _anim.CrossFade("Attack", 0.05f);
                _anim.SetLayerWeight(1, 1);
                break;
            case NpcState.DEAD:
                _deadStatus = true;
                Speed = 0f;
                _anim.CrossFade("Dead", 0.05f);
                _anim.SetLayerWeight(1, 0);
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
        InvokeRepeating("ZigZag", 2f,3f);
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
        LineMove();
        
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
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.Player.PlayerState = PlayerState.DEAD;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            _attackControl = false;
        }
    }

    private void PlayerControl()
    {
        if (!_attackControl&&!_deadStatus)
        {
            if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) < 5f)
            {
                Speed = 13f;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) > 20f&& Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) < 30f)
            {
                Speed = 16f;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) > 30f)
            {
                Speed = 16f;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) < 15f)
            {
                Speed = 15f;
                //transform.DOLookAt(GameManager.Instance.Player.transform.position, 0.5f);
                var player = GameManager.Instance.Player;
                var looPos = new Vector3(player.transform.position.x,transform.position.y, player.transform.position.z);
                //Vector3 dir = player.transform.position - transform.position;
                //transform.position += new Vector3(Time.deltaTime * dir.x, transform.position.y, Time.deltaTime * dir.z);
                transform.DOLookAt(looPos, 0.5f);
            }
            //else
            //{
            //    Speed = 8f;
            //}

        }

    }

    private void LineMove()
    {
        if (transform.position.x < -16f || transform.position.x > 16f)
        {
            lineControl = true;
            //Debug.Log("AlanDışı");
            var lookPos = new Vector3(transform.position.x - rnd, transform.position.y, transform.position.z - 10f);
            transform.LookAt(lookPos);

        }
        else
        {
            lineControl = false;
        }
    }
    
    private void ZigZag()
    {
        if (!lineControl)
        {
            rnd = UnityEngine.Random.Range(-2, 2);
            var lookPos = new Vector3(transform.position.x + rnd, transform.position.y, transform.position.z - 10f);
            transform.LookAt(lookPos);
        }
        
        

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private float _speed = 8f;
    private NpcState _npcState;
    private Animator _anim;
    private bool _attackControl = false;
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
            if (NpcState==value)
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
                _anim.CrossFade("Run", 0.05f);
                Speed = 8f;
                break;
            case NpcState.ATTACK:
                _attackControl = true;
                Speed = 0f;
                _anim.CrossFade("Attack", 0.05f);
                break;
            default:
                break;
        }
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        NpcState = NpcState.RUN;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSystem();
        PlayerControl();
        //Debug.Log(Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position));
    }

    private void MoveSystem()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Tower")
        {
            NpcState = NpcState.ATTACK;
        }
    }

    private void PlayerControl()
    {
        if (!_attackControl)
        {
            if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) < 3f)
            {
                Speed = 7f;
            }
            else if (Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) > 20f)
            {
                Speed = 10f;
            }
            else
            {
                Speed = 8f;
            }
        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed=-7f;
    private PlayerState _playerState;
    private Animator _anim;

    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            if (Speed==value)
            {
                return;
            }
            _speed = value;
        }
    }

    public PlayerState PlayerState
    {
        get
        {
            return _playerState;
        }
        set
        {
            if (PlayerState==value)
            {
                return;
            }
            _playerState = value;
            OnStateChanged();
        }
    }

    private void OnStateChanged()
    {
        switch (PlayerState)
        {
            case PlayerState.IDLE:
                _anim.CrossFade("Idle", 0.05f);
                break;
            case PlayerState.RUN:
                _anim.CrossFade("Run", 0.05f);
                break;
            default:
                break;
        }
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        PlayerState = PlayerState.RUN;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSystem();
    }

    private void MoveSystem()
    {
        transform.Translate(0, 0, -_speed * Time.deltaTime);
    }

    public void AnswerStatus(int _wordCount)
    {
        //GameManager.Instance.SpawnManager.StoneSpawn(_wordCount,transform.position.z + 1f);
    }
}

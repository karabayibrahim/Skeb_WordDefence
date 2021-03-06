using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed=-12f;
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
            case PlayerState.WIN:
                Speed = 0f;
                _anim.CrossFade("Jump", 0.05f);
                break;
            case PlayerState.DEAD:
                Speed = 0f;
                _anim.CrossFade("Dead", 0.1f);
                GameManager.Instance.GameState = GameState.FAIL;
                break;

            default:
                break;
        }
    }

    void Start()
    {
        PlayerPos();
        Finish.FinishAction += FinishStatus;
        _anim = GetComponent<Animator>();
        PlayerState = PlayerState.RUN;
    }

    private void OnDisable()
    {
        Finish.FinishAction -= FinishStatus;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSystem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ICollectable>()!=null)
        {
            other.GetComponent<ICollectable>().DoCollect();
        }
    }
    private void MoveSystem()
    {
        transform.Translate(0, 0, -_speed * Time.deltaTime);
    }

    //public void AnswerStatus(int _wordCount)
    //{
    //    //GameManager.Instance.SpawnManager.StoneSpawn(_wordCount,transform.position.z + 1f);
    //}
    private void FinishStatus()
    {
        PlayerState = PlayerState.WIN;
    }

    public void FinishAnimation()
    {
        GameManager.Instance.FinalCar.GetComponent<Animator>().CrossFade("FinalCar", 0.01f);
        GameManager.Instance.FinalCar.Particle1.SetActive(true);
        GameManager.Instance.FinalCar.Particle2.SetActive(true);
        GameManager.Instance.FinalCar.Turn = true;
        gameObject.SetActive(false);
    }

    private void PlayerPos()
    {
        if (PlayerPrefs.GetInt("LevelIndex")>1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + PlayerPrefs.GetInt("LevelIndex") * 2);
        }
    }
}

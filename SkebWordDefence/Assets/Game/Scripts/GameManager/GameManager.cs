using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class GameManager : MonoSingleton<GameManager>
{
    public PlayerController Player;
    public SpawnManager SpawnManager;
    public ShotSystem ShotSystem;
    public AnswerDataPack AnswerDataPack;
    public QuesTion QuestionData;
    public UIManager UIManager;
    public Car FinalCar;
    public CinemachineVirtualCamera LevelCam;
    public List<GameObject> NpcList = new List<GameObject>();
    public List<GameObject> Particles = new List<GameObject>();
    public int SpawnCount;
    public int WordCount;
    public bool FirstSpawn = false;

    private GameState _gameState;

    [SerializeField] private int _levelIndex = 0;
    void Awake()
    {
        _levelIndex = PlayerPrefs.GetInt("LevelIndex");
        Debug.Log(LevelIndex);
        InvokeRepeating("SpawnNpc", 0, 10f);
        Finish.FinishAction += FinishStatus;

    }

    private void OnDisable()
    {
        Finish.FinishAction -= FinishStatus;
    }

    public GameState GameState
    {
        get
        {
            return _gameState;
        }
        set
        {
            if (GameState==value)
            {
                return;
            }
            _gameState = value;
            OnGameStateChanged();
        }
    }

    private void OnGameStateChanged()
    {
        switch (GameState)
        {
            case GameState.START:
                break;
            case GameState.FAIL:
                CancelInvoke();
                foreach (var item in NpcList)
                {
                    item.GetComponent<NpcController>().NpcState = NpcState.IDLE;
                }
                UIManager.FailStatus();
                break;
            case GameState.WIN:
                UIManager.WinStatus();
                break;
            default:
                break;
        }
    }


    public int LevelIndex
    {
        get
        {
            return _levelIndex;
        }
        set
        {
            if (LevelIndex==value)
            {
                return;
            }
            _levelIndex = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SpawnManager.TowerSpawn(WordCount, Player.transform.position.z + 5f);
        }
        if (NpcList.Count<SpawnCount/2f)
        {
            for (int i = 0; i < SpawnCount; i++)
            {
                SpawnManager.SpawnObjectMethod();
            }
        }
    }

    public void SpawnNpc()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnManager.SpawnObjectMethod();
        }
    }

    private void FinishStatus()
    {
        LevelCam.Follow = null;
        LevelCam.LookAt = null;
        CancelInvoke();
    }
}

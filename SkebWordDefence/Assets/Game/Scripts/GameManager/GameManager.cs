using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoSingleton<GameManager>
{
    public PlayerController Player;
    public SpawnManager SpawnManager;
    public ShotSystem ShotSystem;
    public AnswerDataPack AnswerDataPack;
    public QuesTion QuestionData;
    public UIManager UIManager;
    public List<GameObject> NpcList = new List<GameObject>();
    public List<GameObject> Particles = new List<GameObject>();
    public int SpawnCount;
    public int WordCount;
    public bool FirstSpawn = false;

    private GameState _gameState;

    [SerializeField] private int _levelIndex = 0;
    void Start()
    {
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnManager.SpawnObjectMethod();
        }

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
                foreach (var item in NpcList)
                {
                    item.GetComponent<NpcController>().NpcState = NpcState.IDLE;
                }
                break;
            case GameState.WIN:
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
}

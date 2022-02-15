using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerController Player;
    public SpawnManager SpawnManager;
    public ShotSystem ShotSystem;
    public AnswerData AnswerData;
    public UIManager UIManager;
    public List<GameObject> NpcList = new List<GameObject>();
    public int SpawnCount;
    public int WordCount;
    public bool FirstSpawn = false;
    void Start()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnManager.SpawnObjectMethod();
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

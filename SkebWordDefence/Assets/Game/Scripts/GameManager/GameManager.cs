using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerController Player;
    public SpawnManager SpawnManager;
    public ShotSystem ShotSystem;
    public AnswerData AnswerData;
    public int SpawnCount;
    public int WordCount;
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
            
        }
    }
}

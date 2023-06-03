using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class MonsterSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] MonsterPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseMonster = 8;
    [SerializeField] private float MonsterPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private GameObject HP;

    [Header("Events")]
    public static UnityEvent onMonsterDestroy = new UnityEvent();



    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int monsterAlive;
    private int MonsterLeftToSpawn;
    private int monster = 0;
    private bool isSpawning = false;

    private void Awake()
    {
        onMonsterDestroy.AddListener(MonsterDestroyed);
    }
    private void Start()
    {
        StartCoroutine(StartWave());
    }


    private void Update()
    {
        if (!isSpawning)
        {
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f/MonsterPerSecond) && MonsterLeftToSpawn > 0)
        {
            SpawnMonster();
            MonsterLeftToSpawn--;
            monsterAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(monsterAlive== 0 && MonsterLeftToSpawn ==0)
        {
            EndWave();
        }
    }

    private void MonsterDestroyed()
    {
        monsterAlive--;
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        MonsterLeftToSpawn = MonsterPerWave();
    }

    private void EndWave()
    {
        monster++;
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnMonster()
    {
        if(monster > MonsterPrefabs.Length)
        {
            monster = 0;
        }
        GameObject prefabToWpawn = MonsterPrefabs[monster];
        Instantiate(prefabToWpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        
    }

    private int MonsterPerWave()
    {
        return Mathf.RoundToInt(baseMonster * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}

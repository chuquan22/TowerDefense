using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonsterSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] MonsterPrefabs;
    [SerializeField] public Text txtHealth;
    [SerializeField] public Button btnPrice;
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
    public static int price = 0;
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
        if(Player.currentHealth > 0)
        {
            txtHealth.text = Player.currentHealth.ToString();
        }
          // Debug.Log("Health: " + Player.currentHealth);
          // if player dead
        if (Player.isDead())
        {
            LoadScene();
        }
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

        if(Monster.isMonsterDestroyed)
        {
            price = price + Monster.BONUS_PRICE_MONSTER;
            TMP_Text txtPrice = btnPrice.GetComponentInChildren<TMP_Text>(true);
            txtPrice.text = price.ToString();
            //txtPrice.text = Monster.price.ToString();
            Monster.isMonsterDestroyed = false;
        }

        if (MonsterFly.isMonsterFlyDestroyed)
        {
            price = price + MonsterFly.BONUS_PRICE_MONSTER_FLY;
            TMP_Text txtPrice = btnPrice.GetComponentInChildren<TMP_Text>(true);
            txtPrice.text = price.ToString();
            //txtPrice.text = Monster.price.ToString();
            MonsterFly.isMonsterFlyDestroyed = false;
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

    private void LoadScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}

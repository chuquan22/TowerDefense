using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] MonsterPrefabs;
    [SerializeField] public Text txtHealth;
    [SerializeField] public Button btnPrice;
    public static TMP_Text txtPrice;
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
    public static int price = 30;
    private void Awake()
    {
        onMonsterDestroy.AddListener(MonsterDestroyed);
        txtPrice = btnPrice.GetComponentInChildren<TMP_Text>(true);
        txtPrice.text = price.ToString();
    }
    private void Start()
    {
        StartCoroutine(StartWave());
       
        
    }


    private void Update()
    {
                         /*
                 *      Player.currentHealth--;
                spawner.txtHealth.text = Player.currentHealth.ToString();
                Debug.Log("Health: " + Player.currentHealth);
                // if player dead
                if (Player.isDead())
                {
                    Invoke("LoadScene", 2f);
                }
                 */
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
            txtPrice.text =  price.ToString();
            //txtPrice.text = Monster.price.ToString();
            Monster.isMonsterDestroyed = false;
        }

        if (MonsterFly.isMonsterFlyDestroyed)
        {
            price = price + MonsterFly.BONUS_PRICE_MONSTER_FLY;
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
        Debug.Log("monster "+monster);
        GameObject prefabToWpawn;
        if (monster == MonsterPrefabs.Length -1)
        {
            monster = 0;
        }
        

        if (MonsterLeftToSpawn <= currentWave)
        {
            prefabToWpawn = MonsterPrefabs[monster + 1];
        }
        else
        {
            prefabToWpawn = MonsterPrefabs[monster];
        }
        Instantiate(prefabToWpawn, LevelManager.main.startPoint.GetComponent<Transform>().position, Quaternion.identity);
        
    }

    private int MonsterPerWave()
    {
        return Mathf.RoundToInt(baseMonster * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}

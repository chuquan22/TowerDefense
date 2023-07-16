using Assets.Script;
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
    [SerializeField] private Monster[] MonsterPrefabs;
    [SerializeField] private Button btnPause;
    [SerializeField] private Text textPrice;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Button btnWave;
    [Header("Attributes")]
    [SerializeField] private int baseMonster = 8;
    [SerializeField] private float MonsterPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private GameObject HP;

    [Header("Events")]
    public static UnityEvent onMonsterDestroy = new UnityEvent();

    public static int currentWave = 1;
    private float timeSinceLastSpawn;
    private int monsterAlive;
    private int MonsterLeftToSpawn;
    private int monster = 0;
    private bool isSpawning = false;
    public static int price = 30;
    private readonly int MAX_INDEX_MONSTER = 4;
    private int index;
    public static bool isTowerSold = false;
    public static bool isUpgrade = false;
    public static bool isTowerBought = false;
    private TMP_Text textWave;

    public GameObject pauseMenu;

    int totalMonster = 0;
    private void Awake()
    {
        onMonsterDestroy.AddListener(MonsterDestroyed);
        textPrice.text = price.ToString();
        index = MAX_INDEX_MONSTER;
        textWave = btnWave.GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        StartCoroutine(StartWave());
        btnPause.onClick.AddListener(LoadPauseScene);
        textWave.text = "Wave: " + currentWave;
    }

    public void LoadPauseScene()
    {
        pauseMenu.GetComponent<GamePause>().SetPause();
        // SceneManager.LoadScene("PauseGame");
    }

    public void ResetGame()
    {
        price = 30;
        Monster.maxHP = 30;
        index = MAX_INDEX_MONSTER;
        currentWave = 1;

        foreach (GameObject h in hearts)
        {
            h.SetActive(true);
        }
    }
    private void Update()
    {
        // if monster passed
        if (Monster.isPassed)
        {
            if (index >= 0)
            {
                hearts[index].SetActive(false);
                index--;
            }
            Monster.isPassed = false;
        }

        // if player sell tower
        if (isTowerSold)
        {
            textPrice.text = price.ToString();
            isTowerSold = false;
        }

        // if player upgrade tower
        if (isUpgrade)
        {
            textPrice.text = price.ToString();
            isUpgrade = false;
        }

        // if player buy tower
        if (isTowerBought)
        {
            textPrice.text = price.ToString();
            isTowerBought = false;
        }

        // if player dead
        if (Player.isDead())
        {
            LoadGameOverScene();
        }

        // if pause game
        if (pauseMenu.GetComponent<GamePause>().isPaused)
        {
            btnPause.gameObject.SetActive(false);
        }
        else
        {
            btnPause.gameObject.SetActive(true);
        }

        if (!isSpawning)
        {
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / MonsterPerSecond) && MonsterLeftToSpawn > 0)
        {
            SpawnMonster();
            MonsterLeftToSpawn--;
            monsterAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (monsterAlive == 0 && MonsterLeftToSpawn == 0)
        {
            EndWave();
            Invoke("NextWave",4f);
        }

        // if monster destroyed
        if (Monster.isMonsterDestroyed)
        {
            price = price + Monster.PRICE;
            textPrice.text = price.ToString();
            Monster.isMonsterDestroyed = false;
        }

        // if monster fly destroyed
        if (MonsterFly.isMonsterFlyDestroyed)
        {
            price = price + MonsterFly.PRICE;
            textPrice.text = price.ToString();
            MonsterFly.isMonsterFlyDestroyed = false;
        }

        // if bug destroyed
        if (MonsterBug.isMonsterBugDestroyed)
        {
            price = price + MonsterBug.PRICE;
            textPrice.text = price.ToString();
            MonsterBug.isMonsterBugDestroyed = false;
        }

        // if bee destroyed
        if (MonsterBee.isMonsterBeeDestroyed)
        {
            price = price + MonsterBee.PRICE;
            textPrice.text = price.ToString();
            MonsterBee.isMonsterBeeDestroyed = false;
        }

        // if bone destroyed
        if (MonsterBone.isMonsterBoneDestroyed)
        {
            price = price + MonsterBone.PRICE;
            textPrice.text = price.ToString();
            MonsterBone.isMonsterBoneDestroyed = false;
        }

    }

    private void NextWave()
    {
        currentWave++;
        textWave.text = "Wave: " + currentWave;
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
        StartCoroutine(StartWave());
    }

    private void SpawnMonster()
    {
        Debug.Log("monster "+monster);
        Monster prefabToWpawn;
        if (monster == MonsterPrefabs.Length)
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
        Instantiate(prefabToWpawn, LevelManager.main.startPoint.GetComponent<Transform>().position, Quaternion.identity).name = "MonsterActive "+ totalMonster;
        totalMonster++;
        
    }

    private int MonsterPerWave()
    {
        int baseMonsterCount = Mathf.RoundToInt(baseMonster * Mathf.Pow(currentWave, difficultyScalingFactor));
        int additionalHP = currentWave * 10; // Tăng maxHP của quái tấn công lên 10 sau mỗi lần quái được sinh ra
        Monster.maxHP += additionalHP;
        Debug.Log("Max HP :" + Monster.maxHP);
        return baseMonsterCount;
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject towerUpdate;

    [Header("Attribute")]

    [SerializeField]
    protected TowerField value;

    //[SerializeField] private float targetingRange = 5f;
    //[SerializeField] private float rotationSpeed = 5f;
    //[SerializeField] private float bps = 1f;
    //[SerializeField] private int baseUpgradeCost = 30;
    [SerializeField] private int level = 1;

    private Transform target;
    private float timeUntilFire;
    //public List<GameObject> targets;
    public Tower main;
    private static GameObject SaveTower;

    
    public List<GameObject> bulletPool= new List<GameObject>();
    int maxPool=7;
    //int bulletIndex = 0;

    private AudioSource audioSellTower;

    private AudioSource audioTowerFight;
    private AudioSource audioUpgardeTower;

    protected virtual void Start()
    {
        audioSellTower = GameObject.Find("TowerSell").GetComponent<AudioSource>();
        audioTowerFight = GameObject.Find("TowerFight").GetComponent<AudioSource>();
        audioUpgardeTower = GameObject.Find("TowerUpgrade").GetComponent<AudioSource>();

        bulletPool = new List<GameObject>();

        for (int i = 0; i < maxPool; i++)
        {
            GameObject bullet = NewABullet();
            //bullet.name= + i.ToString();
            bulletPool.Add(bullet);
            bullet.SetActive(false);
        }
        

    }

    GameObject NewABullet()
    {

        GameObject bullet = Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        bullet.name = gameObject.name + "_bullet_"+ bulletPool.Count;
        return bullet;
    }

    protected virtual void Update()
    {



        if (target == null)
        {
            FindTarget();
            return;
        }


        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {

            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / value.Bps)
            {

                Shoot();
                timeUntilFire = 0f;
            }
        }
        RotateTowardsTarget();
    }

    private void Shoot()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        audioTowerFight.Play();

        GameObject bulletObj =null ;

        foreach (GameObject bullet in bulletPool)
        {
            if ( !bullet.activeSelf)
            {
                bulletObj = bullet;
                break;
            }

        }
        
        if (bulletObj == null)
        {
            bulletObj= NewABullet();
            bulletPool.Add(bulletObj);

            Debug.LogError("create");
        }
        

        //GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, targetRotation);
        bulletObj.transform.position = firingPoint.position;
        bulletObj.transform.rotation = targetRotation;

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();


        bulletObj.SetActive(true);
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, value.TargetingRange, (Vector2)
            transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform; 
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= value.TargetingRange;
    }


    private void RotateTowardsTarget()
    {
        try
        {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, value.RotationSpeed * Time.deltaTime);
        }
        catch (Exception ex)
        {
            
        }
    }

    public void Upgrade()
    {
        if (CaculateCostUpgrade() > MonsterSpawner.price) 
        {
            NotificationManager.GetInstance().AddNotification(new Notification
            {
                Title = "Warning",
                Message = "Not enough price to upgrade"
            });
            return;
        } 
        int newPrice = MonsterSpawner.price - CaculateCostUpgrade();
        MonsterSpawner.price = newPrice;
        MonsterSpawner.isUpgrade = true;
        value.Bps = CaculateBPS();
        value.TargetingRange = CaculateRange();
        Instantiate(towerUpdate, transform.position, Quaternion.identity);
        if (level == 1)
        {
            SaveTower = gameObject;
            SaveTower.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
        CloseUpgradeUI();
        audioUpgardeTower.Play();
    }

    private int CaculateCostUpgrade()
    {
        if (level == 1) { return value.UpdateCost_lv2; }
        else return value.UpdateCost_lv3;
    }

    private int CaculateCostSell()
    {
        if (level == 1) { return (int)(value.Cost * 0.8); }
        else if (level == 2) { return (int)(value.UpdateCost_lv2 * 0.8); }
        else return (int)(value.UpdateCost_lv3* 0.8);
    }

    private float CaculateBPS()
    {
        return value.Bps * Mathf.Pow(level, 0.7f);
    }
    private float CaculateRange()
    {
        return value.TargetingRange * Mathf.Pow(level, 0.5f);
    }
    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }
    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }
    

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI())
        {
            return;
        }
        OpenUpgradeUI();
    }

    public void Sell()
    {

        int newPrice = MonsterSpawner.price + CaculateCostSell();
        //Debug.Log("Tower cost : " + TowerTest.cost);
        Debug.Log("New price : " + newPrice);
        // set price
        MonsterSpawner.price = newPrice;
        Debug.Log("Current price : " + MonsterSpawner.price);
        MonsterSpawner.isTowerSold = true;

        Destroy(gameObject);
        Destroy(SaveTower);
        audioSellTower.Play();
        UIManager.main.SetHoveringState(false);
    }
}

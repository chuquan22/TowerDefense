using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject tower;

    [Header("Attribute")]

    [SerializeField]
    private TowerField value;

    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f;
    [SerializeField] private int baseUpgradeCost = 30;
    [SerializeField] private int level = 1;

    public Animator animator;
    private Transform target;
    private float timeUntilFire;
    public List<GameObject> targets;
    

    
    public List<GameObject> bulletPool= new List<GameObject>();
    int maxPool=3;
    //int bulletIndex = 0;



    private void Start()
    {
        // example get file value
        value = ConfigUtils.GetTowerIceField();
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
        return Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void Update()
    {



        if (target == null)
        {
            FindTarget();
            return;
        }


        if (!CheckTargetIsInRange())
        {
            if (animator != null)
            {
                animator.SetBool("Attack", false);
            }
            target = null;
        }
        else
        {

            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / bps)
            {

                Shoot();
                timeUntilFire = 0f;
            }
        }
        RotateTowardsTarget();
    }

    private void Shoot()
    {
        if (animator != null)
        {
            animator.SetBool("Attack", true);
        }
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));


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
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
            transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }


    private void RotateTowardsTarget()
    {
        try
        {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        catch (Exception ex)
        {

        }
    }

    public void Upgrade()
    {
        if (CaculateCost() > MonsterSpawner.price) return;

        int newPrice = MonsterSpawner.price - CaculateCost();
        MonsterSpawner.price = newPrice;
        MonsterSpawner.isUpgrade = true;
        level++;
        bps = CaculateBPS();
        targetingRange = CaculateRange();
        Instantiate(tower, transform.position, Quaternion.identity);
        Destroy(gameObject);
        CloseUpgradeUI();
    }

    private int CaculateCost()
    {
        return Mathf.RoundToInt( baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CaculateBPS()
    {
        return bps * Mathf.Pow(level, 0.5f);
    }
    private float CaculateRange()
    {
        return targetingRange * Mathf.Pow(level, 0.4f);
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
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
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

        int newPrice = MonsterSpawner.price + 20;
        //Debug.Log("Tower cost : " + TowerTest.cost);
        Debug.Log("New price : " + newPrice);
        // set price
        MonsterSpawner.price = newPrice;
        Debug.Log("Current price : " + MonsterSpawner.price);
        MonsterSpawner.isTowerSold = true;

        Destroy(gameObject);
        Plot.main.gameObject.SetActive(true);
    }
}

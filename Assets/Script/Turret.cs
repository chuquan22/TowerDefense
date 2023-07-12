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
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f;
    [SerializeField] private int baseUpgradeCost = 30;
    [SerializeField] private int level = 1;

    public Animator animator;
    private Transform target;
    private float timeUntilFire;
    public List<GameObject> targets;
    

    TowerIO value;


    private void Start()
    {
        value = new TowerIO(
            fileName: "turret.csv",
            filePath: "Tower/Turret"
            );
       
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
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, targetRotation);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
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
        MonsterSpawner.txtPrice.text = newPrice.ToString();
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
        
        int newPrice = MonsterSpawner.price;
        MonsterSpawner.txtPrice.text = newPrice.ToString();
        Destroy(gameObject);
        Plot.main.gameObject.SetActive(true);
    }
}

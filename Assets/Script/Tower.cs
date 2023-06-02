using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Tower : MonoBehaviour, ITower
{
    [SerializeField]
    public double attackDamage;
    [SerializeField]
    public double attackRange; // Range attacking
    [SerializeField]
    public float attackSpeed; // speed atteacking per time

    [SerializeField]
    public double boughtPrice;
    [SerializeField]
    public double sellPrice;


    protected Timer cooldownTimer;

    public int towerLevel;

    private List<Monster> curEnemiesInRange = new List<Monster>();
    private Monster curEnemy;

    [SerializeField]
    public GameObject prefabBullet;



    virtual protected void Start()
    {
        SetConfigurationDataFields(GetConfigurationFromText());
        cooldownTimer = gameObject.AddComponent<Timer>();
        cooldownTimer.Duration = attackSpeed;


    }
    public void Attack()
    {
        // shoot projectile and restart cooldown as appropriate
        if (cooldownTimer.Finished)
        {
            Instantiate(prefabBullet, transform.position, Quaternion.identity);
            cooldownTimer.Run();
        }
    }

    public virtual void UpgradeTower()
    {

    }

    void Update()
    {
        if (curEnemiesInRange.Count != 0)
        {
            Attack();
        }

        // attack every "attackRate" seconds
        //if (Time.time - lastAttackTime > attackSpeed)
        //{
        //    lastAttackTime = Time.time;
        //    curEnemy = GetEnemy();
        //    if (curEnemy != null)
        //        Attack();
        //}
    }



    // returns the current enemy for the tower to attack
    //Monster GetEnemy()
    //{
    //    curEnemiesInRange.RemoveAll(x => x == null);
    //    if (curEnemiesInRange.Count == 0)
    //        return null;
    //    if (curEnemiesInRange.Count == 1)
    //        return curEnemiesInRange[0];

    //}
    // attacks the curEnemy

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Add(other.GetComponent<Monster>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Remove(other.GetComponent<Monster>());
        }
    }

    public void SellTower()
    {
        // money += sellPrice;
        Destroy(gameObject);

    }

    protected void SetConfigurationDataFields(string csvValues)
    {
        string[] values = csvValues.Split(',');

        try
        {
            attackDamage = double.Parse(values[0]);
            attackRange = double.Parse(values[1]);
            attackSpeed = float.Parse(values[2]);

            boughtPrice = double.Parse(values[3]);
            sellPrice = double.Parse(values[4]);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public virtual string GetConfigurationFromText()
    {
        return null;
    }
}

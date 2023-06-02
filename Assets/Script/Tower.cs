using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour , ITower
{
    public double attackRange; // Range attacking
    public double attackDamage;
    public float attackSpeed; // speed atteacking per time

    public double boughtPrice;
    public double sellPrice;

    
    protected Timer cooldownTimer;

    public int towerLevel;

    private List<Monster> curEnemiesInRange = new List<Monster>();
    private Monster curEnemy;

    [SerializeField]
    public GameObject prefabBullet;

   

    private void Start()
    {
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
}

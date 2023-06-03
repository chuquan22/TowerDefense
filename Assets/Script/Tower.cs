using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour,ITower
{
    [SerializeField]
    private Transform TowerRotationPoint;
    [SerializeField]
    private LayerMask EnemyMark;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float rotationSpeed;
    

    public Animator animator;
    private Transform monster;
    public static float damage;
    public float attackSpeed;
    public float price;

   
    public void Attack()
    {
    }

    public void Update()
    {
        if (monster == null)
        {
            FindMonster();
            return;
        }
        if (!CheckMonsterIsInRange())
        {
            //animator.SetBool("Attack", false);
            monster = null;
        }
        else
        {
           // animator.SetBool("Attack", true);
            Attack();
          
        }
        RotateTowardsMonster();
    }


    private bool CheckMonsterIsInRange()
    {
        return Vector2.Distance(monster.position, transform.position) < attackRange;
    }

    private void RotateTowardsMonster()
    {
        try
        {
            float angle = Mathf.Atan2(monster.position.y - transform.position.y, monster.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion monsterRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            TowerRotationPoint.rotation = Quaternion.RotateTowards(TowerRotationPoint.rotation, monsterRotation, rotationSpeed * Time.deltaTime);
        }
        catch(Exception e)
        {

        }
      
    }

    private void FindMonster()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRange, (Vector2)transform.position, 0f, EnemyMark);

        if (hits.Length > 0)
        {
            monster = hits[0].transform;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.forward, attackRange);
    }

    public void UpdateTower()
    {

    }


}

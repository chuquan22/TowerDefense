using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Monster : MonoBehaviour
{
   
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject[] hearts;
    [Header("Attributes")]
    protected float moveSpeed = 2f;
    private Animator animator;
    protected int maxHP = 100;
    public static float currentHP;

    //public static int price = 0;
    private GameObject target;

    private int pathIndex = 0;
    public static bool isMonsterDestroyed = false;
    public const int BONUS_PRICE_MONSTER = 10;
    public static bool isPassed = false;
    public virtual void Start()
    {
        currentHP = maxHP;
        target = LevelManager.main.path[pathIndex];
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Vector2.Distance(target.GetComponent<Transform>().position, transform.position) <= 0.1f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            pathIndex++; 
            if (pathIndex == LevelManager.main.path.Length)
            {      
                Player.currentHealth--;
                isPassed = true;
                MonsterSpawner.onMonsterDestroy.Invoke();      
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
            if (animator != null)
            {
                if (transform.position.y - target.GetComponent<Transform>().position.y > 2f)
                {
                    animator.SetInteger("direction", 1);
                }
                else if (Mathf.Abs(transform.position.y - target.GetComponent<Transform>().position.y) > 2f)
                {
                    animator.SetInteger("direction", 2);
                }
                else if (transform.position.x > target.GetComponent<Transform>().position.x)
                {
                    animator.SetInteger("direction", 0);
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                }
                else
                {
                    animator.SetInteger("direction", 0);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.GetComponent<Transform>().position - transform.position).normalized;
        
        rb.velocity = direction * moveSpeed;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log(currentHP);
        if(currentHP <= 0)
        {
            MonsterSpawner.onMonsterDestroy.Invoke();
            Destroy(gameObject);
            isMonsterDestroyed = true;
        }
    }

}

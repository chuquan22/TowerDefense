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
    [SerializeField]
    [Header("Attributes")]
    protected float moveSpeed = 2f;
    private Animator animator;
    protected int maxHP = 100;
    public static float currentHP;
    //public static int price = 0;
    private Transform target;
    private int pathIndex = 0;
    public static bool isMonsterDestroyed = false;
    public const int BONUS_PRICE_MONSTER = 100;
    public virtual void Start()
    {
        currentHP = maxHP;
        target = LevelManager.main.path[pathIndex];
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++; 
            if (pathIndex == LevelManager.main.path.Length)
            {             
                MonsterSpawner.onMonsterDestroy.Invoke();      
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

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

    private void LoadScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}

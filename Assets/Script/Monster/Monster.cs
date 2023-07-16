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
    public float moveSpeed = 2f;
    private Animator animator;
    public static int maxHP = 30;
    public const int PRICE = 10;
    public static float currentHP;

    //public static int price = 0;
    private GameObject target;

    private int pathIndex = 0;
    public static bool isMonsterDestroyed = false;
    public static Monster main;
    private float currentSpeed = 0;
    private int time;
    Slider slider;

    public static bool isPassed = false;
    public void SetSpeed()
    {
        currentSpeed = (float)(moveSpeed * 0.2);
        Debug.Log(gameObject.name);
        time = 0;
    }
    public void Awake()
    {
        main = this;
    }
    public virtual void Start()
    {
        currentHP = maxHP;
        target = LevelManager.main.path[pathIndex];
        animator = GetComponent<Animator>();
        slider= GetComponentInChildren<Slider>();
        slider.maxValue = maxHP;
        slider.value = slider.maxValue;
        slider.minValue = 0;
    }
    private void Update()
    {
        
        if(Vector2.Distance(target.GetComponent<Transform>().position, transform.position) <= 0.1f)
        {
            
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
                if(transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
        if(currentSpeed > 0 && time <=10)
        {
            rb.velocity = direction * currentSpeed;
            time++;
            Debug.Log(gameObject.name +":"+ currentSpeed);
        }
        else
        {
            rb.velocity = direction * moveSpeed;
            time = 0;
            currentSpeed= 0;
        }
        
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        slider.value -= damage;
        Debug.Log(gameObject.name + ": "+  slider.value);
        if(currentHP <= 0)
        {
            MonsterSpawner.onMonsterDestroy.Invoke();
            Destroy(gameObject);
            isMonsterDestroyed = true;
        }
    }

    
}

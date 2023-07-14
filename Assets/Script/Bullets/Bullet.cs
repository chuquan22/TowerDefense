using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] protected float bulletSpeed = 5f;

    [SerializeField] protected float damage = 20f;

    protected Transform target;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    public virtual void Start()
    {

    }
    private void FixedUpdate()
    {
        if (!target)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
        Monster monster = other.gameObject.GetComponent<Monster>();
        MonsterFly monsterFly = other.gameObject.GetComponent<MonsterFly>();
        if (monster != null)
        {
            monster.TakeDamage(damage);
        }else if(monsterFly != null)
        {
            monsterFly.TakeDamage(damage);
        }
    }
}

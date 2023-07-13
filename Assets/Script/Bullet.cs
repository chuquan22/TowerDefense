using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;

    [SerializeField] private int damage = 20;

    private Transform target;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        Monster monster = other.gameObject.GetComponent<Monster>();
        Butterfly monsterFly = other.gameObject.GetComponent<Butterfly>();
        if (monster != null)
        {
            monster.TakeDamage(damage);
        }else if(monsterFly != null)
        {
            monsterFly.TakeDamage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    private Animator animator;
    

    private Transform target;
    private int pathIndex = 0;
    private void Start()
    {
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



        //if(transform.position.y < target.position.y && transform.position.x == target.position.x) 
        //{
        //    animator.SetInteger("statechange", 1);
        //}else if (transform.position.y > target.position.y && transform.position.x == target.position.x)
        //{
        //    animator.SetInteger("statechange", -1);
        //}else if (transform.position.x > target.position.x && transform.position.y > target.position.y)
        //{
        //    animator.SetInteger("statechange", 0);
        //}else
        //{
        //    transform.localScale.Set(-1,1,1);
        //    animator.SetInteger("statechange", 0);
        //}
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

}

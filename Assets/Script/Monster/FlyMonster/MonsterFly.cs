using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFly : Monster
{
    public static bool isMonsterFlyDestroyed = false;
    public const int PRICE = 15;
    public override void Start()
    {
        moveSpeed = 4f;
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log(currentHP);
        if (currentHP <= 0)
        {
            MonsterSpawner.onMonsterDestroy.Invoke();
            Destroy(gameObject);
            isMonsterFlyDestroyed = true;
        }
    }


}

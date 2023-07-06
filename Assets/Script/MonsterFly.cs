using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFly : Monster
{
    public static bool isMonsterFlyDestroyed = false;
    public const int BONUS_PRICE_MONSTER_FLY = 150;
    public override void Start()
    {
        moveSpeed = 4f;
        base.Start();
    }

    public override void TakeDamage(float damage)
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

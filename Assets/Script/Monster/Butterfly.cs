using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : Monster
{
    public static bool isMonsterFlyDestroyed = false;
    public const int BONUS_PRICE_MONSTER_FLY = 25;


    public override void Awake()
    {
        value = ConfigUtils.GetButterflyMonsterField();
        base.Awake();
    }

    //public override void TakeDamage(int damage)
    //{
    //    currentHP -= damage;
    //    Debug.Log(currentHP);
    //    if (currentHP <= 0)
    //    {
    //        MonsterSpawner.onMonsterDestroy.Invoke();
    //        Destroy(gameObject);
    //        isMonsterFlyDestroyed = true;
    //    }
    //}


}

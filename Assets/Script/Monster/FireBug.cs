using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class FireBug : Monster
    {
        public static bool isMonsterFlyDestroyed = false;
        public override void Awake()
        {
            value = ConfigUtils.GetFireBugMonsterField();
            base.Awake();
        }
        //public override void TakeDamage(int damage)
        //{
        //    maxHP -= damage;
        //    Debug.Log(maxHP);
        //    if (maxHP <= 0)
        //    {
        //        MonsterSpawner.onMonsterDestroy.Invoke();
        //        Destroy(gameObject);
        //        isMonsterFlyDestroyed = true;
        //    }
        //}
    }
}

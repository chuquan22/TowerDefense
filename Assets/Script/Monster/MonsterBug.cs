using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class MonsterBug : Monster
    {
        public static bool isMonsterBugDestroyed = false;

        public override void Awake()
        {
            value = ConfigUtils.GetBugMonsterField();

            base.Awake();
        }

        //public override void TakeDamage(int damage)
        //{
        //    currentHP -= damage;
        //audioMonsterHurt.Play();
        //    Debug.Log(currentHP);
        //    if (currentHP <= 0)
        //    {
        //        MonsterSpawner.onMonsterDestroy.Invoke();
        //        Destroy(gameObject);
        //        isMonsterBugDestroyed = true;
        //    }
        //}
    }
}

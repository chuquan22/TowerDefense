using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class Scorpion : Monster
    {
        public static bool isMonsterFlyDestroyed = false;
       



        public override void Awake()
        {
            value = ConfigUtils.GetScorpionMonsterField();

            base.Awake();
        }
        //public override void TakeDamage(int damage)
        //{
        //    maxHP -= damage;
        //audioMonsterHurt.Play();
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

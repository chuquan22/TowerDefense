using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class MonsterBee : Monster
    {
        public static bool isMonsterBeeDestroyed = false;
        public override void Awake()
        {
            value = ConfigUtils.GetBeeMonsterField();
            base.Awake();
        }

        //public override void TakeDamage(int damage)
        //{
        //    audioMonsterHurt.Play();

        //    currentHP -= damage;
        //    Debug.Log(currentHP);
        //    if (currentHP <= 0)
        //    {
        //        MonsterSpawner.onMonsterDestroy.Invoke();
        //        Destroy(gameObject);
        //        isMonsterBeeDestroyed = true;
        //    }
        //}
    }
}

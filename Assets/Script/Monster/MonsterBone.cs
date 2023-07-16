using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class MonsterBone : Monster
    {
        public static bool isMonsterBoneDestroyed = false;

        public override void Awake()
        {
            value = ConfigUtils.GetBoneMonsterField();
            base.Awake();
        }
        //public override void TakeDamage(int damage)
        //{
        //audioMonsterHurt.Play();
        //    maxHP -= damage;
        //    Debug.Log(maxHP);
        //    if (maxHP <= 0)
        //    {
        //        MonsterSpawner.onMonsterDestroy.Invoke();
        //        Destroy(gameObject);
        //        isMonsterBoneDestroyed = true;
        //    }
        //}
    }
}

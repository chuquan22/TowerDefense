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
        public const int PRICE = 20;
        public override void Start()
        {
            moveSpeed = 3f;
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
                isMonsterBugDestroyed = true;
            }
        }
    }
}

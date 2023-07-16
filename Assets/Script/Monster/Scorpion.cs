﻿using System;
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
        public const int BONUS_PRICE_MONSTER_FLY = 30;
        public override void Start()
        {
            maxHP = MonsterSpawner.currentWave * 10 + 100;
            Debug.Log("CurrentWave: " + MonsterSpawner.currentWave);
            Debug.Log("maxHP: " + maxHP);
            base.Start();
        }

        public override void TakeDamage(int damage)
        {
            maxHP -= damage;
            Debug.Log(maxHP);
            if (maxHP <= 0)
            {
                MonsterSpawner.onMonsterDestroy.Invoke();
                Destroy(gameObject);
                isMonsterFlyDestroyed = true;
            }
        }
    }
}
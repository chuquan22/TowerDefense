﻿using System;
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
        public const int PRICE = 15;
        public override void Start()
        {
            moveSpeed = 2.5f;
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
                isMonsterBeeDestroyed = true;
            }
        }
    }
}
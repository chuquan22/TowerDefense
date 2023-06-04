using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFly : Monster
{

    public override void Start()
    {
        moveSpeed = 4f;
        base.Start();
    }
}

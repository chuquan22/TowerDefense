﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Towers
{
    public class LightTower : Tower
    {
        protected override void Start()
        {
            value = ConfigUtils.GetTowerLightField();
            base.Start();
        }
    }
}

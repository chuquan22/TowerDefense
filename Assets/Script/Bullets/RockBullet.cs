using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Towers
{
    public class RockBullet : Bullet
    {
        public override void Start()
        {
            bulletSpeed = 4;
            damage = 15;
            base.Start();
        }
    }
}

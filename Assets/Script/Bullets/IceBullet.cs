using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Towers
{
    public class IceBullet : Bullet
    {
        public override void Start()
        {
            bulletSpeed = 5;
            damage = 12;
            base.Start();
        }
    }
}

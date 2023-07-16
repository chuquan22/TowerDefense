using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Bullets
{
    public class LightBullet : Bullet
    {
        public override void Start()
        {
            bulletSpeed= 6;
            damage= 10;
            base.Start();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Towers
{
    public class ShockBullet : Bullet
    {
        public override void Start()
        {
            bulletSpeed = 0;
            damage = 5;
            base.Start();
        }

        private void Update()
        {
            if (!target)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
        {
           Monster[] m = collision.gameObject.GetComponents<Monster>();
           foreach(Monster monster in m)
            {
                monster.SetSpeed();
            }
        }

        public void HideBullet()
        {
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDV_Semana_2_Tarea1
{
    internal class EnemyRange
    {
        public int Life;
        public int Damage;
        public int Bullet;

        public EnemyRange(int life, int damage, int bullet)
        {
            this.Life = life;
            this.Damage = damage;
            this.Bullet = bullet;
        }

        public void ReceiveDamage(int damage)
        {
            Life -= damage;
            if (Life < 0) Life = 0;
        }

        public int Attack()
        {
            if (Bullet > 0)
            {
                Bullet--;
                return Damage;
            }
            return 0;
        }

        public bool CanAttack()
        {
            return Bullet > 0;
        }

        public bool IsAlive()
        { 
            return Life > 0;
        }
    }
}

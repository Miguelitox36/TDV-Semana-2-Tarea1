using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDV_Semana_2_Tarea1
{
    internal class EnemyMelee
    {
        public int Life;
        public int Damage;

        public EnemyMelee(int life, int damage) 
        { 
            this.Life = life;
            this.Damage = damage;
        }

        public void ReceiveDamage(int damage)
        {
            Life -= damage;
            if (Life < 0) Life = 0;
        }

        public int Attack()
        {
            return Damage;
        }

        public bool IsAlive()
        {
            return Life > 0;
        }
    }
}

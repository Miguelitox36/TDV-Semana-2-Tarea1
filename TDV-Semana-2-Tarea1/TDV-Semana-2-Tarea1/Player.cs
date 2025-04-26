using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDV_Semana_2_Tarea1
{
    internal class Player
    {
        public int Life;
        public int Damage;
        private int maxLife;
        private Random random = new Random();

        public Player(int life, int damage)
        {
            this.Life = life;
            this.Damage = damage;
            this.maxLife = life;
        }

        public void ReceiveDamage(int damage)
        {
            if (TryDodge())
            {
                Console.WriteLine("¡El jugador esquivó el ataque!");
                return;
            }

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

        public void Heal()
        {
            int healAmount = 20;
            Life += healAmount;
            if (Life > maxLife) Life = maxLife;
            Console.WriteLine($"El jugador se curó {healAmount} puntos. Vida actual: {Life}");
        }

        private bool TryDodge()
        {
            return random.Next(100) < 30; 
        }
    }
}

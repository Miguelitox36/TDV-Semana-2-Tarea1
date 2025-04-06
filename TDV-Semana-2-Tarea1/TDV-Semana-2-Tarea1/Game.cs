using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDV_Semana_2_Tarea1
{
    internal class Game
    {
        private Player player;
        private List<object> enemies;
        private Random random = new Random();

        public void Start()
        {
            Console.WriteLine("=== CREAR JUGADOR ===");
            player = NewPlayer();

            enemies = new List<object>
        {
            new EnemyMelee(30, 10),
            new EnemyRange(25, 8, 3),
            new EnemyMelee(40, 12)
        };

            Console.WriteLine("\n--- COMIENZA EL JUEGO ---");

            while (player.IsAlive() && enemies.Exists(e => IsAlive(e)))
            {
                ShowStatus();

                Console.Write("\nSeleccione enemigo a atacar (0 - {0}): ", enemies.Count - 1);
                if (!int.TryParse(Console.ReadLine(), out int indice) || indice < 0 || indice >= enemies.Count)
                {
                    Console.WriteLine("Índice inválido.");
                    continue;
                }

                var enemy = enemies[indice];
                if (!IsAlive(enemy))
                {
                    Console.WriteLine("Ese enemigo ya está muerto.");
                    continue;
                }

                int damagePlayer = player.Attack();
                TakeEnemyDamage(enemy, damagePlayer);
                Console.WriteLine($"El jugador ataca al enemigo {indice} y causa {damagePlayer} de daño.");


                enemy = GetRandomLiveEnemy();
                if (enemy != null)
                {
                    int DamagePlayer = AttackEnemy(enemy);
                    if (damagePlayer > 0)
                    {
                        player.ReceiveDamage(damagePlayer);
                        Console.WriteLine($"El enemigo ataca al jugador y causa {damagePlayer} de daño.");
                    }
                    else
                    {
                        Console.WriteLine("El enemigo no pudo atacar (sin balas).");
                    }
                }
            }

            if (player.IsAlive())
                Console.WriteLine("\n ¡Victoria! Has vencido a todos los enemigos.");
            else
                Console.WriteLine("\n Has sido derrotado.");
        }

        private Player NewPlayer()
        {
            int life, damage;

            do
            {
                Console.Write("Ingrese vida del jugador (1-100): ");
                life = int.Parse(Console.ReadLine());
                Console.Write("Ingrese daño del jugador (1-100): ");
                damage = int.Parse(Console.ReadLine());
            }

            while (life > 100 || damage > 100);

            return new Player(life, damage);
        }

        private bool IsAlive(object enemy)
        {
            if (enemy is EnemyMelee em)
                return em.IsAlive();
            if (enemy is EnemyRange er)
                return er.IsAlive();
            return false;
        }

        private void TakeEnemyDamage(object enemy, int damage)
        {
            if (enemy is EnemyMelee em)
                em.ReceiveDamage(damage);
            else if (enemy is EnemyRange er)
                er.ReceiveDamage(damage);
        }

        private int AttackEnemy(object enemy)
        {
            if (enemy is EnemyMelee em)
                return em.Attack();
            else if (enemy is EnemyRange er)
                return er.CanAttack() ? er.Attack() : 0;
            return 0;
        }

        private object GetRandomLiveEnemy()
        {
            var alife = enemies.FindAll(IsAlive);
            if (alife.Count == 0) return null;
            return alife[random.Next(alife.Count)];
        }

        private void ShowStatus()
        {
            Console.WriteLine("\nJugador - Vida: " + player.Life);

            for (int i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];
                string type = enemy is EnemyRange ? "Rango" : "Melee";
                string status = IsAlive(enemy) ? "VIVO" : "MUERTO";

                if (enemy is EnemyMelee em)
                    Console.WriteLine($"Enemigo {i} [{type}] - Vida: {em.Life} - Estado: {status}");
                else if (enemy is EnemyRange er)
                    Console.WriteLine($"Enemigo {i} [{type}] - Vida: {er.Life} - Balas: {er.Bullet} - Estado: {status}");
            }
        }

    }
}

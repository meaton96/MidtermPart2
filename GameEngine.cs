using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class GameEngine {
        List<Room>? rooms;
        Player? player;
        List<Enemy>? enemies;
        Thread? enemyThread;
        Thread? inputThread;
        bool inBattle;
        public GameEngine() { Start(); }

        public void Start() {
            inBattle = false;
            Console.CursorVisible = false;
            enemies = new();
            Console.OutputEncoding = Encoding.Unicode;
            player = new Player();
            Orc orc = new Orc();
            enemyThread = new Thread(UpdateEnemies);
            orc.X = 3;
            orc.Y = 3;
            enemies.Add(orc);
            rooms = new() {
                new Room(30, 15, new bool[] { true, true, true, true })
            };
            player.X = 5;
            player.Y = 5;
            inputThread = new Thread(HandleInput);
            inputThread.Start();

            Update();
        }

        public void Update() {

            LoadNewRoom();

            /* while (player!.IsAlive) {

              }*/

        }
        public void LoadNewRoom() {
            GetCurrentRoom().Draw();
            //init enemies for room
            //init objectives for room
            //reset player pos at new door
            enemyThread!.Start();
        }
        public void UpdateEnemies() {
            enemies!.First().Draw();
            while (player!.IsAlive) {
                if (!inBattle) {
                    //enemies!.ForEach(e => e.Update(GetCurrentRoom()));
                    Thread.Sleep(500);
                }
            }
        }
        public void HandleInput() {
            ConsoleKeyInfo keyPressed;
            char destChar = ' ';
            while (player!.IsAlive) {
                keyPressed = Console.ReadKey(true);
                if (!inBattle) {
                    switch (keyPressed.Key) {
                        case ConsoleKey.A:
                            destChar = player.Move(Player.LEFT, GetCurrentRoom());
                            break;
                        case ConsoleKey.W:
                            destChar = player.Move(Player.UP, GetCurrentRoom());
                            break;
                        case ConsoleKey.D:
                            destChar = player.Move(Player.RIGHT, GetCurrentRoom());
                            break;
                        case ConsoleKey.S:
                            destChar = player.Move(Player.DOWN, GetCurrentRoom());
                            break;
                        default: break;
                    }
                    enemies!.ForEach(e => {
                        if (e.X == player.X && e.Y == player.Y) {
                            destChar = e.DrawChar;
                            return;
                        }
                    });
                    Debug.WriteLine("Dest: " + destChar);
                    if (destChar != Room.EMPTY_CHAR) {
                        HandleCollision(destChar);
                    }
                }
            }
        }
        public void HandleCollision(char collisionChar) {
            if (Enemy.ENEMY_CHARS.Contains(collisionChar)) {
                RunBattle();
            }
            //else see if the character finds chest or something
            //else charater found a door
        }
        public void RunBattle() {
            inBattle = true;
            Console.SetCursorPosition(0, GetCurrentRoom().Height + 2);
            Enemy enemy = GetEnemyAtPlayer();
            int currentEnemyHealth = enemy.Health;
            Console.WriteLine(player!.Name + "H: " + player.Health + " vs " + enemy.Name + "H: " +
                enemy.Health + " A: " + enemy.Attack);
            Console.WriteLine(player.GetAbilityList());
            while (inBattle) {
                Ability usedAbility;
                int index = 0;
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.D1:
                        index = 0;
                        break;
                    case ConsoleKey.D2:
                        index = 1;
                        break;
                    case ConsoleKey.D3:
                        index = 2;
                        break;
                    case ConsoleKey.D4:
                        index = 3;
                        break;
                    default:
                        break;
                }
                usedAbility = player.GetAbilityByIndex(index);
                int playerBlockAmount = 0;
                if (usedAbility is OffensiveAbility) {
                    int damageDoneByPlayer = ((OffensiveAbility)usedAbility).Damage;
                    Console.WriteLine(usedAbility.Name + " against " + enemy.Name + " for "
                        + damageDoneByPlayer);
                    currentEnemyHealth -= damageDoneByPlayer;
                    if (currentEnemyHealth > 0) {

                        Console.WriteLine("Orc has " + currentEnemyHealth + " left");
                    }
                    else {
                        inBattle = false;
                        enemies!.Remove(enemy);
                    }
                }
                else {
                    String abilityName = usedAbility.Name;
                    int healAmount = ((DefensiveAbility)usedAbility).DefensivePower;
                    if (abilityName == "Heal") {
                        Console.WriteLine(player.Name + " heals for " + healAmount);
                        player.Health += healAmount;
                    }
                    else if (abilityName == "Block") {
                        playerBlockAmount = healAmount;
                        Console.WriteLine(player.Name + " blocks");
                    }
                }
                int enemyDamageDone = enemy.PerformAttack();
                Console.WriteLine(enemy.Name + " attacks the player for " +
                    (playerBlockAmount == 0 ? enemyDamageDone : enemyDamageDone - playerBlockAmount) + " damage ");
                player.Health -= enemyDamageDone;
                if (!player.IsAlive) {
                    inBattle = false;
                }
            }
            GetCurrentRoom().Draw();
        }

        public Enemy GetEnemyAtPlayer() {
            foreach (Enemy e in enemies!) {
                if (e.X == player!.X && e.Y == player!.Y)
                    return e;
            }
            return null;
        }

        public Room GetCurrentRoom() {
            return rooms!.First();
        }

        /* public void Draw() {
             player!.HasMoved = false;
             //Console.Clear();
             GetCurrentRoom().Draw();
             player.Draw();
            /* char[][] toDraw = CombineChars(GetCurrentRoom().GetRoom());
             for (var x = 0; x < toDraw.Length; x++) {
                 for (var y = 0; y < toDraw[0].Length; y++) {
                     Console.Write(toDraw[x][y]);
                 }
                 Console.Write("\n");
             }
             return toDraw;

         }
       /*  private char[][] CombineChars(char[][] chars) {
             char[][] result = chars;
             for (var x = 0; x < result.Length; x++) {
                 for (var y = 0; y < result[0].Length; y++) {
                     foreach (Entity e in entities!) {
                         if (x == e.X && y == e.Y)
                             result[x][y] = e.DrawChar;
                     }
                     if (x == player!.X && y == player!.Y) {
                         result[x][y] = player.DrawChar;
                     }
                 }
             }
             return result;
         }*/
    }
}

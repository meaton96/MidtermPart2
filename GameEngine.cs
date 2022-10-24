using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class GameEngine {
        List<Room>? rooms;              //list of all rooms probably change this to an array for room layout per floor or w/e
        Player? player;                 //player reference
        List<Enemy>? enemies;           //list of enemies used per room
        Thread? enemyThread;            //thread to handle enemy movement 
        Thread? inputThread;            //thread to handle player input
        bool inBattle;                  //flag for in battle or not
        int numEnemiesKilled = 0;       //counter for "score" for this simple demo
        ManualResetEvent pauseEnemyThread = new ManualResetEvent(true);     //flag to pause the enemy movement thread
        public GameEngine() { Start(); }    //man why didnt i just put the start code in the constructor to avoid null reference stuff

        public void Start() {
            //start by setting battle to false and messing with console settings
            //init enemy list and player
            inBattle = false;
            Console.CursorVisible = false;
            enemies = new();
            Console.OutputEncoding = Encoding.Unicode;
            player = new Player();

            enemyThread = new Thread(UpdateEnemies);    //create thread to move enemies

            //add a single room cause thats all I'm doing definitely not going to work on this another 50 hours...
            rooms = new() {
                new Room(30, 15, new bool[] { true, true, true, true })
            };
            player.X = 1;
            player.Y = 1;
            //create and start player input thread
            inputThread = new Thread(HandleInput);
            inputThread.Start();

            //create a few enemies
            Orc orc = new Orc(3, 3);
            Orc orc1 = new Orc(8, 8);
            Orc orc2 = new Orc(13, 3);
            Orc orc3 = new Orc(25, 10);
            List<Enemy> roomOneEnemies = new List<Enemy>() {
                orc, orc1, orc2, orc3
            };

            //start the game by loading a room
            LoadNewRoom(roomOneEnemies);
        }

        public void Update() {
            //i swear this was going to be for something
            

            /* while (player!.IsAlive) {

              }*/

        }
        //load a new room or you know the only room with a list of enmies to add to it
        public void LoadNewRoom(List<Enemy> newRoomEnemies) {
            GetCurrentRoom().Draw(player);
            enemies.AddRange(newRoomEnemies);
            //init enemies for room
            //init objectives for room
            //reset player pos at new door
            enemyThread!.Start();   //start the enemy movement thread
        }
        //moves the enemies randomly around the room
        public void UpdateEnemies() {
            while (player!.IsAlive) {
                pauseEnemyThread.WaitOne(Timeout.Infinite); //flag to pause enemy thread in battle
                foreach (Enemy e in enemies!.ToList()) {
                    e.Update(GetCurrentRoom()); //update each enemy in the room
                }
                Thread.Sleep(500);  //wait .5seconds

            }
        }
        public void PauseEnemyThread() {
            pauseEnemyThread.Reset();
        }
        public void ResumeEnemyThread() {
            pauseEnemyThread.Set();
        }
        //handle the in room movement for the player
        public void HandleInput() {
            ConsoleKeyInfo keyPressed;
            char destChar = ' ';
            while (player!.IsAlive) {
                keyPressed = Console.ReadKey(true);
                if (!inBattle) {
                    //grab the character in the destination space the player is trying to move to
                    //move the player there
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
                    //i cant remember what this is for
                    //ok i remember
                    //its for starting a battle when the player runs into an enemy
                    enemies!.ForEach(e => {
                        if (e.X == player.X && e.Y == player.Y) {
                            destChar = e.DrawChar;
                            return;
                        }
                    });
                    //if the player is moving into a sqaure that is not empty then call the collision handler
                    if (destChar != Room.EMPTY_CHAR) {
                        HandleCollision(destChar);
                    }
                }
            }
        }
        //handle when the player runs into somethign that isnt an empty square
        public void HandleCollision(char collisionChar) {
            if (Enemy.ENEMY_CHARS.Contains(collisionChar)) {
                RunBattle();
            }
            else if (collisionChar == Room.DOOR_CHAR) {
                PrintEndGameMessage();  //player got to the end of the room they win the demo woo
            }
            //else see if the character finds chest or something
        }
        //i think this method prints the end game message cant be sure
        public void PrintEndGameMessage() {
            player!.Health = 0;
            Console.Clear();
            Console.WriteLine("Congrats you made it out of the room and beat the game wooo");
            Console.WriteLine("You defeated {0} enemies along the way", numEnemiesKilled);
            Console.WriteLine("Bye");
        }

        //run a battle between player and an enemy
        //this should probably be a seperate class
        public void RunBattle() {
            PauseEnemyThread(); //..pause the enemy thread..
            inBattle = true;
            Console.SetCursorPosition(0, GetCurrentRoom().Height + 2);  //move the cursor to under the current room for printing
            Enemy enemy = GetEnemyAtPlayer();
            int currentEnemyHealth = enemy.Health;
            //print enemy and player health and attack
            Console.WriteLine(player!.Name + " H: " + player.Health + " vs " + enemy.Name + " H: " +
                enemy.Health + " A: " + enemy.Attack);
            Console.WriteLine(player.GetAbilityList());
            //handle player input while in the battle
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
                        continue;
                }
                //get the player ability they chose
                usedAbility = player.GetAbilityByIndex(index);
                int playerBlockAmount = 0;
                //do some stuff with it 
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
                        numEnemiesKilled++;
                        enemies!.Remove(enemy);
                        break;
                    }
                }
                //do some different stuff if the player cast a defensive ability
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
                //process the enemies turn
                int enemyDamageDone = enemy.PerformAttack();
                Console.WriteLine(enemy.Name + " attacks the player for " +
                    (playerBlockAmount == 0 ? enemyDamageDone : enemyDamageDone - playerBlockAmount) + " damage ");
                player.Health -= enemyDamageDone;
                if (!player.IsAlive) {
                    inBattle = false;
                }
            }
            //battle ended, redraw the room and resume enemy movement thread
            GetCurrentRoom().Draw(player);
            ResumeEnemyThread();
        }

        //get the enemy that the player ran into
        public Enemy GetEnemyAtPlayer() {
            foreach (Enemy e in enemies!) {
                if (e.X == player!.X && e.Y == player!.Y)
                    return e;
            }
            return null;
        }

        //return the current room i guess
        public Room GetCurrentRoom() {
            return rooms!.First();
        }

    }
}

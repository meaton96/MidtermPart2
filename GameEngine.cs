using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class GameEngine {
        List<Room>? rooms;
        Player? player;
        List<Enemy>? enemies;
        public GameEngine() { Start(); }

        public void Start() {
            Console.CursorVisible = false;
            enemies = new();
            Console.OutputEncoding = Encoding.Unicode;
            player = new Player();
            Orc orc = new Orc();
            orc.X = 3;
            orc.Y = 3;
            enemies.Add(orc);
            rooms = new() {
                new Room(15, 30, new bool[] { true, true, true, true })
            };
            player.X = 5;
            player.Y = 5;
            Thread inputThread = new Thread(HandleInput);
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
            Thread enemyThread = new Thread(UpdateEnemies);
            enemyThread.Start();
        }
        public void UpdateEnemies() {
            while (player!.IsAlive) {
                enemies!.ForEach(e => e.Update(GetCurrentRoom()));
                Thread.Sleep(500);
            }
        }
        public void HandleInput() {
            ConsoleKeyInfo keyPressed;
            while (player!.IsAlive) {
                keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key) {
                    case ConsoleKey.A:
                        player.Move(0, GetCurrentRoom());
                        break;
                    case ConsoleKey.W:
                        player.Move(1, GetCurrentRoom());
                        break;
                    case ConsoleKey.D:
                        player.Move(2, GetCurrentRoom());
                        break;
                    case ConsoleKey.S:
                        player.Move(3, GetCurrentRoom());
                        break;
                    default: break;
                }
            }
            
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

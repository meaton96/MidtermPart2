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
        List<Entity>? entities;
        public GameEngine() { Start(); }

        public void Start() {
            entities = new();
            Console.OutputEncoding = Encoding.Unicode;
            player = new Player();
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

            char[][] previousDraw = CombineChars(GetCurrentRoom().GetRoom());
            Draw();
            while (player!.IsAlive) {
                Console.WriteLine(previousDraw.Equals(CombineChars(GetCurrentRoom().GetRoom())));
                if (!previousDraw.Equals(CombineChars(GetCurrentRoom().GetRoom()))) {
                    Draw();
                    
                    previousDraw = CombineChars(GetCurrentRoom().GetRoom());
                }
                //Thread.Sleep(42);
            }

        }
        public void HandleInput() {

            while (player!.IsAlive) {
                ConsoleKeyInfo keyPressed = Console.ReadKey(false);

                switch (keyPressed.Key) {
                    case ConsoleKey.A:
                        if (IsValidMove(0))
                            player!.X--;
                        break;
                    case ConsoleKey.W:
                        if (IsValidMove(1))
                            player!.Y--;
                        break;
                    case ConsoleKey.D:
                        if (IsValidMove(2))
                            player!.X++;
                        break;
                    case ConsoleKey.S:
                        if (IsValidMove(3))
                            player!.Y++;
                        break;
                    default: break;
                }
            }
            
        }
        
        public bool IsValidMove(int direction) {
            switch (direction) {
                case 0: return GetCurrentRoom().GetRoom()[player!.X - 1][player.Y] != Room.WALL_CHAR;
                case 1: return GetCurrentRoom().GetRoom()[player!.X][player.Y - 1] != Room.WALL_CHAR;
                case 2: return GetCurrentRoom().GetRoom()[player!.X + 1][player.Y] != Room.WALL_CHAR;
                case 3: return GetCurrentRoom().GetRoom()[player!.X][player.Y + 1] != Room.WALL_CHAR;
                default: throw new IndexOutOfRangeException();

            }
        }
        public Room GetCurrentRoom() {
            return rooms!.First();
        }

        public void Draw() {
            Console.Clear();
            char[][] toDraw = CombineChars(GetCurrentRoom().GetRoom());
            for (var x = 0; x < toDraw.Length; x++) {
                for (var y = 0; y < toDraw[0].Length; y++) {
                    Console.Write(toDraw[x][y]);
                }
                Console.Write("\n");
            }

        }
        private char[][] CombineChars(char[][] chars) {
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
        }
    }
}

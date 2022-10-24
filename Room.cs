using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Room {

        public const char WALL_CHAR = '\u25a0';
        public const char DOOR_CHAR = '\u25a1';
        public const char EMPTY_CHAR = ' ';

        private readonly int width, height;
        private readonly bool[] doorLocations;
        private readonly char[][] room;


        //this is a constructor!
        public Room(int width, int height, bool[] doorLocations) {
            this.width = width;
            this.height = height;
            this.doorLocations = doorLocations;
            room = new char[width][];
            for (var x = 0; x < room.Length; x++) {
                room[x] = new char[height];
            }
            BuildRoom();
        }

        //builds a rectangular room by making the walls and doors in the middle of each wall
        private void BuildRoom() {
            int middleX = width / 2;
            int middleY = height / 2;
            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    if (y == 0 || y == height - 1) {
                        if (doorLocations[0] && x == middleX) {
                            room[x][y] = DOOR_CHAR;
                        }
                        if (doorLocations[2] && x == middleX) {
                            room[x][y] = DOOR_CHAR;
                        }
                        else
                            room[x][y] = WALL_CHAR;
                    }
                    else if (x == 0 || x == width - 1) {
                        if (doorLocations[1] && y == middleY) {
                            room[x][y] = DOOR_CHAR;
                        }
                        if (doorLocations[3] && y == middleY) {
                            room[x][y] = DOOR_CHAR;
                        }
                        else
                            room[x][y] = WALL_CHAR;
                    }
                    else
                        room[x][y] = EMPTY_CHAR;

                }

            }
        }
        //draw the current room, takes the player as a way to draw the player before they start moving
        public void Draw(Player player) {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (var x = 0; x < room[0].Length; x++) {
                for (var y = 0; y < room.Length; y++) {
                    if (x == player.X && y == player.Y)
                        Console.Write(player.DrawChar);
                    else
                        Console.Write(room[y][x]);
                }
                Console.Write("\n");
            }
            //print out instruction message
            //why is set cursor position arguments reversed??? who knows....
            Console.SetCursorPosition(Width + 5 , 1);
            Console.Write("W A S D to move, run into an enemy to start fight reach any door to win");
            Console.SetCursorPosition(Width + 5, 2);
            Console.Write(Orc.DRAW_CHAR + " = Orc");
            Console.SetCursorPosition(Width + 5, 3);
            Console.Write(Player.DRAW_CHAR + " = you!");
            Console.SetCursorPosition(Width + 5, 4);
            Console.Write(Room.WALL_CHAR + " = wall");
            Console.SetCursorPosition(Width + 5, 5);
            Console.Write(Room.DOOR_CHAR + " = room exit");
        }

        public char GetCharAt(int x, int y) {
            return room[x][y];
        }
        public char[][] GetRoomArray() {
            return room;
        }
        public int Width { get { return width; } }
        public int Height { get { return height; } }

    }

}

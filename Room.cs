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
        public void Draw() {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (var x = 0; x < room[0].Length; x++) {
                for (var y = 0; y < room.Length; y++) {
                    Console.Write(room[y][x]);
                }
                Console.Write("\n");
            }
        }

        public char GetCharAt(int x, int y) {
            return room[x][y];
        }
        public char[][] GetRoomArray () {
            return room;
        }
        public int Width {  get { return width; } }
        public int Height { get { return height; } }

    }

}

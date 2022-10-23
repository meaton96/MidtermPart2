using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    abstract class Entity {

        protected readonly char drawChar;
        protected int x, y;
        protected int health;
        protected int attack;
        protected string name;
        protected bool hasMoved;
        
        public Entity(char drawChar, string name) {
            this.drawChar = drawChar;
            this.name = name;   
            hasMoved = false; 
        }
        public int X { get { return x; } set { x = value; } }  
        public int Y { get { return y; } set { y = value; } }
        public int Health { get { return health; } set { health = value; } }
        public int Attack { get { return attack; } set { attack = value; } }
        public bool HasMoved { get { return hasMoved; } set { hasMoved = value; } }

        public char DrawChar { get { return drawChar; } }
        public virtual void Update(Room room) { }
        public virtual void Move(int direction, Room room) {
            if (IsValidMove(direction, room)) {
                switch (direction) {
                    case 0:
                        DrawBlank();
                        x--;
                        Draw();
                        break;
                    case 1:
                        DrawBlank();
                        y--;
                        Draw();
                        break;
                    case 2:
                        DrawBlank();
                        x++;
                        Draw();
                        break;
                    case 3:
                        DrawBlank();
                        y++;
                        Draw();
                        break;
                    default: throw new IndexOutOfRangeException();
                }
                hasMoved = true;
            }
            else {
                throw new Exception("Cannot move to that location");
            }

        }
        private bool IsValidMove(int direction, Room room) {
            switch (direction) {
                case 0: return room.GetRoom()[y][x - 1]!= Room.WALL_CHAR;
                case 1: return room.GetRoom()[y - 1][x] != Room.WALL_CHAR;
                case 2: return room.GetRoom()[y][x + 1] != Room.WALL_CHAR;
                case 3: return room.GetRoom()[y + 1][x] != Room.WALL_CHAR;
                default: throw new IndexOutOfRangeException();

            }
        }
        public void Draw() {
            Console.SetCursorPosition(x, y);
            Console.Write(drawChar);
        }
        public void DrawBlank() {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }
        
    }
}

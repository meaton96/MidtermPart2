using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    abstract class Entity {

        //lots of variables here
        private const double ATTACK_VARIANCE_PERCENT = .3;
        protected int attackVariance;
        protected readonly char drawChar;
        protected int x, y;
        protected int health;
        protected int attack;
        protected string name;
        protected bool hasMoved;

        //also a constructor 
        public Entity(char drawChar, string name) {
            this.drawChar = drawChar;
            this.name = name;   
            hasMoved = false; 
        }
        //lots of getters and setters here
        public int X { get { return x; } set { x = value; } }  
        public int Y { get { return y; } set { y = value; } }
        public int Health { get { return health; } set { health = value; } }
        public int Attack { get { return attack; } set { attack = value; } }
        public bool HasMoved { get { return hasMoved; } set { hasMoved = value; } }
        public String Name { get { return name; } }

        public char DrawChar { get { return drawChar; } }
        //for overloading woo
        public virtual void Update(Room room) { }

        //handle entity movement, checks if the move is valid (doesnt run into a wall)
        //then performs the movement if possible
        public virtual char Move(int direction, Room room) {
            char destChar;
            if (IsValidMove(out destChar, direction, room)) {
                switch (direction) {
                    case Player.LEFT:
                        DrawBlank();
                        x--;
                        Draw();
                        break;
                    case Player.UP:
                        DrawBlank();
                        y--;
                        Draw();
                        break;
                    case Player.RIGHT:
                        DrawBlank();
                        x++;
                        Draw();
                        break;
                    case Player.DOWN:
                        DrawBlank();
                        y++;
                        Draw();
                        break;
                    default: throw new IndexOutOfRangeException();
                }
                hasMoved = true; 
            }
            return destChar;

        }
        //tests if the player can move in the desired direciton
        //returns false if the player is trying to move into a wall space
        private bool IsValidMove(out char destChar, int direction, Room room) {
            destChar = direction switch {
                Player.LEFT => room.GetCharAt(X - 1, y),
                Player.UP => room.GetCharAt(X, y - 1),
                Player.RIGHT => room.GetCharAt(X + 1, y),
                Player.DOWN => room.GetCharAt(X, Y + 1),
                _ => throw new IndexOutOfRangeException(),
            };
            return destChar != Room.WALL_CHAR;
        }
        //for setting variance on enemy or player attack values
        //guess im not currently using this!
        public void SetAttack(int attack) {
            this.attack = attack;
            this.attackVariance = (int)(attack * ATTACK_VARIANCE_PERCENT);
        }
        //draw the entity character
        public void Draw() {
            Console.SetCursorPosition(x, y);
            Console.Write(drawChar);
        }
        //draw a blank space when moving off that space
        public void DrawBlank() {
            Console.SetCursorPosition(x, y);
            Console.Write(Room.EMPTY_CHAR);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    abstract class Entity {

        protected readonly char drawChar;
        protected int x, y;
        protected int health;
        protected int attack;
        protected string name;
        
        public Entity(char drawChar, string name) {
            this.drawChar = drawChar;
            this.name = name;   
        }
        public int X { get { return x; } set { x = value; } }  
        public int Y { get { return y; } set { y = value; } }
        public int Health { get { return health; } set { health = value; } }
        public int Attack { get { return attack; } set { attack = value; } }

        public char DrawChar { get { return drawChar; } }
        protected virtual void Update() { }
        
    }
}

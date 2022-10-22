using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Player : Entity {

        private const char DRAW_CHAR = '\u263A';
        private bool isAlive;
        public Player() : base(DRAW_CHAR, "player") {
            isAlive = true;
        }

        public bool IsAlive { get { return isAlive; } }
        
    }
}

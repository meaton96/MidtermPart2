using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Orc : Enemy {

        public const char DRAW_CHAR = '\u262c';
        private static int numOrcs;
        public Orc() : base(DRAW_CHAR, "orc " + ++numOrcs) {
            health = 25;
            attack = 5;

        }
        public Orc(int x, int y) : base(DRAW_CHAR, "orc " + ++numOrcs){
            X = x;
            Y = y;
            health = 25;
            attack = 5;
        }
    }
}

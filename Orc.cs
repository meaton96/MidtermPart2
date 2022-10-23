using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Orc : Enemy {

        private const char DRAW_CHAR = '\u262c';
        private static int numOrcs;
        public Orc() : base(DRAW_CHAR, "orc " + ++numOrcs) {
            health = 25;
            attack = 5;

        }
    }
}

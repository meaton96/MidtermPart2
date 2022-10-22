using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Orc : Enemy {

        private const char DRAW_CHAR = '\u263b';
        private static int numOrcs;
        public Orc(string drawChar, string name) : base(DRAW_CHAR, "orc + " + ++numOrcs) {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    
    internal class Door : Entity{
        private const char DRAW_CHAR = ' ';

        public Door() : base(DRAW_CHAR, "Door") {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Wall : Entity {

        private const char DRAW_CHAR = '\u0001';
        public Wall() : base(DRAW_CHAR, "Wall") {
        }
    }
}

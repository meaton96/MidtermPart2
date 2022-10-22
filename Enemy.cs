using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Enemy : Entity {
        public Enemy(char drawChar, string name) : base(drawChar, name) {
        }
        public virtual void Move() {

        }
    }
}

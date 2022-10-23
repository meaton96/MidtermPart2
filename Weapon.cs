using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Weapon {
        public string Name { get; set; }
        public int Damage { get; set; }
        public Weapon() {
            Name = "Basic Sword";
            Damage = 5;
        }
    }
}

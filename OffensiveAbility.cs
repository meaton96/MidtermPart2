using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class OffensiveAbility  : Ability{
        int damage;

        public OffensiveAbility(string name, string description, int cost, int coolDown, int damage) 
            : base(name, description, cost, coolDown) {
            this.damage = damage;
        }
        public OffensiveAbility(string name, int damage) : base(name) {
            this.damage = damage;
        }

        public int Damage { get { return damage; } }
    }
}

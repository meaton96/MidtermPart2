using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class DefensiveAbility : Ability{
        private readonly int defensivePower;

        public DefensiveAbility(string name, int defensivePower) : base(name) {
            this.defensivePower = defensivePower;
        }

        public DefensiveAbility(string name, string description, int cost, int coolDown, int defensivePower) : 
            base(name, description, cost, coolDown) {
            this.defensivePower = defensivePower;
        }
        public int DefensivePower {  get { return this.defensivePower; } }
    }
}

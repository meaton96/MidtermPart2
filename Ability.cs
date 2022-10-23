using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    abstract class Ability {
        private string name;
        private string? description;
        private int cost;
        private int coolDown;

        public Ability(string name, string description, int cost, int coolDown) {
            this.name = name;
            this.description = description;
            this.cost = cost;
            this.coolDown = coolDown;
        }
        public Ability(string name) {
            this.name = name;
        }


        public string Name { get { return name; } }
        public string Description { get { if (description != null) return description; else return ""; } }  
        public int Cost { get { return cost; } }
        public int CoolDown { get { return coolDown; } }    
    }
}

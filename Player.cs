using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Player : Entity {

        public const int LEFT = 0;
        public const int UP = 1;
        public const int RIGHT = 2;
        public const int DOWN = 3;

        private const char DRAW_CHAR = '\u0002';
        private Ability[] abilities;
        private Weapon equippedWeapon;
        public Player() : base(DRAW_CHAR, "player") {
            equippedWeapon = new Weapon();
            health = 50;
            abilities = new Ability[4];
            abilities[0] = new OffensiveAbility("Attack", equippedWeapon.Damage);
            abilities[1] = new OffensiveAbility("Fireball", "fires a ball", 5, 0, 10);
            abilities[2] = new DefensiveAbility("Heal", "heals you", 5, 0, 10);
            abilities[3] = new DefensiveAbility("Block", 5);
        }

        public bool IsAlive { get { return health > 0; } }

        
        public String GetAbilityList() {
            string abilityList = "";
            for (var x = 0; x < abilities.Length; x++) {
                abilityList += ($"{x + 1}. {abilities[x].Name} ");
                if (x == 1)
                    abilityList += "\n";
            }
            return abilityList;
        }
        public Ability GetAbilityByIndex(int index) {
            if (index >= 0 || index < abilities.Length)
                return abilities[index];
            return null;
        }
    }
}

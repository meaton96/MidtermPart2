using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Enemy : Entity {
        Random random;
        public static readonly HashSet<char> ENEMY_CHARS = new();
        public Enemy(char drawChar, string name) : base(drawChar, name) {
            random = new Random();
            ENEMY_CHARS.Add(drawChar);
        }
        public override void Update(Room room) {
            try {
                Move(random.Next(4), room);
            }
            catch (Exception) { }
            
        }
        public int PerformAttack() {
            //choose attack when the enemy has more than 1 attack
            return random.Next(attack - attackVariance, attack + attackVariance + 1);
        }
    }
}

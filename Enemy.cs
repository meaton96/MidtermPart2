using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPart2 {
    internal class Enemy : Entity {
        Random random;
        public Enemy(char drawChar, string name) : base(drawChar, name) {
            random = new Random();
        }
        public override void Update(Room room) {
            try {
                Move(random.Next(5), room);
            }
            catch (Exception) { }
            
        }
    }
}

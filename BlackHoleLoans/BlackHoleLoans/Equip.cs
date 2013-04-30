using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
    [Flags]
    public enum Slot { Armor, Weapon };

    class Equip : Item
    {
        public Slot slot_type;
        public Equip(String name, Slot type) : base(name) {
            slot_type = type;
        }
        public Equip(String name, String description, Slot type) : base(name, description) {
            slot_type = type;
        }
        //UNFINISHED
    }
}

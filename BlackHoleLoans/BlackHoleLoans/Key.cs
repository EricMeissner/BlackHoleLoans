using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
    public class Key : Item
    {
        public int key_id;  // corresponds to a specific locked item, must be positive.

        public Key (String key_name, int new_key_id):
            base(key_name)
        {
            if (new_key_id > 0)
                key_id = new_key_id;
            else
                key_id = (new_key_id * -1);
            item_type = Item_Type.Key;
        }

        public Key(String key_name, String notes, int new_key_id) :
            base(key_name, notes)
        {
            if (new_key_id > 0)
                key_id = new_key_id;
            else
                key_id = (new_key_id * -1);
            item_type = Item_Type.Key;
        }

        static public bool isKey(Item target)
        {
            if (target.item_type.Equals(Item_Type.Key))
            {
                return true;
            }
            else
                return false;
        }

        static public int getKEY_ID(Key target)
        {
            return target.key_id;
        }
        static public int getKey_Id(Item target)
        {
            if(isKey(target))
            {
            return ((Key)target).key_id;
            }
            else
                return -1;
        }
    }
}

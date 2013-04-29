using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
    public class Item
    {
        public String name
        {
            get;
            protected set;
        }

        public String Item_Notes
        {
            get;
            protected set;
        }

        public enum Item_Type { Default, Key };
        public Item_Type item_type;
        

        public Item(String n)
        {
            name = n;
            Item_Notes = "";
            item_type = Item_Type.Default;
        }

        public Item(String n, String new_notes)
        {
            name = n;
            Item_Notes = new_notes;
            item_type = Item_Type.Default;
        }
    }
}

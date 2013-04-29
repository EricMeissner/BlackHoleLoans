using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
    public class LockedChest : Chest
    {
        int keyID;
        bool locked;
        public LockedChest(OverWorld ow, Tile t, int newKeyID)   // the other constructor is better
            : base(ow, t)
        {
            keyID = newKeyID;
            locked = true;
        }

        public LockedChest(OverWorld ow, Tile t, int gold_amount, List<Item> item_list, int newKeyID)   // the other constructor is better
            : base(ow, t, gold_amount, item_list)
        {
            keyID = newKeyID;
            locked = true;
        }

        public bool tryKey(Key _key)
        {
            return (_key.key_id == keyID);
        }

        public bool tryKey(Item _key)
        {
            if (_key.item_type == Item.Item_Type.Key)
                return ((((Key)_key).key_id) == keyID);
            else
                return false;
        }
        public override void OnInteract()
        {
            if (locked)
            {
                foreach (Item item in overworld.Game_Ref.party[0].inventory)
                {
                    if (tryKey(item))
                    {
                        this.overworld.Game_Ref.messageQueue.Enqueue("You unlocked it! See what's inside!");
                        locked = false;
                        base.OnInteract();
                        return;
                    }
                }
                this.overworld.Game_Ref.messageQueue.Enqueue("It's locked! You will have to find the right key to open it.");
            }
            else
                base.OnInteract();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
    class LockedDoor : Door
    {
        int keyID;
        bool locked;

        public LockedDoor(OverWorld ow, Tile t, int f, TileMap newdestination, Door newsister, int newKeyID)
            : base(ow, t, f, newdestination, newsister)
        {
            keyID = newKeyID;
            locked = true;
        }

        /*
        public LockedDoor(OverWorld over_world, Tile new_tile, int face, TileMap newTileMapDestination, Door newsister, int newKeyID)
            : base(over_world, new_tile, face, newTileMapDestination, newsister)
        {
            keyID = newKeyID;
            locked = true;
        }
         */

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
            if(locked)
            {
                foreach (Item item in overworld.Game_Ref.party[0].inventory)
                {
                    if (tryKey(item))
                    {
                        this.overworld.Game_Ref.messageQueue.Enqueue("Door Unlocked!");
                        locked = false;
                        return;
                    }
                }
                this.overworld.Game_Ref.messageQueue.Enqueue("Door Is locked! You will have to find the right key to proceed.");
            }
            else
                base.OnInteract();
        }
    }
}

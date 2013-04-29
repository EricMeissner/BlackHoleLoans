using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
    public class Chest: Entity
    {
        public int gold;
        public List<Item> items;
        public bool empty;

        public Chest(OverWorld ow, Tile t, int gold_amount, List<Item> item_list)
            : base(ow, t)
        {
            gold = gold_amount;
            items = item_list;
            empty = false;
        }


        public Chest(OverWorld ow, Tile t)
            : base(ow, t)
        {
            gold = 0;
            items = new List<Item>();
            empty = false;
        }

        public override void OnInteract()
        {
            if (empty == false)
            {
                // popup showing contents, maybe inventory management
                foreach (Item i in items)
                {
                    overworld.Game_Ref.party[0].addItem(i);
                }
                overworld.Game_Ref.party[0].addGold(gold);
                overworld.Game_Ref.messageQueue.Enqueue("Obtained " + gold + " credits.");
                foreach (Item item in items)
                {
                    overworld.Game_Ref.messageQueue.Enqueue("Obtained " + item.name);
                }
                this.empty = true;
            }
            else
            {
                overworld.Game_Ref.messageQueue.Enqueue("Someone has already looted this chest.");
            }
        }
    }
}

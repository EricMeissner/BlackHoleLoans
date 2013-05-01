using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace BlackHoleLoans
{
    public enum NPC_Type { Trader, Doctor, QuestGiver };
    // Trader
    // Doctor: Thus far more of a medical robot; if feeling cruel, may want to make a dangerous malfunctioning type as Enemy
    // QuestGiver
    public class NPC : Entity
    {
        public NPC_Type type;
        public String message
        {
            get
            {
                if (type == NPC_Type.Doctor)
                {
                    return "The ancient medical robot sparks and jerks to life.\n" +
                    "'Full party healing cost 100 UNIT NOT FOUND. CONFIRM!?'\n" +
                    "Y: 'Yes, we want healing'  N: 'Um... no thankyou.'";
                }
                return "No message implemented for this NPC\n Z: exit";
            }
        }
        
        public NPC(OverWorld ow, Tile t, int facing, NPC_Type vtype) : base(ow, t, facing)
        {
            type = vtype;
        }
        public NPC(OverWorld ow, Tile t, NPC_Type vtype)
            : base(ow, t, 2)
        {
            type = vtype;
        }

        public override void OnInteract()
        {
            overworld.Game_Ref.npc_menu._asker = this;
        }
        public bool Answer(KeyboardState input)
        {
            if (type == NPC_Type.Trader)
            {
                
            }
            else if (type == NPC_Type.Doctor)
            {
                if (input.IsKeyDown(Keys.Y)) {
                    Player [] party_ref= overworld.Game_Ref.party;
                    if(party_ref[0].GetPlayerStats().Health==party_ref[0].GetPlayerStats().TotalHealth &&
                        party_ref[1].GetPlayerStats().Health==party_ref[1].GetPlayerStats().TotalHealth &&
                        party_ref[2].GetPlayerStats().Health==party_ref[2].GetPlayerStats().TotalHealth) 
                    {
                        overworld.Game_Ref.messageQueue.Enqueue("'Your party is already fully healed.\n'" +
                            "'Medical protocals require you to be injured!'\n" +
                            "'Please return after injuring yourself.'"); // may split into multiple messages
                    }
                    else if(party_ref[0].Gold >= 100){
                        foreach (Player p in party_ref) {
                            p.GetPlayerStats().FullHeal();
                        }
                        party_ref[0].addGold(-100);
                        overworld.Game_Ref.messageQueue.Enqueue("'You have been fully healed.' \n"
                            + "'This unit looks forward to profiting from your injury again.'");
                    }
                    else {
                        overworld.Game_Ref.messageQueue.Enqueue("'Insufficient funds!' \n'Stop bleeding on my floor, deadbeat!'");
                    }   

                    return true;
                }
                else if (input.IsKeyDown(Keys.N))
                {
                    overworld.Game_Ref.messageQueue.Enqueue("'This unit wishes you great wealth and poor health.'\n" +
                    "'Please return soon...'");
                    return true;
                }
                else
                    return false;
            }
            else if (type == NPC_Type.QuestGiver)
            {
                
            }
            //default
            if (input.IsKeyDown(Keys.Z))
            {
                return true;
            }
            else
                return false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlackHoleLoans.PlayerRelated;

namespace BlackHoleLoans
{
  class Player
  {
    PlayerStatistics playerStats;
    Texture2D[] playerSprites;
    Texture2D partyMember1, partyMember2;
    string name;
    int[] classIdentifier;
    int gold;



    public Player(int[] stats, Texture2D[]pS, Texture2D pM1, Texture2D pM2, int[] cI, string n)
    //also pass in array of textures (player dir movement) & an int (for which class
    //the player is?) - don't need anything for the race
    {
      playerSprites = pS;
      partyMember1 = pM1;
      partyMember2 = pM2;
      classIdentifier = cI;
      gold = 0;
      name = n;
      playerStats = new PlayerStatistics(stats[0], stats[1], stats[2]);
    }


  }
}

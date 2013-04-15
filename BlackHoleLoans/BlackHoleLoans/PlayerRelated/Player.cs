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
    string name;
    int classIdentifier;//1=warrior, 2=wizard, 3=shooter
    int gold;



    public Player(int[] stats, Texture2D[]pS, int cI, string n)
    //also pass in array of textures (player dir movement) & an int (for which class
    //the player is?) - don't need anything for the race
    {
      playerSprites = pS;
      classIdentifier = cI;
      gold = 0;
      name = n;
      playerStats = new PlayerStatistics(stats[0], stats[1], stats[2]);
    }

    public Player(int[]stats, Texture2D []partyMemberSprite, int cI)
    {
      playerSprites = partyMemberSprite;
      classIdentifier = cI;
      playerStats = new PlayerStatistics(stats[0], stats[1], stats[2]);

      if (cI == 1)
        name = "WARRIOR";
      else if (cI == 2)
        name = "PSIONIC";
      else
        name = "SHOOTER";
    }

    public Texture2D[] GetPlayerSprites()
    {
      return playerSprites;
    }

  }
}

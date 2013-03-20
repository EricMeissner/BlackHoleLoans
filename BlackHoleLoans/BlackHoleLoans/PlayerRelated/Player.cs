﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlackHoleLoans.PlayerRelated
{
  class Player
  {
    PlayerStatistics playerStats;
    Texture2D[] playerDirMovement;




    public Player(int atk, int def, int con)//also pass in array of textures (player dir movement) & an int (for which class
      //the player is?) - don't need anything for the race
    {


      playerStats = new PlayerStatistics(atk, def, con);
    }


  }
}

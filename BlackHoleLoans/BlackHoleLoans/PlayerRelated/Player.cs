﻿using System;
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
  public class Player
  {
    PlayerStatistics playerStats;
    Texture2D[] playerSprites;
    int classIdentifier;//1=warrior, 2=wizard, 3=shooter
    int gold;
    public int Gold { get { return gold; } }

    public string Name { get; set; }
    public Skill skillA{get;set;}
    public Skill skillB{get;set;}
    public bool isFainted { get; set; }
    public bool hasGone { get; set; }
    public int lastPlayerHealth { get; set; }
    public List<Item> inventory; // only for main character?
      /*
    public List<Equip> gear;
    PlayerStatistics ModifiedStats
    {
        get
        {
            //TODO implementation;
        }
    }
    */

    public Player(int atk, int def, int con, Texture2D[]pS, int cI, string n, Skill a, Skill b)
    //also pass in array of textures (player dir movement) & an int (for which class
    //the player is?) - don't need anything the race
    {
      playerStats = new PlayerStatistics(atk, def, con);
      playerSprites = pS;
      classIdentifier = cI;
      gold = 0;
      Name = n;
      skillA = a;
      skillB = b;
      isFainted = false;
      hasGone = false;
      inventory = new List<Item>();
    }

    public Player(int[]stats, Texture2D []partyMemberSprite, int cI, Skill a, Skill b)
    {
      playerSprites = partyMemberSprite;
      classIdentifier = cI;
      skillA = a;
      skillB = b;
      playerStats = new PlayerStatistics(stats[0], stats[1], stats[2]);
      inventory = new List<Item>();

      if (cI == 1)
        Name = "WARRIOR";
      else if (cI == 2)
        Name = "PSIONIC";
      else
        Name = "SHOOTER";
    }

    public Texture2D[] GetPlayerSprites()
    {
      return playerSprites;
    }

    public PlayerStatistics GetPlayerStats()
    {
      return playerStats;
    }

    public int ExecuteBasicAttack(Enemy e)
    {
      int damage = playerStats.Attack - e.GetEnemyStats().Defence;
      if (damage <= 0)
      {
        damage = 1;
      }
      e.GetEnemyStats().SubtractHealth(damage);
      return damage;
    }

    public void ExecuteSkillA(Enemy e, Player p)
    {
      if (skillA.isDamage)
      {
        int damage = (int)(playerStats.Concentration * skillA.skillRatio) - e.GetEnemyStats().Defence;
        if (damage < 0)
        {
          damage = 1;
        }
        e.GetEnemyStats().SubtractHealth(damage);
      }
      else if (skillA.isHealing)
      {
        int health = (int)(playerStats.Concentration * skillA.skillRatio);
        p.GetPlayerStats().addHealth(health);
      }
    }

    public void ExecuteSkillB(Enemy e, Player p)
    {
      if (skillB.isDamage)
      {
        int damage = (int)(playerStats.Concentration * skillB.skillRatio) - e.GetEnemyStats().Defence;
        if (damage < 0)
        {
          damage = 1;
        }
        e.GetEnemyStats().SubtractHealth(damage);
      }
      else if (skillB.isHealing)
      {
        int health = (int)(playerStats.Concentration * skillB.skillRatio);
        p.GetPlayerStats().addHealth(health);
      }
    }
    public bool addItem(Item i)
    {
        inventory.Add(i);

        return true; //put in code for full inventory here if need-be

    }

    public void addGold(int newgold)
    {
        gold += newgold;
    }

    public String toString()
    {
        String result = "";
        result += "Player info: ";
        result += "Name: " + Name + "; ";
        result += "Gold: " + gold + "; ";
        //result += "Class id: " + classIdentifier + "\n";
        result += "Inventory: ";
        foreach (Item i in inventory)
        {
            result += "" + i.name + ", ";
        }
        return result;
    }
  }
}

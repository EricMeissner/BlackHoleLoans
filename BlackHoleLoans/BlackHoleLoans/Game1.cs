using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BlackHoleLoans.PlayerRelated;

namespace BlackHoleLoans
{
  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    MainMenu mainMenu;
    CharacterCreation characterCreation;
    OverWorld OW;
    OWMenu ingameMenu;
    Player[] party;
    int currentGameState;

    //Eric's code start
    int OWcontrolspeed = 4;
    int OWentityspeed = 2;
    int action_timer = 0;
    int player_timer=0, enemy_timer = 0;
    //End

    //Chad
    public Combat combat;
    public Enemy[] enemy;
    //end chad
    bool createdCombat, pausedGame;
    int combatBackgroundID;
    KeyboardState keyState, prevKeystate;
    float volume=0.25f;//0-muted, .25-not muted
    Texture2D boss1, boss2, boss3;
    bool startedBossFight = false;
    EndGame endGame;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;
      IsMouseVisible = true;

      mainMenu = new MainMenu(Content, graphics.PreferredBackBufferWidth,
        graphics.PreferredBackBufferHeight);
      characterCreation = new CharacterCreation();

      currentGameState = 0;//change back to 0

      createdCombat = pausedGame = false;
      keyState = Keyboard.GetState();
      prevKeystate = keyState;
    }
    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here

      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      // TODO: use this.Content to load your game content here
      mainMenu.setSpriteBatch(spriteBatch);
      characterCreation.setSpriteBatch(spriteBatch);

      mainMenu.LoadContent();
      characterCreation.LoadContent(Content);

      boss1 = Content.Load<Texture2D>("EnemySprites/BHLBoss");
      boss2 = Content.Load<Texture2D>("EnemySprites/BHLBoss2");
      boss3 = Content.Load<Texture2D>("EnemySprites/BHLBoss3");
    }

    protected void LoadOverWorldContent()
    {

      //Eric's code start
      OW.LoadTileTextures(Content, "Textures/grass", "Textures/dirt", "Textures/ground",
            "Textures/mud", "Textures/road", "Textures/bricks", "Textures/blackYellowFloor",
            "Textures/brownFloor", "Textures/redFloor", "Textures/grayFloor");
      OW.LoadAvatar(Content, (party[0].GetPlayerSprites()));

      foreach (TileMap map in OW.mapList)
      {
        foreach (Entity current in map.EntityList)
        {
          current.LoadEntityAvatar(Content);
        }
      }
      //end

    }

    private void CreateMainMenu()
    {
      mainMenu = new MainMenu(Content, graphics.PreferredBackBufferWidth,
         graphics.PreferredBackBufferHeight);
    }

    private void CreateCharacterCreation()
    {
      characterCreation = new CharacterCreation();
    }

    private void CreateOverWorld()
    {
      //Eric's code start

      OW = ContentRepository.getMap(3, this);
      //OWlist = new List<OverWorld>();
      //OWlist.Add(OW);
      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;

      TileMap tempTileMap = ContentRepository.getMap(4);
      TileMap tempTileMap2 = ContentRepository.getMap(5);
      TileMap tempTileMap3 = ContentRepository.getMap(8);
      TileMap tempTileMap4 = ContentRepository.getMap(9);
      TileMap tempTileMap5 = ContentRepository.getMap(10);
      OW.mapList.Add(tempTileMap);
      OW.mapList.Add(tempTileMap2);
      OW.mapList.Add(tempTileMap3);
      OW.mapList.Add(tempTileMap4);
      OW.mapList.Add(tempTileMap5);

      #region Enemy Creation

      Entity enemy1 = new Enemy(OW, tempTileMap.getTile(3, 4), 0, 
        new int[] { 2, 2, 3, 3, 0, 0, 0, 0, 1, 1, 1, 1 , 2, 2, 3, 3 } , "Blue Spider", false);
      enemy1.setAvatarFileString("EnemySprites/BlueCreatureRight", "EnemySprites/BlueCreatureRight",
        "EnemySprites/BlueCreatureLeft", "EnemySprites/BlueCreatureLeft");
      tempTileMap.EntityList.Add(enemy1);
      
      /*
      Entity enemy1 = new Enemy(OW, tempTileMap.getTile(3, 4), 0, "Blue Spider", false,1);
      enemy1.setAvatarFileString("EnemySprites/BlueCreatureRight", "EnemySprites/BlueCreatureRight",
        "EnemySprites/BlueCreatureLeft", "EnemySprites/BlueCreatureLeft");
      tempTileMap.EntityList.Add(enemy1);
      */


      Entity enemy2 = new Enemy(OW, tempTileMap.getTile(8, 11), 0,
        new int[] { 0, 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 0, 0, 1, 1 }, "Blue Spider", false);
      enemy2.setAvatarFileString("EnemySprites/BlueCreatureRight", "EnemySprites/BlueCreatureRight",
        "EnemySprites/BlueCreatureLeft", "EnemySprites/BlueCreatureLeft");
      tempTileMap.EntityList.Add(enemy2);
      
      /*
      Entity enemy2 = new Enemy(OW, tempTileMap.getTile(8, 11), 0, "Blue Spider", false);
      enemy2.setAvatarFileString("EnemySprites/BlueCreatureRight", "EnemySprites/BlueCreatureRight",
        "EnemySprites/BlueCreatureLeft", "EnemySprites/BlueCreatureLeft");
      tempTileMap.EntityList.Add(enemy2);
      */


      Entity enemy3 = new Enemy(OW, tempTileMap2.getTile(2, 12), 0, 
        new int[] { 1, 1, 3, 3, 3, 3, 1, 1 },
        "Purple Spider", false);
      enemy3.setAvatarFileString("EnemySprites/PurpleSpiderRight", "EnemySprites/PurpleSpiderRight",
        "EnemySprites/PurpleSpiderLeft", "EnemySprites/PurpleSpiderLeft");
      tempTileMap2.EntityList.Add(enemy3);


      Entity enemy4 = new Enemy(OW, tempTileMap2.getTile(1, 2), 0,
  new int[] {3,2,2,1,2,2,3,2,2,1,0,0,3,0,0,1,0,0},
  "Purple Spider", false);
      enemy4.setAvatarFileString("EnemySprites/PurpleSpiderRight", "EnemySprites/PurpleSpiderRight",
        "EnemySprites/PurpleSpiderLeft", "EnemySprites/PurpleSpiderLeft");
      tempTileMap2.EntityList.Add(enemy4);

      Entity enemy5 = new Enemy(OW, tempTileMap3.getTile(1, 3), 0,
new int[] {2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3},
"Purple Monster", false);
      enemy5.setAvatarFileString("EnemySprites/PurpleMonsterRight", "EnemySprites/PurpleMonsterRight",
        "EnemySprites/PurpleMonsterLeft", "EnemySprites/PurpleMonsterLeft");
      tempTileMap3.EntityList.Add(enemy5);

      Entity enemy6 = new Enemy(OW, tempTileMap3.getTile(8, 3), 0,
new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2 },
"Purple Monster", false);
      enemy6.setAvatarFileString("EnemySprites/PurpleMonsterRight", "EnemySprites/PurpleMonsterRight",
        "EnemySprites/PurpleMonsterLeft", "EnemySprites/PurpleMonsterLeft");
      tempTileMap3.EntityList.Add(enemy6);

      Entity enemy7 = new Enemy(OW, tempTileMap3.getTile(8, 14), 0,
new int[] { 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
"Purple Monster", false);
      enemy7.setAvatarFileString("EnemySprites/PurpleMonsterRight", "EnemySprites/PurpleMonsterRight",
        "EnemySprites/PurpleMonsterLeft", "EnemySprites/PurpleMonsterLeft");
      tempTileMap3.EntityList.Add(enemy7);

      Entity enemy8 = new Enemy(OW, tempTileMap3.getTile(1, 14), 0,
new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  ,0, 0, 0, 0, 0, 0, 0},
"Purple Monster", false);
      enemy8.setAvatarFileString("EnemySprites/PurpleMonsterRight", "EnemySprites/PurpleMonsterRight",
        "EnemySprites/PurpleMonsterLeft", "EnemySprites/PurpleMonsterLeft");
      tempTileMap3.EntityList.Add(enemy8);

      Entity enemy9 = new Enemy(OW, tempTileMap3.getTile(3, 11), 0,
new int[] { 2,2,2,3,3,3,3,3,0,0,0,1,1,1,1,1 },
"Purple Monster", false);
      enemy9.setAvatarFileString("EnemySprites/PurpleMonsterRight", "EnemySprites/PurpleMonsterRight",
        "EnemySprites/PurpleMonsterLeft", "EnemySprites/PurpleMonsterLeft");
      tempTileMap3.EntityList.Add(enemy9);

      Entity enemy10 = new Enemy(OW, tempTileMap3.getTile(6, 6), 0,
new int[] { 0,0,0,1,1,1,1,1,2,2,2,3,3,3,3,3 },
"Purple Monster", false);
      enemy10.setAvatarFileString("EnemySprites/PurpleMonsterRight", "EnemySprites/PurpleMonsterRight",
        "EnemySprites/PurpleMonsterLeft", "EnemySprites/PurpleMonsterLeft");
      tempTileMap3.EntityList.Add(enemy10);

      Entity enemy11 = new Enemy(OW, tempTileMap5.getTile(1, 7), 0,
new int[] {2},"Boss", true);
      enemy11.setAvatarFileString("EnemySprites/BHLBoss", "EnemySprites/BHLBoss", "EnemySprites/BHLBoss", "EnemySprites/BHLBoss");
      tempTileMap5.EntityList.Add(enemy11);


      #endregion

      #region Map Creation (includes doors)

      Entity temp;

      Entity[] minerals = new Entity[52];

      for (int i = 1; i <= 4; i++)
      {
        for (int j = 1; j <= 14; j++)
        {
          if (j == 7)
            continue;
          minerals[j-1] = new Entity(OW, tempTileMap5.getTile(i, j));
          minerals[j-1].setAvatarFileString("Textures/minerals");
          tempTileMap5.EntityList.Add(minerals[j-1]);
        }
      }


      temp = new Door(OW, OW.OWmap.getTile(2, 7), 3, tempTileMap, null);
      temp.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      Door tempSister = new Door(OW, tempTileMap.getTile(1, 0), 1, OW.OWmap, (Door)temp);
      tempSister.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp).sister = tempSister;
      OW.EntityList.Add(temp);
      tempTileMap.EntityList.Add(tempSister);
      //end


      Entity temp2 = new Door(OW, tempTileMap.getTile(10, 15), 3, tempTileMap2, null);//10 15
      temp2.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
     "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");

      Door tempSister2 = new Door(OW, tempTileMap2.getTile(10, 0), 1, tempTileMap, (Door)temp2);
      tempSister2.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp2).sister = tempSister2;
      tempTileMap.EntityList.Add(temp2);
      tempTileMap2.EntityList.Add(tempSister2);



      Entity temp3 = new Door(OW, tempTileMap2.getTile(0, 8), 2, tempTileMap3, null);//10 15
      temp3.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
     "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");

      Door tempSister3 = new Door(OW, tempTileMap3.getTile(11, 7), 0, tempTileMap2, (Door)temp3);
      tempSister3.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp3).sister = tempSister3;
      tempTileMap2.EntityList.Add(temp3);
      tempTileMap3.EntityList.Add(tempSister3);



      Entity temp4 = new Door(OW, tempTileMap3.getTile(6, 0), 1, tempTileMap4, null);//10 15
      temp4.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
     "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");

      Door tempSister4 = new Door(OW, tempTileMap4.getTile(6, 15), 3, tempTileMap3, (Door)temp4);
      tempSister4.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp4).sister = tempSister4;
      tempTileMap3.EntityList.Add(temp4);
      tempTileMap4.EntityList.Add(tempSister4);



      Entity temp5 = new Door(OW, tempTileMap4.getTile(0, 7), 2, tempTileMap5, null);//10 15
      temp5.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
     "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");

      Door tempSister5 = new Door(OW, tempTileMap5.getTile(11, 7), 0, tempTileMap4, (Door)temp5);
      tempSister5.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp5).sister = tempSister5;
      tempTileMap4.EntityList.Add(temp5);
      tempTileMap5.EntityList.Add(tempSister5);

      #endregion
    }

    private void CreateCombat(Enemy e)
    {
      PlayerStatistics playerStats = party[0].GetPlayerStats();
      int[] pToE = new int[4] //player to enemy stats
      {
        playerStats.Attack, playerStats.Defence, playerStats.Concentration, playerStats.Health
      };

      e.SetEnemySprites();
      Texture2D enemySprite = e.EnemySprite();
      string enemyName = e.Name;

      if (!e.IsTheBoss())
      {
        enemy = new Enemy[3]
            {
              /*
                new Enemy(pToE[0]-5,pToE[1]-5,pToE[2]-5,1, enemyName, enemySprite),//Can also add skills
                new Enemy(pToE[0]-3,pToE[1]-3,pToE[2]-3,1, enemyName, enemySprite, new Skill(Skills.Blast)),
                new Enemy(pToE[0]-1,pToE[1]-1,pToE[2]-1,1, enemyName, enemySprite, new Skill(Skills.Blast), new Skill(Skills.Blast))
            };//Change health back to normal
               * */
                new Enemy(5,5,5,20, enemyName, enemySprite),//Can also add skills
                new Enemy(5,5,5,15, enemyName, enemySprite, new Skill(Skills.Blast)),
                new Enemy(5,5,5,10, enemyName, enemySprite, new Skill(Skills.Blast), new Skill(Skills.Blast))
            };//Change health back to normal
      }
      else
      {
        enemy = new Enemy[3]
            {
                new Enemy(pToE[0]-5,pToE[1]-5,pToE[2]-5,20, "Boss Form 1", boss1,
                  new Skill(Skills.LaserSword), new Skill(Skills.LaserSword)),//Can also add skills

                new Enemy(pToE[0]-3,pToE[1]-3,pToE[2]-3,20, "Boss Form 2", boss2, 
                  new Skill(Skills.Blast), new Skill(Skills.Blast)),

                new Enemy(pToE[0]-1,pToE[1]-1,pToE[2]-1,20, "Boss form 3", boss3, 
                  new Skill(Skills.Leech), new Skill(Skills.Leech))


            };//Change health back to normal
        startedBossFight = true;
      }

      combat = new Combat(Content, graphics.PreferredBackBufferHeight,
          graphics.PreferredBackBufferWidth, this, party, enemy, volume);
      combat.LoadContent();
      combat.SetSpriteBatch(spriteBatch);

      combatBackgroundID = OW.OWmap.getTile(OW.Xpos, OW.Ypos).Texture;

    }

    private void RestartFromMainMenu()
    {
      CreateMainMenu();
      CreateCharacterCreation();
      CreateOverWorld();
      LoadContent();
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
    /// 
    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      // Allows the game to exit
      if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        this.Exit();
      // TODO: Add your update logic here
      keyState = Keyboard.GetState();


      #region main menu
      if (currentGameState == 0)
      {
        currentGameState = mainMenu.Update();
        if (currentGameState == -1)
          this.Exit();
      }
      #endregion

      #region Character Creation

      else if (currentGameState == 1)
      {
        characterCreation.Update();
        currentGameState = characterCreation.BackToMM();
        if (characterCreation.StartOverworld())
        {
          party = characterCreation.CreatePlayer();
          currentGameState = 2;
          CreateOverWorld();
          LoadOverWorldContent();
        }
      }
      #endregion

      #region OW

      else if (currentGameState == 2)//Overworld change back to 2
      {
        if (keyState.IsKeyDown(Keys.M))
        {
          currentGameState = 4;
        }
        //action_timer++;
        enemy_timer++;
        player_timer++;

        if (action_timer >= 60)//change back to 60!!!
        {
          action_timer = 0;
        }
        if(enemy_timer >= 60){
          enemy_timer = 0;
        }
        if (player_timer >= 60)
        {
          player_timer = 0;
        }
        //Eric's code start
        //Console.WriteLine("Player x position"+OW.Xpos+" Player y position"+OW.Ypos);
        #region Tapping directional keys
        if (prevKeystate.IsKeyUp(Keys.Up) && keyState.IsKeyDown(Keys.Up))
        {
          this.OW.playerStep(0);
          //action_timer = 0;
          player_timer = 0;
        }

        else if (prevKeystate.IsKeyUp(Keys.Right) && keyState.IsKeyDown(Keys.Right))
        {
          this.OW.playerStep(1);
          //action_timer = 0;
          player_timer = 0;
        }

        else if (prevKeystate.IsKeyUp(Keys.Down) && keyState.IsKeyDown(Keys.Down))
        {
          this.OW.playerStep(2);
          player_timer = 0;
          //action_timer = 0;
        }

        else if (prevKeystate.IsKeyUp(Keys.Left) && keyState.IsKeyDown(Keys.Left))
        {
          this.OW.playerStep(3);
          player_timer = 0;
          //action_timer = 0;
        }
        #endregion

        #region Holding down directional keys
        else if (player_timer % (60 / OWcontrolspeed) == 0)
        {
          if (keyState.IsKeyDown(Keys.Up))
          {
            this.OW.playerStep(0);
          }
          else if (keyState.IsKeyDown(Keys.Right))
          {
            this.OW.playerStep(1);
          }
          else if (keyState.IsKeyDown(Keys.Down))
          {
            this.OW.playerStep(2);
          }
          else if (keyState.IsKeyDown(Keys.Left))
          {
            this.OW.playerStep(3);
          }
          else
          {
            player_timer = 0;
          }
          if (keyState.IsKeyDown(Keys.Z))
          {
            this.OW.playerInteract();
          }
          //end
        }
        #endregion

        if (enemy_timer % (60 / OWentityspeed) == 0)
        {
          foreach (Entity current in OW.EntityList)
          {
            current.OnUpdate();
          }
        }
        OW.Draw(spriteBatch, graphics);

        if (OW.IsInCombat() || OW.CheckForEnemyCollision())
        {
          currentGameState = 3;
        }
      }
      #endregion

      #region Combat

      else if (currentGameState == 3)//Combat
      {
        if (!createdCombat)
        {
          CreateCombat(OW.GetOpponent());
          createdCombat = true;
        }

        combat.Update(gameTime);

        if (combat.WonFight() && combat.messageQueue.Count==0)
        {
          combat.StopSound();
          OW.GetOpponent().remove();
          OW.FinishedCombat();

          foreach (Player player in party)
          {
            player.isFainted = false;
            player.GetPlayerStats().FullHeal();
            player.hasGone = false;
            player.GetPlayerStats().LevelUp();
          }
          createdCombat = false;

          if (startedBossFight)
          {
            currentGameState = 5;
            Texture2D[] partySprites = new Texture2D[3];
            partySprites[0] = party[0].GetPlayerSprites()[1];
            partySprites[1] = party[1].GetPlayerSprites()[0];
            partySprites[2] = party[2].GetPlayerSprites()[0];

            endGame = new EndGame(1, spriteBatch, Content, partySprites);//Pass in a vlue for winning
            endGame.LoadContent();
          }

          else
          {
            currentGameState = 2;
          }

        }



          
        else if (combat.LostFight())
        {
          if (combat.messageQueue.Count == 0)
          {
            combat.StopSound();
            //RestartFromMainMenu();
            currentGameState = 5;
            createdCombat = false;

            if (startedBossFight)
            {
              Texture2D[] partySprites = new Texture2D[3];
              partySprites[0] = party[0].GetPlayerSprites()[1];
              partySprites[1] = party[1].GetPlayerSprites()[0];
              partySprites[2] = party[2].GetPlayerSprites()[0];
              endGame = new EndGame(2, spriteBatch, Content, partySprites);//pass in a value for losing boss fight
            }
            else
            {
              Texture2D[] partySprites = new Texture2D[3];
              partySprites[0] = party[0].GetPlayerSprites()[1];
              partySprites[1] = party[1].GetPlayerSprites()[0];
              partySprites[2] = party[2].GetPlayerSprites()[0];
              endGame = new EndGame(3, spriteBatch, Content, partySprites);//pass in a value for dying in combat
            }
            endGame.LoadContent();
          }
        }

        else if (combat.RanAway())
        {
          combat.StopSound();
          currentGameState = 2;
          OW.GetOpponent().ParalyzeEnemy();
          OW.FinishedCombat();
          createdCombat = false;

          foreach (Player player in party)
          {
            player.isFainted = false;
            //player.GetPlayerStats().FullHeal();
            player.hasGone = false;
          }
        }
      }

      #endregion

      #region OWMenu
      else if (currentGameState == 4)//In-game menu
      {
        if (!pausedGame)
        {
          ingameMenu = new OWMenu(party, volume);
          ingameMenu.LoadContent(Content);
          pausedGame = true;
        }
        volume = ingameMenu.GetVol();
        currentGameState = ingameMenu.Update(this);
        if (currentGameState == 0)
          RestartFromMainMenu();
        if (currentGameState != 4)
        {
          pausedGame = false;
        }
      }
      #endregion

      #region Endgame
      else if (currentGameState == 5)
      {
        endGame.Update();
        if (endGame.LeaveEndGame())
        {
          currentGameState = 0;
          RestartFromMainMenu();
        }
      }
      #endregion
      //Eric's Code start
      else
      {
        //Console.WriteLine("Error, Unknown gamestate reached: gamestate = " + currentGameState);
        this.Exit();
      }
      //End
      prevKeystate = keyState;
      base.Update(gameTime);
    }

 
    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {

      // TODO: Add your drawing code here
      spriteBatch.Begin();

      if (currentGameState == 0)
      {
        mainMenu.Draw();
      }
      else if (currentGameState == 1)
      {
        characterCreation.Draw();
      }
      else if (currentGameState == 2)//change back to 2
      {
        spriteBatch.End();
        OW.Draw(spriteBatch, graphics);

      }

      else if (currentGameState == 3)
      {
        if (createdCombat)
          combat.Draw(gameTime, combatBackgroundID);
      }

      else if (currentGameState == 4)
      {
        if (pausedGame)
          ingameMenu.Draw(spriteBatch);
      }

      else if (currentGameState == 5)
      {
        GraphicsDevice.Clear(Color.Black);
        endGame.Draw();
      }

      if (currentGameState != 2)//change back to 2
        spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}

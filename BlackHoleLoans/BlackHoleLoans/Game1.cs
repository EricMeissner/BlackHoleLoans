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
    Player []party;
    int currentGameState;

    //Eric's code start
    int OWcontrolspeed = 4;
    int OWentityspeed = 2;
    int action_timer = 0;
    //End

    //Chad
    public Combat combat;
    float frameRate;
    public Enemy[] enemy;
    //end chad
    bool createdCombat;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;
      IsMouseVisible = true;

      mainMenu = new MainMenu(Content, graphics.PreferredBackBufferWidth,
        graphics.PreferredBackBufferHeight);

      currentGameState = 0;//change back to 0
      characterCreation = new CharacterCreation();

      createdCombat = false;

      //Eric's code start

      OW = ContentRepository.getMap(3, this);
      //OWlist = new List<OverWorld>();
      //OWlist.Add(OW);
      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;

      TileMap tempTileMap = ContentRepository.getMap(4);
      OW.mapList.Add(tempTileMap);
      
      Entity temp = new Enemy(OW, OW.OWmap.getTile(5, 5), 0, new int[] { 0, 1, 2, 3 });
      temp.setAvatarFileString("EntityAvatar/RedTest/RedArrowUp", "EntityAvatar/RedTest/RedArrowRight",
          "EntityAvatar/RedTest/RedArrowDown", "EntityAvatar/RedTest/RedArrowLeft");
      tempTileMap.EntityList.Add(temp);
      
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

      //map2 crashes when going back through the door
      TileMap tempTileMap2 = ContentRepository.getMap(5);
      OW.mapList.Add(tempTileMap2);
      
      Entity temp2 = new Door(OW, tempTileMap.getTile(10, 15), 3, tempTileMap2, null);//10 15
      temp2.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
     "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      
      Door tempSister2 = new Door(OW, tempTileMap2.getTile(10, 0), 1, tempTileMap, (Door)temp2);
      tempSister2.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp2).sister = tempSister2;
      tempTileMap.EntityList.Add(temp2);
      tempTileMap2.EntityList.Add(tempSister2);

      
      //Map 3 - Change names..lol
      TileMap tempTileMap3 = ContentRepository.getMap(5);//change
      OW.mapList.Add(tempTileMap3);

      Entity temp3 = new Door(OW, tempTileMap2.getTile(10, 15), 3, tempTileMap3, null);//10 15
      temp3.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
     "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");

      Door tempSister3 = new Door(OW, tempTileMap3.getTile(10, 0), 1, tempTileMap2, (Door)temp3);
      tempSister3.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp3).sister = tempSister3;
      tempTileMap2.EntityList.Add(temp3);
      tempTileMap3.EntityList.Add(tempSister3);
      
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
    }

    protected void LoadOverWorldContent()
    {

      //Eric's code start
      OW.LoadTileTextures(Content, "Textures/grass", "Textures/dirt", "Textures/ground",
            "Textures/mud", "Textures/road", "Textures/bricks");
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

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
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
      KeyboardState keyState = Keyboard.GetState();

      if (currentGameState == 0)
      {
        currentGameState = mainMenu.Update();
        if (currentGameState == -1)
          this.Exit();
      }

      else if (currentGameState == 1)
      {
        characterCreation.Update();
        currentGameState = characterCreation.BackToMM();
        if (characterCreation.StartOverworld())
        {
          party = characterCreation.CreatePlayer();
          currentGameState = 2;
          LoadOverWorldContent();
        }
      }

      else if (currentGameState == 2)//Overworld change back to 2
      {
        action_timer++;
        if (action_timer >= 60)
        {
          action_timer = 0;
        }
        //Eric's code start
        //Console.WriteLine("Player x position"+OW.Xpos+" Player y position"+OW.Ypos);
        if (action_timer % (60 / OWcontrolspeed) == 0)
        {
          if (keyState.IsKeyDown(Keys.Up))
          {
            this.OW.playerStep(0);
          }
          if (keyState.IsKeyDown(Keys.Right))
          {
            this.OW.playerStep(1);
          }
          if (keyState.IsKeyDown(Keys.Down))
          {
            this.OW.playerStep(2);
          }
          if (keyState.IsKeyDown(Keys.Left))
          {
            this.OW.playerStep(3);
          }
          if (keyState.IsKeyDown(Keys.Z))
          {
            this.OW.playerInteract();
          }
          //end
        }
        if (action_timer % (60 / OWentityspeed) == 0)
        {
          foreach (Entity current in OW.EntityList)
          {
            current.OnUpdate();
          }
        }
        OW.Draw(spriteBatch, graphics);
        if (OW.IsInCombat())
        {
          currentGameState = 3;
        }
      }

      else if (currentGameState == 3)//Combat
      {
        if (!createdCombat)
        {
          CreateCombat();
          createdCombat = true;
        }
        combat.Update(gameTime);
        if (combat.WonFight())
        {
          currentGameState = 2;
          OW.FinishedCombat();
        }
      }

      else if (currentGameState == 4)//In-game menu
      {

      }

      //Eric's Code start
      else
      {
        Console.WriteLine("Error, Unknown gamestate reached: gamestate = " + currentGameState);
        this.Exit();
      }
      //End
      base.Update(gameTime);
    }

    private void CreateCombat()
    {
      //Chad
      enemy = new Enemy[3]
            {
                new Enemy(5,5,5,1,"Dummy1"),
                new Enemy(5,5,5,1,"Dummy2"),
                new Enemy(5,5,5,1,"Dummy3")
            };
      combat = new Combat(Content, graphics.PreferredBackBufferHeight,
          graphics.PreferredBackBufferWidth, this, party, enemy);
      combat.LoadContent();
      combat.SetSpriteBatch(spriteBatch);

      //end chad
    }
    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Green);

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
        if(createdCombat)
          combat.Draw(gameTime);
      }

      if (currentGameState != 2)//change back to 2
        spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}

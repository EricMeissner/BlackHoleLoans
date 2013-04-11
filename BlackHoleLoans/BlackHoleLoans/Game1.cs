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
    Player player;
    int currentGameState;

    //Eric's code start
    int OWcontrolspeed = 4;
    int OWentityspeed = 2;
    int action_timer = 0;
    //End

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

      //Eric's code start

      OW = ContentRepository.getMap(4, this);
      //OWlist = new List<OverWorld>();
      //OWlist.Add(OW);
      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;
      //currentLevel = 0;
      //printOnlyOnce = 0;
      TileMap tempTileMap = ContentRepository.getMap(3);
      OW.mapList.Add(tempTileMap);
      Entity temp = new Enemy(OW, OW.OWmap.getTile(5, 5), 0, new int[] { 0, 1, 2, 3 });
      temp.setAvatarFileString("EntityAvatar/RedTest/RedArrowUp", "EntityAvatar/RedTest/RedArrowRight",
          "EntityAvatar/RedTest/RedArrowDown", "EntityAvatar/RedTest/RedArrowLeft");
      OW.EntityList.Add(temp);
      temp = new Door(OW, OW.OWmap.getTile(1, 0), 1, tempTileMap, null);
      temp.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      Door tempSister = new Door(OW, tempTileMap.getTile(2, 7), 3, OW.OWmap, (Door)temp);
      tempSister.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp).sister = tempSister;
      OW.EntityList.Add(temp);
      tempTileMap.EntityList.Add(tempSister);
      //end
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

      //Eric's code start
      OW.LoadTileTextures(Content, "Textures/grass", "Textures/dirt", "Textures/ground",
            "Textures/mud", "Textures/road", "Textures/bricks");
      OW.LoadAvatar(Content, "Avatar/mFighterUp", "Avatar/mFighterRight", "Avatar/mFighterDown", "Avatar/mFighterLeft");

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
          player = characterCreation.CreatePlayer();
          currentGameState = 2;
        }
      }

      else if (currentGameState == 2)//Overworld
      {
        //Eric's code start
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
      }

      else if (currentGameState == 3)//Combat
      {

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
      action_timer++;
      if (action_timer >= 60)
      {
        action_timer = 0;
      }
      //End
      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

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
      else if (currentGameState == 2)
      {
        spriteBatch.End();
        OW.Draw(spriteBatch, graphics);

      }


      if (currentGameState != 2)
        spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}

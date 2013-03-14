using System;
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
  class CharacterCreation
  {
    SpriteBatch spriteBatch;
    ContentManager _content;
    Texture2D[] classes, races;
    Texture2D[] partyClasses, partyRaces;
    SpriteFont bigFont, className, classDescription;
    Texture2D stars, spaceship;
    Texture2D chosenClass, chosenRace;
    int cursorX, cursorY;
    int cursorLocation;
    int currentScreen;
    int whichClass, whichRace;
    int remainingStatPoints;
    KeyboardState prevKeyboardState, currentKeyboardState;

    public CharacterCreation() {

      classes = new Texture2D[3];
      races = new Texture2D[3];
      cursorX = 20;
      cursorY = 185;
      cursorLocation = 1;
      currentScreen = 1;
      whichClass = 0;//Currently no race has ben chosen
      remainingStatPoints = 10;
      prevKeyboardState = Keyboard.GetState();
      
    }

    public void LoadContent(ContentManager content)
    {
      classes[0] = content.Load<Texture2D>("PlayerSprites/mFighterDown");
      classes[1] = content.Load<Texture2D>("PlayerSprites/mWizardDown");
      classes[2] = content.Load<Texture2D>("PlayerSprites/mShooterDown");
      stars = content.Load<Texture2D>("MainMenu/stars");
      spaceship = content.Load<Texture2D>("MainMenu/spaceship");
      bigFont = content.Load<SpriteFont>("Fonts/MenuTitles");
      className = content.Load<SpriteFont>("Fonts/MenuTitles");

      _content = content;
    }

    private void LoadRaces(int whichClass)
    {
      if (whichClass == 1)//warrior
      {
        races[0] = _content.Load<Texture2D>("PlayerSprites/mFighterDown");
        races[1] = _content.Load<Texture2D>("PlayerSprites/mFighterDownGreen");
        races[2] = _content.Load<Texture2D>("PlayerSprites/mFighterDownBlue");

      }
      else if (whichClass == 2)//psychic
      {
        races[0] = _content.Load<Texture2D>("PlayerSprites/mWizardDown");
        races[1] = _content.Load<Texture2D>("PlayerSprites/mWizardDownGreen");
        races[2] = _content.Load<Texture2D>("PlayerSprites/mWizardDownBlue");
      }
      else //shooter
      {
        races[0] = _content.Load<Texture2D>("PlayerSprites/mShooterDown");
        races[1] = _content.Load<Texture2D>("PlayerSprites/mShooterDownGreen");
        races[2] = _content.Load<Texture2D>("PlayerSprites/mShooterDownBlue");
      }
    }

    public Player createPlayer() 
    {
      return null;
    }

    public void Update() 
    {
      //Console.WriteLine(cursorLocation +" currentScreen = " + currentScreen);
      
      if (currentScreen == 1 || currentScreen == 2)
        UpdateClassesOrRaces();
      else
        UpdateStats();

    }


    private void UpdateClassesOrRaces()
    {
      
      currentKeyboardState = Keyboard.GetState();

      if (prevKeyboardState.IsKeyUp(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter))
      {
        if (currentScreen == 1)
        {
          SelectClass();

        }
        else
          SelectRace();//Select Race

      }
      else if (prevKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Down))
      {
        if (cursorLocation != 3)
        {
          cursorY += 150;
          cursorLocation++;
        }

      }
      else if (prevKeyboardState.IsKeyUp(Keys.Up) && currentKeyboardState.IsKeyDown(Keys.Up))
      {
        if (cursorLocation != 1)
        {
          cursorY -= 150;
          cursorLocation--;
        }
      }
      prevKeyboardState = currentKeyboardState;
    
    }

    private void SelectClass()
    {
      //The cursor location is the same value as the class image in the array of classes
      //-1 because array index starts from 0, cursor location starts at 1
      chosenClass = classes[cursorLocation-1];
      currentScreen = 2;
      whichClass = cursorLocation;
      cursorLocation = 1;
      cursorX = 20;
      cursorY = 188;
      LoadRaces(whichClass);
    }

    private void SelectRace()
    {
      chosenRace = races[cursorLocation - 1];
      currentScreen = 3;
      whichRace = cursorLocation;
      cursorLocation = 1;
      cursorX = 20;
      cursorY = 188;
    }

    private void UpdateStats()
    {
    
    }


    public void Draw()
    {
      spriteBatch.Draw(stars, new Rectangle(0, 0, 800, 600), Color.White);
      spriteBatch.Draw(spaceship, new Vector2(cursorX, cursorY), null,
          Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

      spriteBatch.DrawString(bigFont, "Character Creation Step: "+currentScreen, Vector2.Zero, Color.White);
      if (currentScreen == 1)
        DrawClasses();

      else if (currentScreen == 2)
        DrawRaces();

      else if (currentScreen == 3)
      {
        spriteBatch.DrawString(bigFont, "Add stat stuff here...", new Vector2(0, 100), Color.White);
        DrawCenterSprite(chosenRace, 100, 100);//Prints correct image

      }
    }


    private void DrawClasses()
    {
      spriteBatch.DrawString(bigFont, "Select your class (Press enter to continue)", new Vector2(0, 50), Color.White);

      spriteBatch.DrawString(className, "Warrior", new Vector2(150, 200), Color.White);
      spriteBatch.DrawString(className, "Psychic", new Vector2(150, 350), Color.White);
      spriteBatch.DrawString(className, "Shooter", new Vector2(150, 500), Color.White);

      for (int i = 0; i < 3; i++)
      {
        DrawCenterSprite(classes[i], 300, i*150+200);
      }
    }

    private void DrawRaces()
    {
      spriteBatch.DrawString(bigFont, "Select your class (Press enter to continue)", new Vector2(0, 50), Color.White);

      spriteBatch.DrawString(className, "Human", new Vector2(150, 200), Color.White);
      spriteBatch.DrawString(className, "Alien", new Vector2(150, 350), Color.White);
      spriteBatch.DrawString(className, "Space Elf", new Vector2(150, 500), Color.White);

      for (int i = 0; i < 3; i++)
      {
        DrawCenterSprite(races[i], 300, i * 150 + 200);
      }

    }

    private void DrawCenterSprite(Texture2D charImage, int x, int y)
    {
      int width = charImage.Width / 3;
      int height = charImage.Height;
      int row = (int)((float)1 / (float)3);
      int column = 1 % 3;

      Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
      Rectangle destinationRectangle = new Rectangle((int)x, (int)y, width, height);

      spriteBatch.Draw(charImage, new Vector2(x,y), sourceRectangle,
            Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

    }



    public void setSpriteBatch(SpriteBatch sB)
    {
      spriteBatch=sB;
    }


  }
}

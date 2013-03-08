using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlackHoleLoans
{
  class MainMenu
  {
    private SpriteBatch spriteBatch;
    private ContentManager _content;
    private Texture2D spaceship, stars, BHLlogo;

    KeyboardState prevKeyboardState, currentKeyboardState;
    SpriteFont smallFont, bigFont;

    private int _height, _width, numOptions, lowestPossibleY;
    private int cursorX, cursorY;
    int volume, menuCursorLocation;
    int menuScreen;

    public MainMenu(ContentManager content, int width, int height)
    {
      _content = content;
      _width = width;
      _height = height;

      prevKeyboardState = Keyboard.GetState();
      currentKeyboardState = Keyboard.GetState();

      cursorX = 200;
      cursorY = 200;
      menuCursorLocation = 1;
      volume = 25;
      menuScreen = 1;
      numOptions = 4;
      lowestPossibleY = 425;
    }

    public void LoadContent()
    {
      spaceship = _content.Load<Texture2D>("MainMenu/spaceship");
      stars = _content.Load<Texture2D>("MainMenu/stars");
      BHLlogo = _content.Load<Texture2D>("MainMenu/BHL");
      smallFont = _content.Load<SpriteFont>("Fonts/MenuOptions");
      bigFont = _content.Load<SpriteFont>("Fonts/MenuTitles");
    }

    public int Update()
    {
      prevKeyboardState = currentKeyboardState;
      currentKeyboardState = Keyboard.GetState();

      if (menuScreen == 1)
      {
        numOptions = 4;
        lowestPossibleY = 425;
      }
      else
      {
        numOptions = 2;
        lowestPossibleY = 275;
        changeVolume();
      }

      return interactWithMainMenu();
    }

    public void Draw()
    {
      spriteBatch.Draw(stars, new Rectangle(0, 0,_width ,_height), Color.White);
      spriteBatch.Draw(BHLlogo, new Vector2(175, 50), Color.White);
      if (menuScreen == 1)
        drawMainMenu();
      else if (menuScreen == 2)
        drawOptions();
    }



    protected int interactWithMainMenu()
    {
      #region Entering a menu options

      if (prevKeyboardState.IsKeyUp(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter))
      {
        if ((menuCursorLocation == 1 || menuCursorLocation == 2) && menuScreen == 1)
          return menuCursorLocation;

        if (menuCursorLocation == 3)
        {
          menuCursorLocation = 1;
          menuScreen = 2;
          cursorY = 200;
        }

        if (menuScreen == 2 && menuCursorLocation == 2)
        {
          menuCursorLocation = 1;
          menuScreen = 1;
          cursorY = 200;
        }

        if (menuCursorLocation == 4)
          return -1;
      }

      #endregion

      #region Move cursor in the menus
      else if (prevKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Down))
      {
        if (menuCursorLocation != numOptions)
        {
          cursorY += 75;
          menuCursorLocation++;
        }
        else
        {
          cursorY = 200;
          menuCursorLocation = 1;
        }

      }
      else if (prevKeyboardState.IsKeyUp(Keys.Up) && currentKeyboardState.IsKeyDown(Keys.Up))
      {
        if (menuCursorLocation != 1)
        {
          cursorY -= 75;
          menuCursorLocation--;
        }
        else
        {
          cursorY = lowestPossibleY;
          menuCursorLocation = numOptions;
        }
      }
      #endregion

      return 0;

    }

    protected void drawMainMenu()
    {
      spriteBatch.DrawString(bigFont, "Main Menu", new Vector2(200, 150), Color.White);
      spriteBatch.DrawString(smallFont, "New Game", new Vector2(350, 220), Color.White);
      spriteBatch.DrawString(smallFont, "Load Game", new Vector2(350, 295), Color.White);
      spriteBatch.DrawString(smallFont, "Options", new Vector2(350, 370), Color.White);
      spriteBatch.DrawString(smallFont, "Exit", new Vector2(350, 445), Color.White);

      spriteBatch.Draw(spaceship, new Vector2(cursorX, cursorY), null,
                    Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

    }

    protected void drawOptions()
    {
      spriteBatch.DrawString(bigFont, "Options", new Vector2(200, 150), Color.White);
      spriteBatch.DrawString(smallFont, "Volume: " + volume, new Vector2(350, 220), Color.White);
      spriteBatch.DrawString(smallFont, "Back", new Vector2(350, 295), Color.White);

      spriteBatch.Draw(spaceship, new Vector2(cursorX, cursorY), null,
              Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

    }

    protected void changeVolume()
    {
      if (menuCursorLocation == 1)
      {

        if(prevKeyboardState.IsKeyUp(Keys.Left) && currentKeyboardState.IsKeyDown(Keys.Left))
        {
          if(volume > 0)
            volume -= 5;
        }

        if(prevKeyboardState.IsKeyUp(Keys.Right) && currentKeyboardState.IsKeyDown(Keys.Right))
        {
          if(volume <100)
            volume += 5;
        }

      }
    }

    public void setSpriteBatch(SpriteBatch sB)
    {
      spriteBatch = sB;
    }
  }
}

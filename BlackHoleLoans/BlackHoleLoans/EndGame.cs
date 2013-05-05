using System;
using System.Collections.Generic;
using System.Collections;
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
  class EndGame
  {
    bool finishedEndGame = false;
    KeyboardState keyState, prevKeyState;
    int whichEnding;
    SpriteBatch spriteBatch;
    ContentManager content;
    Texture2D redX, endSharkLose, stars, mineral, win, spaceship, sharkDesk;
    Texture2D[] playerSprites;
    SpriteFont font;

    public EndGame(int ending, SpriteBatch sB, ContentManager cM, Texture2D[] pS)
    {
      keyState = Keyboard.GetState();
      prevKeyState = keyState;
      whichEnding = ending;
      spriteBatch = sB;
      content = cM;
      playerSprites = pS; 
    }

    public void LoadContent()
    {
      redX = content.Load<Texture2D>("EndGame/REDX");
      font = content.Load<SpriteFont>("Fonts/MenuTitles");
      endSharkLose = content.Load<Texture2D>("EndGame/EndShark");
      stars = content.Load<Texture2D>("MainMenu/stars");
      mineral = content.Load<Texture2D>("Textures/minerals");
      win = content.Load<Texture2D>("EndGame/win-bhl");
      spaceship = content.Load<Texture2D>("MainMenu/spaceship");
      sharkDesk = content.Load<Texture2D>("MainMenu/Shark-desk2");
    }

    public void Update()
    {
      keyState = Keyboard.GetState();

      if (prevKeyState.IsKeyDown(Keys.Enter) && keyState.IsKeyUp(Keys.Enter))
      {
        finishedEndGame = true;
      }

      prevKeyState = keyState;
    }

    public void Draw()
    {

      if (whichEnding == 1)//Won the game
      {
        spriteBatch.Draw(stars, new Rectangle(0, 0, 800, 600), Color.White);
        spriteBatch.Draw(win, new Rectangle(0, 0, 800, 600), Color.White);

        spriteBatch.Draw(playerSprites[0], new Vector2(180, 130), Color.White);
        spriteBatch.Draw(playerSprites[1], new Vector2(200, 150), Color.White);
        spriteBatch.Draw(playerSprites[2], new Vector2(220, 170), Color.White);

        spriteBatch.Draw(spaceship, new Vector2(50, 100), null, Color.White, 0f, Vector2.Zero, .5f, SpriteEffects.None, 0f);
        spriteBatch.Draw(mineral, new Vector2(300, 150), null, Color.White, 0f, Vector2.Zero, .5f, SpriteEffects.None, 0f);
        spriteBatch.Draw(sharkDesk, new Vector2(400, 150), Color.White);
      }

      else if (whichEnding == 2)//Lost to boss (get eaten)
      {
        spriteBatch.Draw(stars, new Rectangle(0, 0, 800, 600), Color.White);
        spriteBatch.Draw(endSharkLose, new Rectangle(0, 0, 800, 600), Color.White);

        spriteBatch.Draw(playerSprites[0], new Vector2(180, 130), Color.White);
        spriteBatch.Draw(playerSprites[1], new Vector2(200, 150), Color.White);
        spriteBatch.Draw(playerSprites[2], new Vector2(220, 170), Color.White);
      }

      else if (whichEnding == 3)//Lost in combat (just dead)
      {

        spriteBatch.Draw(stars, new Rectangle(0, 0, 800, 600), Color.White);

        for (int i = 0; i < 3; i++)
        {
          spriteBatch.Draw(playerSprites[i], new Vector2(100+(i*200), 200), Color.White);
        }
        spriteBatch.Draw(redX, new Vector2(75, 175), Color.White);
        spriteBatch.Draw(redX, new Vector2(275, 175), Color.White);
        spriteBatch.Draw(redX, new Vector2(475, 175), Color.White);

        spriteBatch.DrawString(font, "You Lose!", new Vector2(300, 0), Color.White);
        spriteBatch.DrawString(font, "Press enter to go to the Main Menu", new Vector2(200, 50), Color.White);
      }

    }

    public bool LeaveEndGame()
    {
      return finishedEndGame;
    }
  }
}

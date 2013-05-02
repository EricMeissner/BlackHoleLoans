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
    Texture2D redX, endSharkLose, stars, mineral;
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
      endSharkLose = content.Load<Texture2D>("MainMenu/EndShark");
      stars = content.Load<Texture2D>("MainMenu/stars");
      mineral = content.Load<Texture2D>("Textures/minerals");
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
        spriteBatch.DrawString(font, "You Win!", new Vector2(200, 0), Color.White);
        spriteBatch.Draw(mineral, new Vector2(200,200), Color.White);
        spriteBatch.DrawString(font, "You can now pay back the loan woohooooo", new Vector2(300, 300), Color.White);
      }

      else if (whichEnding == 2)//Lost to boss (get eaten)
      {

      }

      else if (whichEnding == 3)//Lost in combat (just dead)
      {
        /*
        spriteBatch.Draw(redX, new Vector2(75,175), Color.White);
        spriteBatch.Draw(redX, new Vector2(275, 175), Color.White);
        spriteBatch.Draw(redX, new Vector2(475,175), Color.White);

        spriteBatch.Draw(stars, new Rectangle(0,0,800,600), Color.White);
        spriteBatch.Draw(endSharkLose, new Rectangle(0, 0, 800, 600), Color.White);
         * */
        /*
        spriteBatch.Draw(stars, new Rectangle(0, 0, 800, 600), Color.White);
        spriteBatch.Draw(endSharkLose, new Rectangle(0, 0, 800, 600), Color.White);

        spriteBatch.Draw(playerSprites[0], new Vector2(180, 130), Color.White);
        spriteBatch.Draw(playerSprites[1], new Vector2(200, 150), Color.White);
        spriteBatch.Draw(playerSprites[2], new Vector2(220, 170), Color.White);
        */

      }

    }

    public bool LeaveEndGame()
    {
      return finishedEndGame;
    }
  }
}

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
  class Overworld
  {
    int characterX, characterY;
    KeyboardState prevKeyboardState, currentKeyboardState;
    private ContentManager _content;
    Texture2D playerDown, playerUp, playerLeft, playerRight;
    AnimatedMovement down, up, left, right;
    SpriteBatch spriteBatch;
    bool isMoving = false;
    int direction;
    int screenHeight, screenWidth;

    public Overworld(ContentManager content, int sHeight, int sWidth) 
    {
      characterX = 100;
      characterY = 100;
      prevKeyboardState = Keyboard.GetState();
      currentKeyboardState = prevKeyboardState;
      _content = content;
      screenHeight = sHeight;
      screenWidth = sWidth;
      direction = 0;
      /*
       *0=down
       *1=right
       *2=up
       *3=left
       */
    }

    public void LoadContent()
    {
      playerDown = _content.Load<Texture2D>("PlayerSprites/mFighterDown");
      playerUp = _content.Load<Texture2D>("PlayerSprites/mFighterUp");
      playerLeft = _content.Load<Texture2D>("PlayerSprites/mFighterLeft");
      playerRight = _content.Load<Texture2D>("PlayerSprites/mFighterRight");

      down = new AnimatedMovement(playerDown, 1, 3);
      up = new AnimatedMovement(playerUp, 1, 3);
      left = new AnimatedMovement(playerLeft, 1, 3);
      right = new AnimatedMovement(playerRight, 1, 3);
    }

    public void Update()
    {

      characterMovement();


    }

    private void characterMovement()
    {
      currentKeyboardState = Keyboard.GetState();
      isMoving = true;

      if (currentKeyboardState.IsKeyDown(Keys.Down))
      {
        down.Update();
        direction = 0;
        if (characterY + (2 * playerDown.Height) <= screenHeight - 2)
          characterY += 2;
        else
          isMoving = false;

      }
      else if (currentKeyboardState.IsKeyDown(Keys.Right))
      {
        right.Update();
        direction = 1;
        if (characterX + 2*(playerDown.Width/3) <= screenWidth-2)
          characterX+=2;
        else
          isMoving = false;
      }
      else if (currentKeyboardState.IsKeyDown(Keys.Up))
      {
        up.Update();
        direction = 2;
        if (characterY >= 2)
          characterY -= 2;
        else
          isMoving = false;
      }
      else if (currentKeyboardState.IsKeyDown(Keys.Left))
      {
        left.Update();
        direction = 3;
        if(characterX>=2)
          characterX-=2;
        else
          isMoving = false;
      }
      else
        isMoving = false;

      prevKeyboardState = currentKeyboardState;//Might not need this
    
    }

    public void Draw()
    {

      drawCharacterMovement();
    
    }

    

    private void drawCharacterMovement()
    {

      if (direction == 0)
      {
        if (isMoving)
          down.Draw(spriteBatch, new Vector2(characterX, characterY));
        else
          down.DrawStill(spriteBatch, new Vector2(characterX, characterY));
      }

      if (direction == 1)
      {
        if(isMoving)
          right.Draw(spriteBatch, new Vector2(characterX, characterY));
        else
          right.DrawStill(spriteBatch, new Vector2(characterX, characterY));
      }
      if (direction == 2)
      {
        if (isMoving)
          up.Draw(spriteBatch, new Vector2(characterX, characterY));
        else
          up.DrawStill(spriteBatch, new Vector2(characterX, characterY));
      }
      if (direction == 3)
      {
        if (isMoving)
          left.Draw(spriteBatch, new Vector2(characterX, characterY));
        else
          left.DrawStill(spriteBatch, new Vector2(characterX, characterY));
      }
    }


    public void setSpriteBatch(SpriteBatch sB)
    {
      spriteBatch = sB;
    }



  }
}

using System;
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
  class CharacterCreation
  {
    #region Class fields

    SpriteBatch spriteBatch;
    ContentManager _content;
    Texture2D[] classes, races, playerSprites;
    SpriteFont bigFont, className, smallFont;
    Texture2D stars, spaceship, arrow, flippedArrow, rightArrow, leftArrow;
    Texture2D chosenClass, partyMember1, partyMember2;
    int cursorX, cursorY;
    int cursorLocation;
    int currentScreen;
    int whichClass, whichRace;
    int remainingStatPoints;
    int[] chosenStats, classIdentifier;
    string playerName;
    string[] letters;
    int[] letterIndex;
    KeyboardState prevKeyboardState, currentKeyboardState;
    const int SELECT_NAME = 0, SELECT_CLASS = 1, SELECT_RACE = 2, SELECT_STATS = 3, INFO_SCREEN=4;
    bool backToMM, startOverworld;
    String begin = "Press Enter ->"; //strings used for on - screen controls
    String back = "<- Press B";
    Texture2D shark, infoHeader;

    #endregion End Class fields

    public CharacterCreation()
    {

      classes = new Texture2D[3];
      races = new Texture2D[3];
      playerSprites = new Texture2D[4];
      cursorX = 20;
      cursorY = 185;
      cursorLocation = 1;
      currentScreen = 0;
      whichClass = 0;//Currently no race has ben chosen
      remainingStatPoints = 10;
      chosenStats = new int[3];
      classIdentifier = new int[3];
      prevKeyboardState = Keyboard.GetState();

      chosenStats[0] = chosenStats[1] = chosenStats[2] = 5;
      letterIndex = new int[5];
      letters = new string[27] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                                "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
                                  "U","V","W","X","Y","Z"," "};
      backToMM = startOverworld = false;
    }

    public void LoadContent(ContentManager content)
    {
      classes[0] = content.Load<Texture2D>("PlayerSprites/mFighterDown");
      classes[1] = content.Load<Texture2D>("PlayerSprites/mWizardDown");
      classes[2] = content.Load<Texture2D>("PlayerSprites/mShooterDown");
      stars = content.Load<Texture2D>("MainMenu/space");
      spaceship = content.Load<Texture2D>("MainMenu/spaceship");
      bigFont = content.Load<SpriteFont>("Fonts/MenuTitles");
      className = content.Load<SpriteFont>("Fonts/MenuTitles");
      arrow = content.Load<Texture2D>("Arrows/theArrow");
      flippedArrow = content.Load<Texture2D>("Arrows/theFlippedArrow");
      rightArrow = content.Load<Texture2D>("Arrows/theRightArrow");
      leftArrow = content.Load<Texture2D>("Arrows/theLeftArrow");
      smallFont = content.Load<SpriteFont>("Fonts/MenuOptions");
      infoHeader = content.Load<Texture2D>("MainMenu/Info");
      shark = content.Load<Texture2D>("MainMenu/Shark-desk");

      _content = content;
    }

    private void LoadRaces()
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

    private void SaveRace()
    {//human, green, blue
      classIdentifier[0] = whichClass;

      if (whichClass == 1)//warrior
      {
        classIdentifier[1] = 2;
        classIdentifier[2] = 3;
        if (whichRace == 1)
        {
          playerSprites[0] = _content.Load<Texture2D>("PlayerSprites/mFighterUp");
          playerSprites[1] = _content.Load<Texture2D>("PlayerSprites/mFighterRight");
          playerSprites[2] = _content.Load<Texture2D>("PlayerSprites/mFighterDown");
          playerSprites[3] = _content.Load<Texture2D>("PlayerSprites/mFighterLeft");
        }
        else if (whichRace == 2)
        {
          playerSprites[0] = _content.Load<Texture2D>("PlayerSprites/mFighterUp");
          playerSprites[1] = _content.Load<Texture2D>("PlayerSprites/mFighterRightGreen");
          playerSprites[2] = _content.Load<Texture2D>("PlayerSprites/mFighterDownGreen");
          playerSprites[3] = _content.Load<Texture2D>("PlayerSprites/mFighterLeftGreen");
        }

        else if (whichRace == 3)
        {
          playerSprites[0] = _content.Load<Texture2D>("PlayerSprites/mFighterUp");
          playerSprites[1] = _content.Load<Texture2D>("PlayerSprites/mFighterRightBlue");
          playerSprites[2] = _content.Load<Texture2D>("PlayerSprites/mFighterDownBlue");
          playerSprites[3] = _content.Load<Texture2D>("PlayerSprites/mFighterLeftBlue");
        }
      }
      else if (whichClass == 2)//psychic
      {
        classIdentifier[1] = 1;
        classIdentifier[2] = 3;
        if (whichRace == 1)
        {
          playerSprites[0] = _content.Load<Texture2D>("PlayerSprites/mWizardUp");
          playerSprites[1] = _content.Load<Texture2D>("PlayerSprites/mWizardRight");
          playerSprites[2] = _content.Load<Texture2D>("PlayerSprites/mWizardDown");
          playerSprites[3] = _content.Load<Texture2D>("PlayerSprites/mWizardLeft");
        }

        else if (whichRace == 2)
        {
          playerSprites[0] = _content.Load<Texture2D>("PlayerSprites/mWizardUp");
          playerSprites[1] = _content.Load<Texture2D>("PlayerSprites/mWizardRightGreen");
          playerSprites[2] = _content.Load<Texture2D>("PlayerSprites/mWizardDownGreen");
          playerSprites[3] = _content.Load<Texture2D>("PlayerSprites/mWizardLeftGreen");

        }

        else if (whichRace == 3)
        {
          playerSprites[0] = _content.Load<Texture2D>("PlayerSprites/mWizardUp");
          playerSprites[1] = _content.Load<Texture2D>("PlayerSprites/mWizardRightBlue");
          playerSprites[2] = _content.Load<Texture2D>("PlayerSprites/mWizardDownBlue");
          playerSprites[3] = _content.Load<Texture2D>("PlayerSprites/mWizardLeftBlue");
        }


      }
      else //shooter
      {
        classIdentifier[1] = 1;
        classIdentifier[2] = 2;
        if (whichRace == 1)
        {
          playerSprites[0] = _content.Load<Texture2D>("PlayerSprites/mShooterUp");
          playerSprites[1] = _content.Load<Texture2D>("PlayerSprites/mShooterRight");
          playerSprites[2] = _content.Load<Texture2D>("PlayerSprites/mShooterDown");
          playerSprites[3] = _content.Load<Texture2D>("PlayerSprites/mShooterLeft");
        }

        else if (whichRace == 2)
        {
          playerSprites[0] = _content.Load<Texture2D>("PlayerSprites/mShooterUp");
          playerSprites[1] = _content.Load<Texture2D>("PlayerSprites/mShooterRightGreen");
          playerSprites[2] = _content.Load<Texture2D>("PlayerSprites/mShooterDownGreen");
          playerSprites[3] = _content.Load<Texture2D>("PlayerSprites/mShooterLeftGreen");

        }

        else if (whichRace == 3)
        {
          playerSprites[0] = _content.Load<Texture2D>("PlayerSprites/mShooterUp");
          playerSprites[1] = _content.Load<Texture2D>("PlayerSprites/mShooterRightBlue");
          playerSprites[2] = _content.Load<Texture2D>("PlayerSprites/mShooterDownBlue");
          playerSprites[3] = _content.Load<Texture2D>("PlayerSprites/mShooterLeftBlue");
        }

      }
      SavePartyMembers();
    }

    private void SavePartyMembers()
    {

      if (whichClass == 1)//warrior
      {
        if (whichRace == 1)
        {
          partyMember1 = _content.Load<Texture2D>("PlayerSprites/mWizardRight");
          partyMember2 = _content.Load<Texture2D>("PlayerSprites/mShooterRight");
        }
        else if (whichRace == 2)
        {
          partyMember1 = _content.Load<Texture2D>("PlayerSprites/mWizardRightGreen");
          partyMember2 = _content.Load<Texture2D>("PlayerSprites/mShooterRightGreen");
        }

        else if (whichRace == 3)
        {
          partyMember1 = _content.Load<Texture2D>("PlayerSprites/mWizardRightBlue");
          partyMember2 = _content.Load<Texture2D>("PlayerSprites/mShooterRightBlue");
        }
      }
      else if (whichClass == 2)//psychic
      {
        if (whichRace == 1)
        {
          partyMember1 = _content.Load<Texture2D>("PlayerSprites/mFighterRight");
          partyMember2 = _content.Load<Texture2D>("PlayerSprites/mShooterRight");
        }

        else if (whichRace == 2)
        {
          partyMember1 = _content.Load<Texture2D>("PlayerSprites/mFighterRightGreen");
          partyMember2 = _content.Load<Texture2D>("PlayerSprites/mShooterRightGreen");

        }

        else if (whichRace == 3)
        {
          partyMember1 = _content.Load<Texture2D>("PlayerSprites/mFighterRightBlue");
          partyMember2 = _content.Load<Texture2D>("PlayerSprites/mShooterRightBlue");
        }


      }
      else //shooter
      {
        if (whichRace == 1)
        {
          partyMember1 = _content.Load<Texture2D>("PlayerSprites/mFighterRight");
          partyMember2 = _content.Load<Texture2D>("PlayerSprites/mWizardRight");
        }

        else if (whichRace == 2)
        {
          partyMember1 = _content.Load<Texture2D>("PlayerSprites/mFighterRightGreen");
          partyMember2 = _content.Load<Texture2D>("PlayerSprites/mWizardRightGreen");

        }

        else if (whichRace == 3)
        {
          partyMember1 = _content.Load<Texture2D>("PlayerSprites/mFighterRightBlue");
          partyMember2 = _content.Load<Texture2D>("PlayerSprites/mWizardRightBlue");
        }
      }
    }

    public void Update()
    {
      backToMM = false;
      currentKeyboardState = Keyboard.GetState();

      if (currentScreen == SELECT_NAME)
        UpdatePlayerName();//call select name screen

      else if (currentScreen == SELECT_CLASS || currentScreen == SELECT_RACE)
        UpdateClassesOrRaces();
      else if (currentScreen == INFO_SCREEN)
        UpdateInfoScreen();
      else
        UpdateStats();

      prevKeyboardState = currentKeyboardState;
    }

    private void UpdatePlayerName()//FIXFIXFIX
    {
      if (prevKeyboardState.IsKeyDown(Keys.B) && currentKeyboardState.IsKeyUp(Keys.B))
      {
        RequestBackToMM();
        letterIndex[0] = letterIndex[1] = letterIndex[2] = letterIndex[3] = letterIndex[4] = 0;
        chosenStats[0] = chosenStats[1] = chosenStats[2] = 5;
        remainingStatPoints = 10;
      }

      else if (prevKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyUp(Keys.Enter))
      {
        SelectPlayerName();
      }
      else if (prevKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Down))
      {
        if (letterIndex[cursorLocation - 1] == 26)
          letterIndex[cursorLocation - 1] = 0;
        else
          letterIndex[cursorLocation - 1]++;
      }

      else if (prevKeyboardState.IsKeyUp(Keys.Up) && currentKeyboardState.IsKeyDown(Keys.Up))
      {
        if (letterIndex[cursorLocation - 1] == 0)
          letterIndex[cursorLocation - 1] = 26;
        else
          letterIndex[cursorLocation - 1]--;
      }

      else if (prevKeyboardState.IsKeyUp(Keys.Right) && currentKeyboardState.IsKeyDown(Keys.Right))
      {
        if (cursorLocation != 5)
          cursorLocation++;
      }

      else if (prevKeyboardState.IsKeyUp(Keys.Left) && currentKeyboardState.IsKeyDown(Keys.Left))
      {
        if (cursorLocation != 1)
          cursorLocation--;
      }


    }

    private void UpdateClassesOrRaces()
    {
      if (prevKeyboardState.IsKeyDown(Keys.B) && currentKeyboardState.IsKeyUp(Keys.B))
      {
        cursorLocation = 1;
        cursorX = 20;
        cursorY = 188;
        if (currentScreen == 1)
        {
          currentScreen = 0;
        }
        else if (currentScreen == 2)
        {
          currentScreen = 1;
        }
        cursorLocation = 1;
      }

      else if (prevKeyboardState.IsKeyUp(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter))
      {
        if (currentScreen == 1)
        {
          SelectClass();
        }
        else
          SelectRace();

      }
      else if (prevKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Down))
      {
        if (cursorLocation != 3)
        {
          cursorY += 150;
          cursorLocation++;
        }
        else
        {
          cursorY -= 300;
          cursorLocation = 1;
        }
      }
      else if (prevKeyboardState.IsKeyUp(Keys.Up) && currentKeyboardState.IsKeyDown(Keys.Up))
      {
        if (cursorLocation != 1)
        {
          cursorY -= 150;
          cursorLocation--;
        }
        else
        {
          cursorY += 300;
          cursorLocation = 3;
        }
      }
    }

    private void UpdateStats()
    {
      if (prevKeyboardState.IsKeyDown(Keys.B) && currentKeyboardState.IsKeyUp(Keys.B))
      {
        currentScreen = 2;
        cursorLocation = 1;
        cursorX = 20;
        cursorY = 188;
        chosenStats[0] = chosenStats[1] = chosenStats[2] = 5;
        remainingStatPoints = 10;
      }

      else if (prevKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyUp(Keys.Enter))
      {
        if(remainingStatPoints==0)
          currentScreen = INFO_SCREEN;

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

      else if (prevKeyboardState.IsKeyUp(Keys.Right) && currentKeyboardState.IsKeyDown(Keys.Right))
      {
        if (remainingStatPoints != 0)
        {
          chosenStats[cursorLocation - 1]++;
          remainingStatPoints--;
        }
      }

      else if (prevKeyboardState.IsKeyUp(Keys.Left) && currentKeyboardState.IsKeyDown(Keys.Left))
      {
        if (chosenStats[cursorLocation - 1] != 5)
        {
          chosenStats[cursorLocation - 1]--;
          remainingStatPoints++;
        }
      }
    }

    private void UpdateInfoScreen()
    {
      if (prevKeyboardState.IsKeyDown(Keys.B) && currentKeyboardState.IsKeyUp(Keys.B))    // if "b" is pressed, it travels back to previous screen
      {
        currentScreen = SELECT_STATS;
      }
      if (prevKeyboardState.IsKeyUp(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter))    //if enter is pressed the boolean is set to true
      {
        startOverworld = true;
      }
    }

    private void SelectPlayerName()
    {
      string thePlayerName = letters[letterIndex[0]];//append all chars
      for (int i = 1; i < letterIndex.Length; i++)
      {
        thePlayerName += letters[letterIndex[i]];
      }
      playerName = thePlayerName.Trim();

      if (playerName == "")
        playerName = "Player";

      currentScreen = 1;
      cursorLocation = 1;
    }

    private void SelectClass()
    {
      //The cursor location is the same value as the class image in the array of classes
      //-1 because array index starts from 0, cursor location starts at 1
      chosenClass = classes[cursorLocation - 1];//need to determine other 3 pictures for this
      currentScreen = 2;
      whichClass = cursorLocation;
      cursorLocation = 1;
      cursorX = 20;
      cursorY = 188;
      LoadRaces();
    }

    private void SelectRace()
    {
      currentScreen = 3;
      whichRace = cursorLocation;
      cursorLocation = 1;
      cursorX = 20;//might not need cX and cY for Stats method
      cursorY = 188;
      SaveRace();
    }

    public void Draw()
    {
      spriteBatch.Draw(stars, new Rectangle(0, 0, 800, 600), Color.White);
      spriteBatch.DrawString(smallFont, begin, new Vector2(625, 560), Color.Aqua); // displayes the on - screen controls
      spriteBatch.DrawString(smallFont, back, new Vector2(0, 560), Color.Aqua);
      if (currentScreen != 3 && currentScreen != 0 && currentScreen != 4)
        spriteBatch.Draw(spaceship, new Vector2(cursorX, cursorY), null,
           Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
      if(currentScreen != 4)
        spriteBatch.DrawString(bigFont, "Character Creation Step: " + (currentScreen + 1), Vector2.Zero, Color.White);
      if (currentScreen == SELECT_NAME)
        DrawNameSelection();

      else if (currentScreen == SELECT_CLASS)
        DrawClasses();

      else if (currentScreen == SELECT_RACE)
        DrawRaces();

      else if (currentScreen == SELECT_STATS)
        DrawCurrentStats();

      else
        DrawInfoScreen();
    }

    private void DrawNameSelection()
    {
      Color[] letterColors = { Color.White, Color.White, Color.White, Color.White, Color.White, Color.White };
      letterColors[cursorLocation - 1] = Color.Red;

      spriteBatch.DrawString(bigFont, "Select your name (Press enter to continue)", new Vector2(0, 50), Color.White);

      spriteBatch.DrawString(bigFont, "Your Name: ", new Vector2(0, 150), Color.White);

      for (int i = 0; i < letterIndex.Length; i++)
      {
        spriteBatch.DrawString(bigFont, "" + letters[letterIndex[i]], new Vector2(200 + 30 * (i), 150), letterColors[i]);
      }
      spriteBatch.Draw(arrow, new Vector2((210 + cursorLocation * 30) - arrow.Width, 150 - arrow.Height), Color.White);
      spriteBatch.Draw(flippedArrow, new Vector2((210 + cursorLocation * 30) - arrow.Width, 270 - arrow.Height), Color.White);
    }

    private void DrawClasses()
    {
      spriteBatch.DrawString(bigFont, "Select your class (Press enter to continue)", new Vector2(0, 50), Color.White);

      spriteBatch.DrawString(className, "Warrior", new Vector2(150, 200), Color.White);
      spriteBatch.DrawString(className, "Psionic", new Vector2(150, 350), Color.White);
      spriteBatch.DrawString(className, "Shooter", new Vector2(150, 500), Color.White);

      for (int i = 0; i < 3; i++)
      {
        DrawCenterSprite(classes[i], 300, i * 150 + 200);
      }
    }

    private void DrawRaces()
    {
      spriteBatch.DrawString(bigFont, "Select your race (Press enter to continue)", new Vector2(0, 50), Color.White);

      spriteBatch.DrawString(className, "Human", new Vector2(150, 200), Color.White);
      spriteBatch.DrawString(className, "Alien", new Vector2(150, 350), Color.White);
      spriteBatch.DrawString(className, "Space Elf", new Vector2(150, 500), Color.White);

      for (int i = 0; i < 3; i++)
      {
        DrawCenterSprite(races[i], 300, i * 150 + 200);
      }

    }

    private void DrawCurrentStats()
    {

      Color[] statColors = { Color.White, Color.White, Color.White };
      statColors[cursorLocation - 1] = Color.Red;

      spriteBatch.DrawString(bigFont, "Player Statistics (Press Enter to Continue)", new Vector2(0, 50), Color.White);
      spriteBatch.DrawString(bigFont, "Your Character -> ", new Vector2(50, 100), Color.White);
      DrawCenterSprite(playerSprites[2], 350, 90);//Prints correct image

      spriteBatch.DrawString(bigFont, "Your Name: " + playerName, new Vector2(50, 150), Color.White);

      spriteBatch.DrawString(bigFont, "Remaining Stat Points (Must use ALL!): " + remainingStatPoints,
        new Vector2(50, 200), Color.White);

      spriteBatch.DrawString(bigFont, "Attack:", new Vector2(50, 300), Color.White);
      spriteBatch.DrawString(bigFont, "Defense:", new Vector2(50, 400), Color.White);
      spriteBatch.DrawString(bigFont, "Concentration:", new Vector2(50, 500), Color.White);

      spriteBatch.DrawString(bigFont, "" + chosenStats[0], new Vector2(350, 300), statColors[0]);
      spriteBatch.Draw(leftArrow,
        new Vector2(350 - leftArrow.Width, 240 + cursorLocation * 100 - leftArrow.Height),
        Color.White);

      spriteBatch.DrawString(bigFont, "" + chosenStats[1], new Vector2(350, 400), statColors[1]);
      spriteBatch.Draw(rightArrow,
        new Vector2(450 - leftArrow.Width, 240 + cursorLocation * 100 - leftArrow.Height),
        Color.White);

      spriteBatch.DrawString(bigFont, "" + chosenStats[2], new Vector2(350, 500), statColors[2]);
    }

    public void DrawInfoScreen()
    {

      String infoText = " Hello " + playerName + ",\n   You have embarked on a new journey to pay \n"+  
      "    back a loan you borrowed from a loan shark\n    at Black Hole Loans. The loan was used "+
      "\n    to buy a spaceship. You must defeat \n    enemies and reach the end in order to\n    "+ 
        "collect enough money to pay off the loan.\n     Failure to pay off the loan will"+
        "\n    lead to being eaten by the shark.";
     
      
      spriteBatch.DrawString(smallFont, infoText, new Vector2(0, 145), Color.White); //displays the information message
      spriteBatch.Draw(infoHeader, new Vector2(150, 10), null,                    //displays a black hole loans banner
          Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
      spriteBatch.Draw(shark, new Vector2(300, 490), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);    // displays the shark sprite
      spriteBatch.Draw(playerSprites[3], new Vector2(410, 540), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f); //displays the player sprite
    }

    private void DrawCenterSprite(Texture2D charImage, int x, int y)
    {
      int width = charImage.Width;
      int height = charImage.Height;
      int row = (int)((float)1 / (float)3);
      int column = 0;

      Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
      Rectangle destinationRectangle = new Rectangle((int)x, (int)y, width, height);

      spriteBatch.Draw(charImage, new Vector2(x, y), sourceRectangle,
            Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

    }

    public void setSpriteBatch(SpriteBatch sB)
    {
      spriteBatch = sB;
    }

    public Player[] CreatePlayer()
    {
      int[] partyMemStats = chosenStats;
      partyMemStats[0] -= 2;
      partyMemStats[1] -= 2;
      partyMemStats[2] -= 2;
      Texture2D[] pMember1 = new Texture2D[4] { partyMember1, partyMember1, partyMember1, partyMember1 };
      Texture2D[] pMember2 = new Texture2D[4] { partyMember2, partyMember2, partyMember2, partyMember2 };

      Player[] thePlayerList = new Player[3]{
        new Player(chosenStats[0],chosenStats[1],chosenStats[2], playerSprites, classIdentifier[0], 
          playerName, new Skill(Skills.Blast), new Skill(Skills.LaserSword)),

        new Player(partyMemStats, pMember1, classIdentifier[1],
          new Skill(Skills.Blast), new Skill(Skills.Leech)),

        new Player(partyMemStats, pMember2, classIdentifier[2], new Skill(Skills.Blast),
                new Skill(Skills.BloodShot))
      };
      return thePlayerList;
    }

    private void RequestBackToMM()
    {
      backToMM = true;
    }

    public int BackToMM()
    {
      if (backToMM)
        return 0;
      else
        return 1;
    }

    public bool StartOverworld()
    {
      return startOverworld;
    }

  }
}
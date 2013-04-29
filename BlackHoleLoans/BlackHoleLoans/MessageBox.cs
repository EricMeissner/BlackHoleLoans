using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BlackHoleLoans
{
    public class MessageBox
    {
        //static SpriteBatch spriteBatch;
        static private Texture2D messagebase;
        static private SpriteFont combatfontsmall;
        static private ContentManager _content;
        static int _width, _height;

        public MessageBox(ContentManager newcontent, int height, int width)
        {
            _content = newcontent;
            _height = height;
            _width = width;
        }
        public void LoadContent()
        {
            messagebase = _content.Load<Texture2D>("Combat/messagebase");
            combatfontsmall = _content.Load<SpriteFont>("Combat/combatfontsmall");
        }

        public void DrawMessage(String message, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(messagebase, new Rectangle(0, 0, _width, messagebase.Height * 3), Color.White);
            Vector2 textCenter = combatfontsmall.MeasureString(message) * .5f;
            spriteBatch.DrawString(combatfontsmall, message, new Vector2(_width / 2 - textCenter.X, messagebase.Height / 2 - textCenter.Y), Color.White);
        }
    }
}

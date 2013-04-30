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
    public class NPCMenu
    {
        static private Texture2D messagebase;
        static private SpriteFont combatfontsmall;
        static private ContentManager _content;
        static int _width, _height;
        public NPC _asker;

        public NPCMenu(ContentManager newcontent, int height, int width, NPC asker)
        {
            _content = newcontent;
            _height = height;
            _width = width;
            _asker = asker;
        }
        public NPCMenu(ContentManager newcontent, int height, int width)
        {
            _content = newcontent;
            _height = height;
            _width = width;
            _asker = null;
        }
        public void LoadContent()
        {
            messagebase = _content.Load<Texture2D>("Combat/messagebase");
            combatfontsmall = _content.Load<SpriteFont>("Combat/combatfontsmall");
        }

        public void DrawMessage(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(messagebase, new Rectangle(0, 0, _width, 3*messagebase.Height), Color.White);
            Vector2 textCenter = combatfontsmall.MeasureString(_asker.message) * .5f;
            spriteBatch.DrawString(combatfontsmall, _asker.message, new Vector2(_width / 2 - textCenter.X, messagebase.Height / 2 - textCenter.Y/3), Color.White);
        }
        public void DrawMessage(NPC asker, SpriteBatch spriteBatch)
        {
            _asker = asker;
            spriteBatch.Draw(messagebase, new Rectangle(0, 0, _width, 3*messagebase.Height), Color.White);
            Vector2 textCenter = combatfontsmall.MeasureString(_asker.message) * .5f;
            spriteBatch.DrawString(combatfontsmall, _asker.message, new Vector2(_width / 2 - textCenter.X, messagebase.Height / 2 - textCenter.Y/3), Color.White);
        }
        public bool Answer(KeyboardState input, KeyboardState prev)
        {
            if (input != prev)
            {
                return _asker.Answer(input);
            }
            else
                return false;
        }
    }
}

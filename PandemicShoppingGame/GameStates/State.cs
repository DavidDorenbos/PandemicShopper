using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PandemicShoppingGame.GameStates
{
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected BaseGame _game;

        #endregion

        #region Methods

        public State(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;
        }

        public abstract void Initialize();

        public abstract void LoadContent();

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public int NextLevel()
        {
            String XmlScoreFile = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Scores/score.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlScoreFile);
            for (int i = 1; i < 100; i++)
            {
                String levelDoc = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Levels/" + i + ".xml";

                if (File.Exists(levelDoc))
                {
                    if (!doc.GetElementsByTagName("Level" + i)[0].HasChildNodes)
                    {
                        return i;
                    }
                }
                else
                {
                    return i - 1;
                }
            }
            return 1;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandemicShoppingGame.GameParts;
using PandemicShoppingGame.Scores;
using PandemicShoppingGame.Level;
using Microsoft.Xna.Framework.Input;

namespace PandemicShoppingGame.GameStates
{
    public class GameState : State
    {
        public SpriteFont font;
        public Texture2D background;

        private int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        private LevelManager levelManager;
        private ScoreManager scoreManager;

        private Stopwatch stopwatch = new Stopwatch();
        private long time;

        private int level = 1;

        public GameState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            font  = _content.Load<SpriteFont>("Fonts/Standard");
            background = _content.Load<Texture2D>("tile");

            levelManager = new LevelManager();
            levelManager.LoadLevel(level);

        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            //Temp keybind to get out of the game while it doesn't work
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw Background
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap);
            spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), new Rectangle(0, 0, background.Width * 30, background.Height * 20), Color.White);
            spriteBatch.End();
        }
    }
}

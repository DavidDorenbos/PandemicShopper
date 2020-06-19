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
using PandemicShoppingGame.Level;
using PandemicShoppingGame.Scores;
using Microsoft.Xna.Framework.Input;

namespace PandemicShoppingGame.GameStates
{
    public class GameState : State
    {
        public SpriteFont font;
        public Texture2D background;

        public Texture2D shopListTexture;
        public Texture2D bagTexture;

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

            shopListTexture = _content.Load<Texture2D>("shoplist");
            bagTexture = _content.Load<Texture2D>("bag");

            levelManager = new LevelManager(_content);
            levelManager.LoadLevel(level);

            stopwatch.Start();
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
            //Update time value
            time = stopwatch.ElapsedMilliseconds / 1000;

            //Temp keybind to get out of the game while it doesn't work
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
            }

            levelManager.player.Move();
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

            //Draw level contents
            spriteBatch.Begin();

            //Draw game info
            spriteBatch.DrawString(font, "Health: " + levelManager.player.health + " + " + levelManager.player.armor, new Vector2(20, 20), Color.Black);
            spriteBatch.DrawString(font, "Time: " + time, new Vector2(300, 20), Color.Black);
            spriteBatch.DrawString(font, "Level " + level, new Vector2(screenWidth / 2, 20), Color.Black);

            //Draw player and enemies
            spriteBatch.Draw(levelManager.player.texture, levelManager.player.position, null, Color.White, levelManager.player.angle, levelManager.player.origin, 1, SpriteEffects.None, 0f);
            for (int i = 0; i < levelManager.enemies.Count; i++)
            {
                spriteBatch.Draw(levelManager.enemies[i].texture, levelManager.enemies[i].position, Color.White);
            }

            //Draw shelves
            for (int i = 0; i < levelManager.shelves.Count; i++)
            {
                spriteBatch.Draw(levelManager.shelves[i].texture, levelManager.shelves[i].position, Color.White);
            }

            //Draw shoppinglist
            spriteBatch.Draw(shopListTexture, new Rectangle(20, 60, 40, 40), Color.White);

            //Draw inventory
            spriteBatch.Draw(bagTexture, new Rectangle(20, 120, 40, 40), Color.White);

            spriteBatch.End();
        }
    }
}

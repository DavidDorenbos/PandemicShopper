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
using Microsoft.Xna.Framework.Media;

namespace PandemicShoppingGame.GameStates
{
    public class GameState : State
    {
        //TODO:
        //Keep track of levels
        public SpriteFont font;
        public Texture2D background;

        public Texture2D shopListTexture;
        public Texture2D bagTexture;

        private int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        private LevelManager levelManager;
        private ScoreManager scoreManager;

        private KeyboardState newState;
        private KeyboardState oldState;

        private Stopwatch stopwatch = new Stopwatch();
        private long time;

        private int level;

        private int healthHelper = 0;

        public GameState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content, int level)
          : base(game, graphicsDevice, content)
        {
            font  = _content.Load<SpriteFont>("Fonts/Standard");
            background = _content.Load<Texture2D>("tile");

            this.level = level;

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

            DetectShelfColision();
            PickUpProducts();
            UpdatePlayerHealth();
            CheckPlayerDied();
            DetectLevelFinished();
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

            //Draw cashier
            spriteBatch.Draw(levelManager.cashier.texture, levelManager.cashier.position, Color.White);

            //Draw shelves
            for (int i = 0; i < levelManager.shelves.Count; i++)
            {
                spriteBatch.Draw(levelManager.shelves[i].texture, levelManager.shelves[i].position, Color.White);
            }

            //Draw worlditems
            for (int i = 0; i < levelManager.productList.Count; i++)
            {
                spriteBatch.Draw(levelManager.productList[i].texture, levelManager.productList[i].position, Color.White);
            }

            //Draw shoppinglist
            spriteBatch.Draw(shopListTexture, new Rectangle(20, 60, 40, 40), Color.White);

            //Draw shoppinglist items
            int xPositionShoplistItems = 70;
            for (int i = 0; i < levelManager.shopList.Count; i++)
            {
                levelManager.shopList[i].position.X = xPositionShoplistItems;
                spriteBatch.Draw(levelManager.shopList[i].texture, levelManager.shopList[i].position, Color.White);
                //Alignment Shoppinglist items
                xPositionShoplistItems += 20 + levelManager.shopList[i].texture.Width;
            }

            //Draw inventory
            spriteBatch.Draw(bagTexture, new Rectangle(20, 120, 40, 40), Color.White);

            //Draw inventoryproducts
            int xPositionInventory = 80;
            for (int i = 0; i < levelManager.player.inventory.Count; i++)
            {
                levelManager.player.inventory[i].position.X = xPositionInventory;
                levelManager.player.inventory[i].position.Y = 130;
                spriteBatch.Draw(levelManager.player.inventory[i].texture, levelManager.player.inventory[i].position, Color.White);
                //Alignment Shoppinglist items
                xPositionInventory += 20 + levelManager.player.inventory[i].texture.Width;
            }

            //Draw player and enemies
            for (int i = 0; i < levelManager.enemies.Count; i++)
            {
                spriteBatch.Draw(levelManager.enemies[i].texture, levelManager.enemies[i].position, Color.White);
            }
            spriteBatch.Draw(levelManager.player.texture, levelManager.player.position, null, Color.White, levelManager.player.angle, levelManager.player.origin, 1, SpriteEffects.None, 0f);

            spriteBatch.End();
        }

        private void DetectShelfColision()
        {
            //check if player walking into a shelve, if so stop the movement and place them player away from the object and give back the movement

            foreach (LevelObject obj in levelManager.shelves)
            {
                if (obj.IsTouchingLeft(levelManager.player))
                {
                    levelManager.player.speed = 0;
                    levelManager.player.position.X -= 3;
                    levelManager.player.speed = 2;
                }
                if (obj.IsTouchingRight(levelManager.player))
                {

                    levelManager.player.speed = 0;
                    levelManager.player.position.X += 3;
                    levelManager.player.speed = 2;
                }
                if (obj.IsTouchingTop(levelManager.player))
                {
                    levelManager.player.speed = 0;
                    levelManager.player.position.Y -= 3;
                    levelManager.player.speed = 2;
                }
                if (obj.IsTouchingBottom(levelManager.player))
                {
                    levelManager.player.speed = 0;
                    levelManager.player.position.Y += 3;
                    levelManager.player.speed = 2;
                }
            }
        }

        private void PickUpProducts()
        {
            Product delete = null;
            //Pick Up items off the ground
            newState = Keyboard.GetState();
            if (newState.IsKeyUp(Keys.E) && oldState.IsKeyDown(Keys.E))
            {
                foreach (Product p in levelManager.productList)
                {
                    if (p.isClose(levelManager.player) && p.name == "mask")
                    {
                        levelManager.player.armor += 25;
                        delete = p;
                        levelManager.player.slurp.Play();
                    }
                    else if (p.isClose(levelManager.player) && !levelManager.player.inventory.Contains(p))
                    {
                        levelManager.player.inventory.Add(p);
                        levelManager.player.slurp.Play();
                    }
                }
            }
            oldState = newState;

            //if mask is picked up remove it from the game
            if (delete != null)
            {
                levelManager.productList.Remove(delete);
            }
        }

        private void UpdatePlayerHealth()
        {
            //Check if player is close to enemy
            foreach (Enemy e in levelManager.enemies)
            {
                if (e.isClose(levelManager.player))
                {
                    if (healthHelper == 4 && levelManager.player.armor > 0)
                    {
                        levelManager.player.armor--;
                        healthHelper = 0;
                        levelManager.player.scream.Play();
                    }
                    else if (healthHelper == 4 && levelManager.player.armor == 0)
                    {
                        levelManager.player.health--;
                        healthHelper = 0;
                        levelManager.player.scream.Play();
                    }
                    else
                    {
                        healthHelper++;
                    }
                }
            }
        }

        private void CheckPlayerDied()
        {
            if(levelManager.player.health == 0)
            {
                _game.ChangeState(new GameLostState(_game, _graphicsDevice, _content, level));
            }
        }

        private void DetectLevelFinished()
        {
            if (levelManager.cashier.IsTouchingLeft(levelManager.player) || levelManager.cashier.IsTouchingTop(levelManager.player) || levelManager.cashier.IsTouchingRight(levelManager.player) || levelManager.cashier.IsTouchingBottom(levelManager.player))
            {
                stopwatch.Stop();
                levelManager.player.speed = 0;
                int timeScore = unchecked((int)time);

                scoreManager = new ScoreManager(level, levelManager.player.health, timeScore, levelManager.player.inventory, levelManager.shopList);

                scoreManager.CalculateScore();
                scoreManager.SaveScore(level);

                _game.ChangeState(new EndGameState(_game, _graphicsDevice, _content, scoreManager.GetScore()));
            }
        }
    }
}

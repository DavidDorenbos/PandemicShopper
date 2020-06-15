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

namespace PandemicShoppingGame.GameStates
{
    public class GameState : State
    {
        public SpriteFont font;

        public Player player;
        public LevelObject cashier;

        public Texture2D textureShopList;
        public Texture2D textureBag;
        public Texture2D background;

        public Texture2D textureShelf;
        public Texture2D textureShelfVertical;

        public Texture2D textureCashier;
        public Texture2D textureEnemy;
        public Texture2D texture;
        private Texture2D maskTexture;

        private Texture2D singMilkTexture;
        private Texture2D singBreadTexture;
        private Texture2D singKetchTexture;
        private Texture2D singOliveTexture;

        private int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public List<LevelObject> objectList = new List<LevelObject>();
        public List<Product> shoppingList = new List<Product>();
        public List<Product> productList = new List<Product>();
        public List<Enemy> enemies = new List<Enemy>();

        public int level;
        private int score;

        private ScoreManager scoreManager;
        public Stopwatch stopwatch = new Stopwatch();
                
        public long time;

        private bool noNewLevel = false;

        public GameState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content, int level)
          : base(game, graphicsDevice, content)
        {
            font  = _content.Load<SpriteFont>("Fonts/Standard");

            this.level = level;

            //Initialize all used variables
            textureBag = _content.Load<Texture2D>("bag");
            textureShopList = _content.Load<Texture2D>("shoplist");

            background = _content.Load<Texture2D>("tile");

            textureShelf = _content.Load<Texture2D>("shelf");
            textureShelfVertical = _content.Load<Texture2D>("shelfVertical");

            textureCashier = _content.Load<Texture2D>("cashier");
            textureEnemy = _content.Load<Texture2D>("enemy");
            texture = _content.Load<Texture2D>("player1");
            maskTexture = _content.Load<Texture2D>("mask");

            singMilkTexture = _content.Load<Texture2D>("singmilk");
            singBreadTexture = _content.Load<Texture2D>("singBread");
            singKetchTexture = _content.Load<Texture2D>("singKetch");
            singOliveTexture = _content.Load<Texture2D>("singOlive");

            // TODO: Add your initialization logic here
            //Load Textures
            XmlDocument xDoc = new XmlDocument();
            String levelDoc = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Levels/" + level + ".xml";

            if (File.Exists(levelDoc))
            {
                xDoc.Load(levelDoc);

                //Load Shelves
                XmlNodeList verticalShelves = xDoc.GetElementsByTagName("VerticalShelve");
                for (int i = 0; i < verticalShelves.Count; i++)
                {
                    LevelObject obj = new LevelObject(Int32.Parse(verticalShelves[i].FirstChild.InnerText), Int32.Parse(verticalShelves[i].LastChild.InnerText), textureShelfVertical);
                    objectList.Add(obj);
                }
                XmlNodeList horizontalShelves = xDoc.GetElementsByTagName("HorizontalShelve");
                for (int i = 0; i < horizontalShelves.Count; i++)
                {
                    LevelObject obj = new LevelObject(Int32.Parse(horizontalShelves[i].FirstChild.InnerText), Int32.Parse(horizontalShelves[i].LastChild.InnerText), textureShelf);
                    objectList.Add(obj);
                }

                //Init cashier
                XmlNodeList cashierEl = xDoc.GetElementsByTagName("Cashier");
                cashier = new LevelObject(Int32.Parse(cashierEl[0].FirstChild.InnerText), Int32.Parse(cashierEl[0].LastChild.InnerText), textureCashier);

                //Init Player
                XmlNodeList playerEl = xDoc.GetElementsByTagName("Player");
                player = new Player(Int32.Parse(playerEl[0].FirstChild.InnerText), Int32.Parse(playerEl[0].LastChild.InnerText), texture, _content);

                //Init products in world
                XmlNodeList productsinWorld = xDoc.GetElementsByTagName("WorldProduct");
                for (int i = 0; i < productsinWorld.Count; i++)
                {
                    Product prod = new Product(productsinWorld[i].FirstChild.InnerText, Int32.Parse(productsinWorld[i].ChildNodes[1].InnerText), Int32.Parse(productsinWorld[i].ChildNodes[2].InnerText), null);
                    if (productsinWorld[i].FirstChild.InnerText == "singMilk")
                    {
                        prod.texture = singMilkTexture;
                    }
                    else if (productsinWorld[i].FirstChild.InnerText == "singBread")
                    {
                        prod.texture = singBreadTexture;
                    }
                    else if (productsinWorld[i].FirstChild.InnerText == "singKetch")
                    {
                        prod.texture = singKetchTexture;
                    }
                    else if (productsinWorld[i].FirstChild.InnerText == "SingOlive")
                    {
                        prod.texture = singOliveTexture;
                    }
                    else if (productsinWorld[i].FirstChild.InnerText == "mask")
                    {
                        prod.texture = maskTexture;
                    }
                    productList.Add(prod);
                }

                //Init products in Shoppinglist
                XmlNodeList productsShoplist = xDoc.GetElementsByTagName("ShoppingListProduct");
                for (int i = 0; i < productsShoplist.Count; i++)
                {
                    Product prod = new Product(productsShoplist[i].FirstChild.InnerText, 0, 70, null);
                    if (productsShoplist[i].FirstChild.InnerText == "singMilk")
                    {
                        prod.texture = singMilkTexture;
                    }
                    else if (productsShoplist[i].FirstChild.InnerText == "singBread")
                    {
                        prod.texture = singBreadTexture;
                    }
                    else if (productsShoplist[i].FirstChild.InnerText == "singKetch")
                    {
                        prod.texture = singKetchTexture;
                    }
                    else if (productsShoplist[i].FirstChild.InnerText == "SingOlive")
                    {
                        prod.texture = singOliveTexture;
                    }
                    shoppingList.Add(prod);
                }

                //Init enemies
                XmlNodeList enemiesEl = xDoc.GetElementsByTagName("Enemy");
                for (int i = 0; i < enemiesEl.Count; i++)
                {
                    Enemy en = new Enemy(Int32.Parse(enemiesEl[i].FirstChild.InnerText), Int32.Parse(enemiesEl[i].LastChild.InnerText), textureEnemy);
                    enemies.Add(en);
                }
                stopwatch.Start();
            }
            else
            {
                noNewLevel = true;
            }
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
            if (noNewLevel == true)
            {
                _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content, level));
            }
            else
            {
                player.Update(gameTime, objectList, productList, enemies);
                time = stopwatch.ElapsedMilliseconds / 1000;

                int timeScore =  unchecked((int)time);

                scoreManager = new ScoreManager(level, player.health, timeScore, player.inventory, shoppingList);

                //Game Won
                if (cashier.IsTouchingLeft(player) || cashier.IsTouchingTop(player) || cashier.IsTouchingRight(player) || cashier.IsTouchingBottom(player))
                {
                    stopwatch.Stop();
                    player.Speed = 0;
                    List<string> shoplist = new List<string>();
                    List<string> inventory = new List<string>();

                    foreach (Product prod in player.inventory)
                    {
                        inventory.Add(prod.name);
                    }
                    foreach (Product prod in shoppingList)
                    {
                        shoplist.Add(prod.name);
                    }

                    scoreManager.CalculateScore();

                    score = scoreManager.GetScore();

                    level++;
                    _game.ChangeState(new EndGameState(_game, _graphicsDevice, _content, level, score));
                }

                //Game Lost
                if (player.health == 0)
                {
                    _game.ChangeState(new GameLostState(_game, _graphicsDevice, _content, level));
                }
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

            if (noNewLevel == false)
            {
                spriteBatch.Begin();

                //Draw Health
                spriteBatch.DrawString(font, "Health: " + player.health + " + " + player.armor, new Vector2(20, 20), Color.Black);
                player.Draw(spriteBatch);

                //Draw Score
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(200, 20), Color.Black);
                player.Draw(spriteBatch);


                //Draw Level
                spriteBatch.DrawString(font, "Level " + level, new Vector2(screenWidth / 2, 20), Color.Black);
                player.Draw(spriteBatch);

                //Draw Level
                spriteBatch.DrawString(font, "Time: " + time, new Vector2(300, 20), Color.Black);
                player.Draw(spriteBatch);

                //Draw Cashier
                cashier.Draw(spriteBatch);

                //Draw ShoppingList
                spriteBatch.Draw(textureShopList, new Rectangle(20, 60, 40, 40), Color.White);
                int xPositionShoplistItems = 70;
                for (int i = 0; i < shoppingList.Count; i++)
                {
                    shoppingList[i].Position.X = xPositionShoplistItems;
                    shoppingList[i].Draw(spriteBatch);
                    //Alignment Shoppinglist items
                    xPositionShoplistItems += 20 + shoppingList[i].texture.Width;
                }

                //Draw Inventory
                spriteBatch.Draw(textureBag, new Rectangle(20, 120, 40, 40), Color.White);
                int xPositionInventoryItems = 70;
                for (int i = 0; i < player.inventory.Count; i++)
                {
                    player.inventory[i].Position.X = xPositionInventoryItems;
                    player.inventory[i].Position.Y = 130;
                    player.inventory[i].Draw(spriteBatch);
                    //Alignment Inventory items
                    xPositionInventoryItems += 20 + player.inventory[i].texture.Width;
                }

                //Draw Shelves
                for (int i = 0; i < objectList.Count; i++)
                {
                    objectList[i].Draw(spriteBatch);
                }

                //Draw Enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw(spriteBatch);
                }

                //Draw Products In world
                foreach (Product prod in productList)
                {
                    prod.Draw(spriteBatch);
                }
                spriteBatch.End();
            }
        }
    }
}

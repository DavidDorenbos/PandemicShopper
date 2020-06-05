using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace PandemicShoppingGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private SpriteFont font;

        private Player player;
        private LevelObject cashier;

        private Texture2D textureShopList;
        private Texture2D textureBag;
        private Texture2D background;
        private int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        List<LevelObject> objectList = new List<LevelObject>();
        List<Product> shoppingList = new List<Product>();
        List<Product> productList = new List<Product>();
        List<Enemy> enemies = new List<Enemy>();
        public int score = 0;
        public string level;


        public Game1(string level)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.HardwareModeSwitch = false;
            Content.RootDirectory = "Content";
            this.level = level;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            font = Content.Load<SpriteFont>("newFont");
            // TODO: Add your initialization logic here
            //Load Textures
            textureBag = Content.Load<Texture2D>("bag");
            textureShopList = Content.Load<Texture2D>("shoplist");
            var textureShelf = Content.Load<Texture2D>("shelf");
            var textureShelfVertical = Content.Load<Texture2D>("shelfVertical");
            var textureCashier = Content.Load<Texture2D>("cashier");
            var textureEnemy = Content.Load<Texture2D>("enemy");
            var texture = Content.Load<Texture2D>("player1");

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Levels/" + level + ".xml");

            //Load Shelves
            XmlNodeList verticalShelves = xDoc.GetElementsByTagName("VerticalShelve");
            for(int i = 0; i < verticalShelves.Count; i++)
            {
                LevelObject obj = new LevelObject(Int32.Parse(verticalShelves[i].FirstChild.InnerText),Int32.Parse(verticalShelves[i].LastChild.InnerText), textureShelfVertical);
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
            player = new Player(Int32.Parse(playerEl[0].FirstChild.InnerText), Int32.Parse(playerEl[0].LastChild.InnerText), texture);

            //Init textures
            var singMilkTexture = Content.Load<Texture2D>("singmilk");
            var singBreadTexture = Content.Load<Texture2D>("singBread");
            var singKetchTexture = Content.Load<Texture2D>("singKetch");
            var singOliveTexture = Content.Load<Texture2D>("singOlive");

            //Init products in world
            XmlNodeList productsinWorld = xDoc.GetElementsByTagName("WorldProduct");
            for (int i = 0; i < productsinWorld.Count; i++)
            {
                Product prod = new Product(productsinWorld[i].FirstChild.InnerText, Int32.Parse(productsinWorld[i].ChildNodes[1].InnerText), Int32.Parse(productsinWorld[i].ChildNodes[2].InnerText) , null);
                if(productsinWorld[i].FirstChild.InnerText == "singMilk")
                {
                    prod.texture = singMilkTexture;
                } else if(productsinWorld[i].FirstChild.InnerText == "singBread")
                {
                    prod.texture = singBreadTexture;
                } else if (productsinWorld[i].FirstChild.InnerText == "singKetch")
                {
                    prod.texture = singKetchTexture;
                }
                else if (productsinWorld[i].FirstChild.InnerText == "SingOlive")
                {
                    prod.texture = singOliveTexture;
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

            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load background
            background = Content.Load<Texture2D>("tile");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime, objectList, productList,enemies);

            //End game
            if (cashier.IsTouchingLeft(player) || cashier.IsTouchingTop(player) || cashier.IsTouchingRight(player) || cashier.IsTouchingBottom(player) || player.health == 0)
            {
                player.Speed = 0;
                List<string> shoplist = new List<string>();
                List<string> inventory = new List<string>();
                IEnumerable<string> missing;
                
                foreach (Product prod in player.inventory)
                {
                    inventory.Add(prod.name);
                }
                foreach (Product prod in shoppingList)
                {
                    shoplist.Add(prod.name);
                }
                
                missing = shoplist.Except(inventory);
                score = player.health - (missing.Count() * 10);
                
            }
           

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Draw Background
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap);
            spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), new Rectangle(0,0, background.Width * 30, background.Height * 20), Color.White);
            spriteBatch.End();
           
            spriteBatch.Begin();

            //Draw Health
            spriteBatch.DrawString(font, "Health: " + player.health, new Vector2(20, 20), Color.Black);
            player.Draw(spriteBatch);

            //Draw Health
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(200, 20), Color.Black);
            player.Draw(spriteBatch);

            //Draw Cashier
            cashier.Draw(spriteBatch);

            //Draw ShoppingList
            spriteBatch.Draw(textureShopList, new Rectangle(20,60,40,40), Color.White);
            int xPositionShoplistItems = 70;
            for (int i = 0; i < shoppingList.Count; i++)
            {  
                shoppingList[i].Position.X = xPositionShoplistItems;
                shoppingList[i].Draw(spriteBatch);
                //Alignment Shoppinglist items
                xPositionShoplistItems += 20 + shoppingList[i].texture.Width;
            }

            //Draw Inventory
            spriteBatch.Draw(textureBag, new Rectangle(20, 120,40,40), Color.White);
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

            base.Draw(gameTime);
        }
    }
}

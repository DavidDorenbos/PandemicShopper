using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

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

        private Texture2D textureShopList;
        private Texture2D textureBag;
        private Texture2D background;
        private int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        List<LevelObject> objectList = new List<LevelObject>();
        List<Product> shoppingList = new List<Product>();
        List<Product> productList = new List<Product>();
        List<Enemy> enemies = new List<Enemy>();
        LevelObject cashier = new LevelObject(200, 300,null);
        public int score = 0;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.HardwareModeSwitch = false;
            Content.RootDirectory = "Content";
            
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
            //Load objects
            textureBag = Content.Load<Texture2D>("bag");
            textureShopList = Content.Load<Texture2D>("shoplist");
            var textureShelf = Content.Load<Texture2D>("shelf");
            var textureShelfVertical = Content.Load<Texture2D>("shelfVertical");
            var textureCashier = Content.Load<Texture2D>("cashier");
            var textureEnemy = Content.Load<Texture2D>("enemy");
            LevelObject shelf = new LevelObject(1750, 800, textureShelf);
            LevelObject shelf1 = new LevelObject(1750, 500, textureShelf);
            LevelObject shelf2 = new LevelObject(1600, 500, textureShelf);
            LevelObject shelf3 = new LevelObject(1600, 800, textureShelf);
            LevelObject shelf4 = new LevelObject(1300, 800, textureShelf);
            LevelObject shelf10 = new LevelObject(1450, 800, textureShelf);
            LevelObject shelf11 = new LevelObject(1600, 800, textureShelf);
            LevelObject shelf12 = new LevelObject(1600, 800, textureShelf);
            LevelObject shelf9 = new LevelObject(1200, 740, textureShelfVertical);
            LevelObject shelf5 = new LevelObject(1200, 600, textureShelfVertical);
            LevelObject shelf6 = new LevelObject(1600, 350, textureShelfVertical);
            LevelObject shelf7 = new LevelObject(1200, 450, textureShelfVertical);
            LevelObject shelf22 = new LevelObject(1600, 200, textureShelfVertical);
            LevelObject shelf8 = new LevelObject(1050, 450, textureShelf);
            LevelObject shelf13 = new LevelObject(900, 450, textureShelf);
            LevelObject shelf14 = new LevelObject(750, 450, textureShelf);
            LevelObject shelf15 = new LevelObject(600, 450, textureShelf);
            LevelObject shelf16 = new LevelObject(450, 450, textureShelf);
            LevelObject shelf17 = new LevelObject(300, 450, textureShelf);
            LevelObject shelf18 = new LevelObject(1450, 200, textureShelf);
            LevelObject shelf19 = new LevelObject(1300, 200, textureShelf);
            LevelObject shelf20 = new LevelObject(1150, 200, textureShelf);
            LevelObject shelf21 = new LevelObject(1000, 200, textureShelf);
            LevelObject shelf23 = new LevelObject(850, 200, textureShelf);
            LevelObject shelf24 = new LevelObject(700, 200, textureShelf);
            LevelObject shelf25 = new LevelObject(550, 200, textureShelf);
            LevelObject shelf26 = new LevelObject(400, 200, textureShelf);

            objectList.Add(shelf);
            objectList.Add(shelf1);
            objectList.Add(shelf2);
            objectList.Add(shelf3);
            objectList.Add(shelf4);
            objectList.Add(shelf5);
            objectList.Add(shelf6);
            objectList.Add(shelf7);
            objectList.Add(shelf8);
            objectList.Add(shelf9);
            objectList.Add(shelf10);
            objectList.Add(shelf11);
            objectList.Add(shelf12);
            objectList.Add(shelf13);
            objectList.Add(shelf14);
            objectList.Add(shelf15);
            objectList.Add(shelf16);
            objectList.Add(shelf17);
            objectList.Add(shelf18);
            objectList.Add(shelf19);
            objectList.Add(shelf20);
            objectList.Add(shelf21);
            objectList.Add(shelf22);
            objectList.Add(shelf23);
            objectList.Add(shelf24);
            objectList.Add(shelf25);
            objectList.Add(shelf26);


            cashier.texture = textureCashier;

            //Init Player
            var texture = Content.Load<Texture2D>("player1");
            player = new Player(texture);
            player.Position = new Vector2(1800, 700);

            //Init textures
            var singMilkTexture = Content.Load<Texture2D>("singmilk");
            var singBreadTexture = Content.Load<Texture2D>("singBread");
            var singKetchTexture = Content.Load<Texture2D>("singKetch");
            var singOliveTexture = Content.Load<Texture2D>("singOlive");

            //Init shoppinglist            
            Product singMilk = new Product("singMilk", 0, 70, singMilkTexture);
            Product singBread = new Product("singBread", 0, 70, singBreadTexture);
            Product singKetch = new Product("singKetch", 0, 70, singKetchTexture);
            Product SingOlive = new Product("SingOlive", 0, 70, singOliveTexture);

            shoppingList.Add(singMilk);
            shoppingList.Add(singBread);
            shoppingList.Add(singKetch);
            shoppingList.Add(SingOlive);


            //Init Products in world
            Product singMilkWorld = new Product("singMilk", 1000, 400, singMilkTexture);
            Product singBreadWorld = new Product("singBread", 600, 400, singBreadTexture);

            productList.Add(singMilkWorld);
            productList.Add(singBreadWorld);

            //Init Enemies
            Enemy enemy1 = new Enemy(1000, 300, textureEnemy);

            enemies.Add(enemy1);

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


            // TODO: Add your drawing code here
           
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

﻿using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PandemicShoppingGame.GameParts
{
    public class Player
    {
        public Texture2D texture;
        public Vector2 Position;
        public float Speed = 2f;
        public float angle = 0;
        public Vector2 origin;
        public List<Product> inventory = new List<Product>();
        public KeyboardState newState;
        public KeyboardState oldState;

        private SoundEffect slurp;
        private SoundEffect scream;

        public int health;
        public int armor;

        private int healthDecreaseDelay = 0;

        public Player(int x, int y, Texture2D texture, ContentManager content)
        {
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            this.Position.X = x;
            this.Position.Y = y;
            this.texture = texture;
            this.health = 100;
            this.armor = 0;

            slurp = content.Load<SoundEffect>("Effects/slurp");
            scream = content.Load<SoundEffect>("Effects/scream");
        }

        public void Update(GameTime gameTime, List<LevelObject> levelObjects, List<Product> productList, List<Enemy> enemies)
        {
            Move();

            // Picking up items when near en pressing e
            newState = Keyboard.GetState();
            if (newState.IsKeyUp(Keys.E) && oldState.IsKeyDown(Keys.E))
            {
                foreach (Product prod in productList)
                {
                    if (prod.isClose(this) && !inventory.Contains(prod))
                    {
                        if (prod.GetName() == "mask")
                        {
                            armor += 25;
                            slurp.Play();
                        }
                        else
                        {
                            inventory.Add(prod);
                            slurp.Play();
                        }
                    }
                }
            }
            oldState = newState;

            //Decreasing health when standing too close to enemies
            if (healthDecreaseDelay == 4)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.isClose(this) && armor > 0)
                    {
                        this.armor -= 1;
                        scream.Play();
                    }
                    else if (enemy.isClose(this) && health > 0)
                    {
                        this.health -= 1;
                        scream.Play();
                    }
                }
                healthDecreaseDelay = 0;
            }
            healthDecreaseDelay++;



            foreach (LevelObject obj in levelObjects)
            {
               if(obj.IsTouchingLeft(this))
               {

                   this.Speed = 0;
                   this.Position.X -= 3;
                   this.Speed = 2;
               }  
               if (obj.IsTouchingRight(this))
               {

                   this.Speed = 0;
                   this.Position.X += 3;
                   this.Speed = 2;
               }
               if (obj.IsTouchingTop(this))
               {
                   this.Speed = 0;
                   this.Position.Y -= 3;
                   this.Speed = 2;
               }
                if (obj.IsTouchingBottom(this))
                {
                    this.Speed = 0;
                    this.Position.Y += 3;
                    this.Speed = 2;
                }
            }
        }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position.X -= Speed;
                angle = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += Speed;
                angle = (float)Math.PI / 1.0f;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position.Y -= Speed;
                angle = (float)Math.PI / 2.0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += Speed;
                angle = ((float)Math.PI / 2.0f) * 3;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, angle, origin,1, SpriteEffects.None, 0f);
        }
    }
}

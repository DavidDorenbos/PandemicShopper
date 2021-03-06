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

namespace PandemicShoppingGame.Level
{
    public class Player : Pawn
    {
        public Vector2 origin;
        public List<Product> inventory = new List<Product>();

        public SoundEffect slurp;
        public SoundEffect scream;
        public SoundEffect cough;

        public int health = 100;
        public int armor = 0;
        public int coughing = 1;

        public Player(ContentManager _content, int x, int y, float speed, float angle)
            : base(x, y, speed, angle)
        {
            this.texture = _content.Load<Texture2D>("player1");
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            slurp = _content.Load<SoundEffect>("Effects/slurp");
            scream = _content.Load<SoundEffect>("Effects/scream");
            cough = _content.Load<SoundEffect>("Effects/cough");
        }

        public void PickUpProduct(Product p)
        {
            inventory.Add(p);
        }

        public override void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= speed;
                angle = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += speed;
                angle = (float)Math.PI / 1.0f;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= speed;
                angle = (float)Math.PI / 2.0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += speed;
                angle = ((float)Math.PI / 2.0f) * 3;
            }
        }
    }
}

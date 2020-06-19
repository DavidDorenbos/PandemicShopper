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
using PandemicShoppingGame.Level;

namespace PandemicShoppingGame.GameParts
{
    public class Player : Pawn
    {
        public Vector2 origin;
        public List<Product> inventory = new List<Product>();

        private SoundEffect slurp;
        private SoundEffect scream;

        public int health = 100;
        public int armor = 0;

        public Player(Texture2D texture, Vector2 position, float speed, float angle, int x, int y)
            : base(texture, position, speed, angle)
        {
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            slurp = content.Load<SoundEffect>("Effects/slurp");
            scream = content.Load<SoundEffect>("Effects/scream");

            this.texture = texture;
            position = new Vector2(x, y);
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

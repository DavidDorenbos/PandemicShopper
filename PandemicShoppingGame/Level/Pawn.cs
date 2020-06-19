using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PandemicShoppingGame.Level
{
    public abstract class Pawn
    {
        protected Texture2D texture;
        protected Vector2 position { get; set; }
        protected float speed;
        protected float angle;

        public Pawn(Texture2D texture, Vector2 position, float speed, float angle)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            this.angle = angle;
        }

        public abstract void Move();
    }
}

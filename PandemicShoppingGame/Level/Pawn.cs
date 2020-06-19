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
        public Vector2 position;
        public Texture2D texture;

        public float speed;
        public float angle;

        public Pawn(int x, int y, float speed, float angle)
        {
            this.position = new Vector2(x, y);
            this.speed = speed;
            this.angle = angle;
        }

        public abstract void Move();
    }
}

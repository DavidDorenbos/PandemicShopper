using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandemicShoppingGame.Level
{
    public class LevelObject 
    {

        private int x { get; set; }
        private int y { get; set; }
        public Texture2D texture;
        public Vector2 Position;

        public LevelObject(int x, int y, Texture2D texture)
        {
            this.x = x;
            this.y = y;
            Position = new Vector2(x, y);
            this.texture = texture;
        }

        public bool IsTouchingLeft(Player player)
        {
            return this.texture.Width + this.x > player.position.X + (player.texture.Width / 2f) &&
              this.x < player.position.X + (player.texture.Width / 2f) &&
              this.texture.Height + this.y > player.position.Y - (player.texture.Height / 2f) &&
              this.y < player.position.Y + (player.texture.Height / 2f);
        }

        public bool IsTouchingRight(Player player)
        {
            return this.texture.Width + this.x > player.position.X - (player.texture.Width / 2f) &&
              this.x < player.position.X - (player.texture.Width / 2f) &&
              this.texture.Height + this.y > player.position.Y - (player.texture.Height / 2f) &&
              this.y < player.position.Y + (player.texture.Height / 2f);
        }

        public bool IsTouchingTop(Player player)
        {
            return this.y < player.position.Y + (player.texture.Height / 2f) &&
              this.y + texture.Height > player.position.Y + (player.texture.Height / 2f) &&
              this.texture.Width + this.x > player.position.X - (player.texture.Width / 2f) &&
              this.x < player.position.X + (player.texture.Width / 2f);
        }

        public bool IsTouchingBottom(Player player)
        {
            return y < player.position.Y + (player.texture.Height / 2f) &&
              texture.Height + y > player.position.Y - (player.texture.Height / 2f) &&
              x + texture.Width > player.position.X - (player.texture.Width / 2f) &&
              x < player.position.X + (player.texture.Width / 2f);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PandemicShoppingGame.Level
{
    public class LevelObject 
    {

        public Texture2D texture;
        public Vector2 position;

        public LevelObject(ContentManager _content, int x, int y, String texture)
        {
            position = new Vector2(x, y);
            this.texture = _content.Load<Texture2D>(texture);
        }

        public bool IsTouchingLeft(Player player)
        {
            return this.texture.Width + position.X > player.position.X + (player.texture.Width / 2) &&
              position .X < player.position.X + (player.texture.Width / 2) &&
              this.texture.Height + position.Y > player.position.Y - (player.texture.Height / 2) &&
              position.Y < player.position.Y + (player.texture.Height / 2);
        }

        public bool IsTouchingRight(Player player)
        {
            return this.texture.Width + position.X > player.position.X - (player.texture.Width / 2) &&
              position.X < player.position.X - (player.texture.Width / 2) &&
              this.texture.Height + position.Y > player.position.Y - (player.texture.Height / 2) &&
              position.Y < player.position.Y + (player.texture.Height / 2);
        }

        public bool IsTouchingTop(Player player)
        {
            return position.Y < player.position.Y + (player.texture.Height / 2) &&
              position.Y + texture.Height > player.position.Y + (player.texture.Height / 2) &&
              this.texture.Width + position.X > player.position.X - (player.texture.Width / 2) &&
              position.X < player.position.X + (player.texture.Width / 2);
        }

        public bool IsTouchingBottom(Player player)
        {
            return position.Y < player.position.Y + (player.texture.Height / 2) &&
              texture.Height + position.Y > player.position.Y - (player.texture.Height / 2) &&
              position.X + texture.Width > player.position.X - (player.texture.Width / 2) &&
              position.X < player.position.X + (player.texture.Width / 2);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandemicShoppingGame.GameParts
{
    public class Product
    {
        public string name;
        private int x;
        private int y;
        public Texture2D texture;
        public Vector2 Position;

        public Product(String name, int x, int y, Texture2D texture)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.texture = texture;
            Position = new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        //Check if player is close to product
        public bool isClose(Player player)
        {
            float playerx = player.Position.X;
            float playery = player.Position.Y;
            if(Math.Abs(playerx - x) < 100 && Math.Abs(playery - y) < 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String GetName()
        {
            return name;
        }

    }
}

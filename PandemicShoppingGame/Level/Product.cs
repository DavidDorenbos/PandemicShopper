using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandemicShoppingGame.Level
{
    public class Product
    {
        public string name;
        public Texture2D texture;
        public Vector2 position;

        public Product(ContentManager _content, String name, int x, int y)
        {
            this.name = name;
            this.texture = _content.Load<Texture2D>(name);
            position = new Vector2(x, y);
        }



        //Check if player is close to product
        public bool isClose(Player player)
        {
            float playerx = player.position.X;
            float playery = player.position.Y;
            if (Math.Abs(playerx - position.X) < 100 && Math.Abs(playery - position.Y) < 100)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PandemicShoppingGame.Level;

namespace PandemicShoppingGame.GameParts
{
    public class Enemy : Pawn
    {
        private int x;
        private int y;
        public Enemy(Texture2D texture, Vector2 position, float speed, float angle, int x, int y)
            :base(texture, position, speed, angle)
        {
            this.x = x;
            this.y = y;

            this.texture = texture;
            position = new Vector2(x, y);
        }

        //Check if player is close to enemy
        public bool isClose(Player player)
        {
            float playerx = player.
            //float playery = player.position.Y;
            if (Math.Abs(playerx - x) < 100 && Math.Abs(playery - y) < 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Move()
        {
            //Maybe in de toekomst. Nu mag ie gewoon ffe blijven staan.
        }
    }
}

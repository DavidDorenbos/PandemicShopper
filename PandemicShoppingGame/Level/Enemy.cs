using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandemicShoppingGame.Level;

namespace PandemicShoppingGame.Level
{
    public class Enemy : Pawn
    {
        public Enemy(ContentManager _content, int x, int y, float speed, float angle)
            :base(x, y, speed, angle)
        {
            this.texture = _content.Load<Texture2D>("enemy");
        }

        //Check if player is close to enemy
        public bool isClose(Player player, int range)
        {
            float playerx = player.position.X;
            float playery = player.position.Y;
            if (Math.Abs(playerx - position.X) < range && Math.Abs(playery - position.Y) < range)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using System.Xml;

namespace PandemicShoppingGame.Level
{
    class LevelManager
    {
        private bool noNewLevel;

        private ContentManager _content;
        public List<Enemy> enemies = new List<Enemy>();
        public Player player;

        public LevelManager(ContentManager _content)
        {
            this.noNewLevel = false;
        }

        public void LoadLevel(int level)
        {
            //Load Textures
            XmlDocument xDoc = new XmlDocument();
            String levelDoc = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Levels/" + level + ".xml";

            LoadPawns(xDoc);
        }

        private void LoadPawns(XmlDocument xDoc)
        {
            //Init Player
            XmlNodeList playerEl = xDoc.GetElementsByTagName("Player");
            player = new Player(_content, Int32.Parse(playerEl[0].FirstChild.InnerText), Int32.Parse(playerEl[0].LastChild.InnerText), 2f, 0);

            //Init enemies
            XmlNodeList enemiesEl = xDoc.GetElementsByTagName("Enemy");
            for (int i = 0; i < enemiesEl.Count; i++)
            {
                Enemy en = new Enemy(_content, Int32.Parse(enemiesEl[i].FirstChild.InnerText), Int32.Parse(enemiesEl[i].LastChild.InnerText), 0, 0);
                enemies.Add(en);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using System.Xml;
using System.IO;

namespace PandemicShoppingGame.Level
{
    class LevelManager
    {
        private bool noNewLevel;

        private ContentManager _content;

        public List<Enemy> enemies = new List<Enemy>();
        public Player player;

        public List<LevelObject> shelves = new List<LevelObject>();
        public List<Product> productList = new List<Product>();
        public List<Product> shopList = new List<Product>();

        public LevelObject cashier;

        public LevelManager(ContentManager _content)
        {
            this._content = _content;
            this.noNewLevel = false;
        }

        public void LoadLevel(int level)
        {
            //Load Textures
            XmlDocument xDoc = new XmlDocument();
            String levelDoc = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Levels/" + level + ".xml";
            if (File.Exists(levelDoc))
            {
                xDoc.Load(levelDoc);

                LoadPawns(xDoc);
                LoadShelves(xDoc);
                LoadCashier(xDoc);
            }
        }

        //Load all pawns out of the xml file
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

        private void LoadShelves(XmlDocument xDoc)
        {
            XmlNodeList verticalShelves = xDoc.GetElementsByTagName("VerticalShelve");
            for (int i = 0; i < verticalShelves.Count; i++)
            {
                LevelObject obj = new LevelObject(_content, Int32.Parse(verticalShelves[i].FirstChild.InnerText), Int32.Parse(verticalShelves[i].LastChild.InnerText), "shelfVertical");
                shelves.Add(obj);
            }
            XmlNodeList horizontalShelves = xDoc.GetElementsByTagName("HorizontalShelve");
            for (int i = 0; i < horizontalShelves.Count; i++)
            {
                LevelObject obj = new LevelObject(_content, Int32.Parse(horizontalShelves[i].FirstChild.InnerText), Int32.Parse(horizontalShelves[i].LastChild.InnerText), "shelf");
                shelves.Add(obj);
            }
        }

        private void LoadCashier(XmlDocument xDoc)
        {

        }
    }
}

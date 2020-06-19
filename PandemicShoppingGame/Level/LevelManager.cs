using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PandemicShoppingGame.Level
{
    class LevelManager
    {
        private bool noNewLevel;

        public List<Pawn> enemies = new List<Pawn>();
        public Pawn player;

        public LevelManager()
        {
            this.noNewLevel = false;
        }

        public void LoadLevel(int level)
        {
            //Load Textures
            XmlDocument xDoc = new XmlDocument();
            String levelDoc = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Levels/" + level + ".xml";
        }

        private void LoadPawns()
        {

        }
    }
}

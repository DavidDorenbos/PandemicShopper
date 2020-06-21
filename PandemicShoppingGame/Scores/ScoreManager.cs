using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PandemicShoppingGame.Level;
using System.Xml;

namespace PandemicShoppingGame.Scores
{
    public class ScoreManager
    {
        private int score = 0;

        private int level;
        private int health;
        private int time;
        private List<Product> inventory = new List<Product>();
        private List<Product> shopList = new List<Product>();

        public ScoreManager (int level, int health, int time, List<Product> inventory, List<Product> shopList)
        {
            this.level = level;
            this.health = health;
            this.time = time;
            this.inventory = inventory;
            this.shopList = shopList;
        }

        public int GetScore()
        {
            return score;
        }

        public void CalculateScore ()
        {
            Dictionary<string,int> inventoryList = new Dictionary<string, int>();
            foreach(Product p in inventory)
            {
                if (inventoryList.ContainsKey(p.name))
                {
                    inventoryList[p.name]++;
                }
                else
                {
                    inventoryList.Add(p.name, 1);
                }
            }

            foreach (Product p in shopList)
            {
                if (inventoryList.ContainsKey(p.name))
                {
                    if(inventoryList[p.name] == 1)
                    {
                        inventoryList.Remove(p.name);
                    }
                    else
                    {
                        inventoryList[p.name]--;
                    }
                }
                else
                {
                    if (inventoryList.ContainsKey("missing"))
                    {
                        inventoryList["missing"]++;
                    }
                    else
                    {
                        inventoryList.Add("missing", 1);
                    }
                    
                }
            }
            
            int sum = inventoryList.Sum(x => x.Value);
            if(sum == 0)
            {
                sum = 1;
            }

            score = (1000 + (health * (10 * level))) - (time * (10 * level))  - (sum * 25);

            if (score < 1)
            {
                score = 1;
            }
        }

        public void SaveScore(int level)
        {
            String XmlScoreFile = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Scores/score.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlScoreFile);
            XmlElement levelNode = doc.DocumentElement["Level" + level.ToString()];
            XmlElement scoreNode = doc.CreateElement("Score");
            scoreNode.InnerText = score.ToString();
            if (levelNode == null)
            {
                XmlElement node = doc.CreateElement("Level" + level.ToString());
                node.AppendChild(scoreNode);
            }
            else
            {
                if (levelNode.FirstChild == null)
                {
                    levelNode.AppendChild(scoreNode);
                }
                else
                {
                    int highScoreEl = Int32.Parse(levelNode.FirstChild.InnerText);

                    if (score > highScoreEl)
                    {
                        levelNode.FirstChild.InnerText = score.ToString();
                    }
                }
            }

            doc.Save(XmlScoreFile);
        }
    }
}

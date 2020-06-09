using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace PandemicShoppingGame.Scores
{
    public class ScoreManager
    {
        private int level;
        private int score;

        public ScoreManager (int level, int score)
        {
            this.level = level;
            this.score = score;
        }

        public int GetScore()
        {
            return score;
        }

        public void CalculateScore (int health)
        {
            score = level * health;

            String XmlScoreFile = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Scores/score.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlScoreFile);
            XmlElement levelNode = doc.DocumentElement["Level" + level.ToString()];
            XmlElement scoreNode = doc.CreateElement("Score");
            scoreNode.InnerText = score.ToString();
            if(levelNode == null)
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

﻿using System;
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

        public void getScore()
        {
            Console.WriteLine(score);
        }

        public void CalculateScore (int health)
        {
            score = level * health;

            String XmlScoreFile = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Scores/score.xml";

        }
    }
}
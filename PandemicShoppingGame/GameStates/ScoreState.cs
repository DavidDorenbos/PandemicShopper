using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandemicShoppingGame.GameControls;
using PandemicShoppingGame.Scores;

namespace PandemicShoppingGame.GameStates
{
    public class ScoreState : State
    {

        private List<Component> _components;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Vector2 scorePosition;
        public SpriteFont font;

        private Texture2D[] numbertextures = new Texture2D[10];

        private int level;

        public ScoreState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content, int level)
          : base(game, graphicsDevice, content)
        {

            font = _content.Load<SpriteFont>("Fonts/Standard");
            if (level == 0)
            {
                this.level = 1;
            }
            else
            {
                this.level = level;
            }
            
            for(int i = 0; i <10; i++)
            {
                numbertextures[i] = _content.Load<Texture2D>("Numbers/" + i.ToString());
            }


            var buttonFont = _content.Load<SpriteFont>("Fonts/Standard");

            backgroundTexture = _content.Load<Texture2D>("Backgrounds/MainMenuBackground");
            backgroundPosition = new Vector2(0, 0);

            var backButtonTexture = _content.Load<Texture2D>("Buttons/Back");

            var backButton = new Button(backButtonTexture, buttonFont)
            {
                Position = new Vector2(1300, 780),
            };

            backButton.Click += BackButton_Click;

            _components = new List<Component>()
              {
                backButton,
              };

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content, level));
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {

        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            int levelHelper = 1;
            int y = 300;
            foreach (int score in getHighScores())
            {
                String output = "Level " + levelHelper + ": " + score;
                spriteBatch.DrawString(font, output, new Vector2(600, y), Color.Black);
                levelHelper++;
                y += 50;
            }
            spriteBatch.End();


            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }

        public List<int> getHighScores()
        {
            List<int> scores = new List<int>();

            String XmlScoreFile = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Scores/score.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlScoreFile);

            for (int i = 1; i < 10; i++)
            {
                XmlElement levelNode = doc.DocumentElement["Level" + i];
                if (levelNode.FirstChild == null)
                {
                    scores.Add(0);
                }
                else
                {
                    scores.Add(Int32.Parse(levelNode.FirstChild.InnerText));
                }
            }
            return scores;
        }
    }
}

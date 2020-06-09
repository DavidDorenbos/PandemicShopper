using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandemicShoppingGame.GameControls;
using PandemicShoppingGame.Scores;

namespace PandemicShoppingGame.GameStates
{
    public class EndGameState : State
    {

        private List<Component> _components;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D scoreTexture;
        private Vector2 scorePosition;

        private Texture2D[] numbertextures = new Texture2D[10];

        private int level;
        private int score;

        public EndGameState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content, ScoreManager _scoreManager, int level)
          : base(game, graphicsDevice, content)
        {
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

            score = _scoreManager.getScore();

            var restartButtonTexture = _content.Load<Texture2D>("Buttons/Restart");
            var nextLevelsButtonTexture = _content.Load<Texture2D>("Buttons/Next");
            var mainMenuButtonTexture = _content.Load<Texture2D>("Buttons/MainMenu");
            var exitButtonTexture = _content.Load<Texture2D>("Buttons/Exit");

            var buttonFont = _content.Load<SpriteFont>("Fonts/Standard");

            scoreTexture = _content.Load<Texture2D>("Titles/ScoreTitle");
            scorePosition = new Vector2(1300, 170);

            backgroundTexture = _content.Load<Texture2D>("Backgrounds/MainMenuBackground");
            backgroundPosition = new Vector2(0, 0);

            var restartGameButton = new Button(restartButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 170),
            };

            restartGameButton.Click += RestartGameButton_Click;

            var nextLevelButton = new Button(nextLevelsButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 376),
            };

            nextLevelButton.Click += NextLevelButton_Click;

            var mainMenuButton = new Button(mainMenuButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 582),
            };

            mainMenuButton.Click += MainMenuButton_Click;

            var exitButton = new Button(exitButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 788),
            };

            exitButton.Click += ExitButton_Click;
            
            _components = new List<Component>()
              {
                restartGameButton,
                nextLevelButton,
                mainMenuButton,
                exitButton,
              };

            
        }

        private void RestartGameButton_Click(object sender, EventArgs e)
        {
            int previouslevel = level -= 1;
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, previouslevel));
        }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, level));
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content, level));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
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
            spriteBatch.Draw(scoreTexture, scorePosition, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();

            int[] array = GetIntArray(score);
            int x = 1350;
            for (int i =0; i< array.Length; i++)
            {
                spriteBatch.Draw(numbertextures[array[i]], new Vector2(x, 250), Color.White);
                x += 40;
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
    }
}

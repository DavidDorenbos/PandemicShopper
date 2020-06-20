using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandemicShoppingGame.GameControls;

namespace PandemicShoppingGame.GameStates
{
    public class LevelSelectionState : State
    {

        private List<Component> _components;
        private List<Component> _levelsItems;

        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private SpriteFont font;

        public LevelSelectionState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            font = _content.Load<SpriteFont>("Fonts/Standard");

            var backButtonTexture = _content.Load<Texture2D>("Buttons/Back");
            var levelButtonTexture = _content.Load<Texture2D>("Buttons/LevelSelectionItem");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Standard");

            backgroundTexture = _content.Load<Texture2D>("Backgrounds/MainMenuBackground");
            backgroundPosition = new Vector2(0, 0);

            var backButton = new Button(backButtonTexture, buttonFont)
            {
                Position = new Vector2(150, 780),
            };

            backButton.Click += BackButton_Click;

            var oneButton = new Button(levelButtonTexture, buttonFont)
            {
                Position = new Vector2(610, 130),
                Text = "Level 1",
            };

            oneButton.Click += OneButton_Click;

            var twoButton = new Button(levelButtonTexture, buttonFont)
            {
                Position = new Vector2(1000, 130),
                Text = "Level 2",
            };

            twoButton.Click += TwoButton_Click;

            var threeButton = new Button(levelButtonTexture, buttonFont)
            {
                Position = new Vector2(1390, 130),
                Text = "Level 3",
            };

            threeButton.Click += ThreeButton_Click;

            var fourButton = new Button(levelButtonTexture, buttonFont)
            {
                Position = new Vector2(610, 396),
                Text = "Level 4",
            };

            fourButton.Click += FourButton_Click;

            var fiveButton = new Button(levelButtonTexture, buttonFont)
            {
                Position = new Vector2(1000, 396),
                Text = "Level 5",
            };

            fiveButton.Click += FiveButton_Click;

            var sixButton = new Button(levelButtonTexture, buttonFont)
            {
                Position = new Vector2(1390, 396),
                Text = "Level 6",
            };

            sixButton.Click += SixButton_Click;

            var sevenButton = new Button(levelButtonTexture, buttonFont)
            {
                Position = new Vector2(610, 662),
                Text = "Level 7",
            };

            sevenButton.Click += SevenButton_Click;

            var eightButton = new Button(levelButtonTexture, buttonFont)
            {
                Position = new Vector2(1000, 662),
                Text = "Level 8",
            };

            eightButton.Click += EightButton_Click;

            var nineButton = new Button(levelButtonTexture, buttonFont)
            {
                Position = new Vector2(1390, 662),
                Text = "Level 9",
            };

            nineButton.Click += NineButton_Click;

            _components = new List<Component>()
              {
                backButton,
                oneButton,
                twoButton,
                threeButton,
                fourButton,
                fiveButton,
                sixButton,
                sevenButton,
                eightButton,
                nineButton,
              };
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
        }

        private void OneButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, 1));
        }

        private void TwoButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, 2));
        }

        private void ThreeButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, 3));
        }

        private void FourButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
        }

        private void FiveButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
        }

        private void SixButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
        }

        private void SevenButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
        }

        private void EightButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
        }

        private void NineButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
        }

        private int GetNumberOfLevels()
        {
            int levels = 0;

            int i = 1;

            while (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Levels/" + i + ".xml"))
            {
                levels++;
                i++;
            }

            return levels;
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
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}

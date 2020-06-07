using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandemicShoppingGame.GameControls;

namespace PandemicShoppingGame.GameStates
{
    public class MainMenuState : State
    {
        private List<Component> _components;

        public MainMenuState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Buttons/Button");
            var startButtonTexture = _content.Load<Texture2D>("Buttons/Start");
            var levelsButtonTexture = _content.Load<Texture2D>("Buttons/Levels");
            var optionButtonTexture = _content.Load<Texture2D>("Buttons/Options");
            var exitButtonTexture = _content.Load<Texture2D>("Buttons/Exit");

            var buttonFont = _content.Load<SpriteFont>("Fonts/Standard");

            var startGameButton = new Button(startButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 170),
            };

            startGameButton.Click += StartGameButton_Click;

            var levelButton = new Button(levelsButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 376),
            };

            levelButton.Click += LevelButton_Click;

            var optionButton = new Button(optionButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 582),
            };

            optionButton.Click += OptionButton_Click;

            var exitGameButton = new Button(exitButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 788),
            };

            exitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
              {
                startGameButton,
                levelButton,
                optionButton,
                exitGameButton,
              };
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void LevelButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Fankie");
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new OptionsGameState(_game, _graphicsDevice, _content));
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
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
            // remove sprites if they're not needed
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

    }
}

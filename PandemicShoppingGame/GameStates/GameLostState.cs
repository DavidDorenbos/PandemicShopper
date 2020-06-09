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
    public class GameLostState : State
    {

        private List<Component> _components;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private SpriteFont font;

        public GameLostState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content, ScoreManager _scoreManager)
          : base(game, graphicsDevice, content)
        {
            font = _content.Load<SpriteFont>("Fonts/Standard");

            var MainMenuButtonTexture = _content.Load<Texture2D>("Buttons/MainMenu");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Standard");

            backgroundTexture = _content.Load<Texture2D>("Backgrounds/MainMenuBackground");
            backgroundPosition = new Vector2(0, 0);

            var backButton = new Button(MainMenuButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 788),
            };

            backButton.Click += BackButton_Click;

            _components = new List<Component>()
              {
                backButton,
              };
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
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

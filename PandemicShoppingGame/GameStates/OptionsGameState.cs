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
    public class OptionsGameState : State
    {

        private List<Component> _components;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;
        private int level;

        public OptionsGameState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content, int level)
          : base(game, graphicsDevice, content)
        {
            this.level = level;
            var backButtonTexture = _content.Load<Texture2D>("Buttons/Back");

            var buttonFont = _content.Load<SpriteFont>("Fonts/Standard");

            backgroundTexture = _content.Load<Texture2D>("Backgrounds/MainMenuBackground");
            backgroundPosition = new Vector2(0, 0);

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

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}

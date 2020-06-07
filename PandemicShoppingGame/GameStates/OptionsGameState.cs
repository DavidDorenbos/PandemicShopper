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

        public OptionsGameState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var mainMenuButtonTexture = _content.Load<Texture2D>("Buttons/MainMenu");

            var buttonFont = _content.Load<SpriteFont>("Fonts/Standard");

            var mainMenuButton = new Button(mainMenuButtonTexture, buttonFont)
            {
                Position = new Vector2(735, 170),
            };

            mainMenuButton.Click += MainMenuButton_Click;

            _components = new List<Component>()
              {
                mainMenuButton,
              };
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
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

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}

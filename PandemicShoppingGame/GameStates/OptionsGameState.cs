using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using PandemicShoppingGame.GameControls;

namespace PandemicShoppingGame.GameStates
{
    public class OptionsGameState : State
    {

        public SpriteFont font;
        private List<Component> _components;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;
        private int level;

        public OptionsGameState(BaseGame game, GraphicsDevice graphicsDevice, ContentManager content, int level)
          : base(game, graphicsDevice, content)
        {
            font = _content.Load<SpriteFont>("Fonts/Standard");

            this.level = level;
            var backButtonTexture = _content.Load<Texture2D>("Buttons/Back");
            var volUpButtonTexture = _content.Load<Texture2D>("Buttons/volUp");
            var volDownButtonTexture = _content.Load<Texture2D>("Buttons/volDown");

            var buttonFont = _content.Load<SpriteFont>("Fonts/Standard");

            backgroundTexture = _content.Load<Texture2D>("Backgrounds/MainMenuBackground");
            backgroundPosition = new Vector2(0, 0);

            var backButton = new Button(backButtonTexture, buttonFont)
            {
                Position = new Vector2(1300, 780),
            };

            backButton.Click += BackButton_Click;


            var volDownButton = new Button(volDownButtonTexture, buttonFont)
            {
                Position = new Vector2(700, 300),
            };

            volDownButton.Click += volDown_Click;


            var volUpButton = new Button(volUpButtonTexture, buttonFont)
            {
                Position = new Vector2(1100, 300),
            };

            volUpButton.Click += volUp_Click;

            _components = new List<Component>()
              {
                backButton,
                volUpButton,
                volDownButton
              };
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content, level));
        }
        private void volDown_Click(object sender, EventArgs e)
        {
            MediaPlayer.Volume -= 0.1f;
        }
        private void volUp_Click(object sender, EventArgs e)
        {
            MediaPlayer.Volume += 0.1f; 
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
            spriteBatch.DrawString(font, "Volume", new Vector2(900, 325), Color.Black);
            spriteBatch.End();

            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}

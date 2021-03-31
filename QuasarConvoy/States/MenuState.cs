﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using QuasarConvoy.Controls;
using System.Collections.Generic;
using System.Text;

namespace QuasarConvoy.States
{
    public class MenuState : State
    {
        private List<Component> components;
        private Texture2D background;
        private Rectangle mainFrame;

        public MenuState(Game1 _game, GraphicsDevice _graphicsDevice, ContentManager _contentManager) : base(_game, _graphicsDevice, _contentManager)
        {
            var playButtonTexture = _contentManager.Load<Texture2D>("UI Stuff/Play Button");
            var quitButtonTexture = _contentManager.Load<Texture2D>("UI Stuff/Quit Button");

            var newGameButton = new Button(playButtonTexture, _contentManager)
            {
                Position = new Vector2(_graphicsDevice.PresentationParameters.BackBufferWidth / 2 - 
                            playButtonTexture.Width / 2, _graphicsDevice.PresentationParameters.BackBufferHeight / 2 -
                            (playButtonTexture.Height * 4) / 5),         
            };

            newGameButton.Click += NewGameButton_Click;

            var quitButton = new Button(quitButtonTexture, _contentManager)
            {
                Position = new Vector2(_graphicsDevice.PresentationParameters.BackBufferWidth / 2 -
                            quitButtonTexture.Width / 2, _graphicsDevice.PresentationParameters.BackBufferHeight / 2 +
                            quitButtonTexture.Height / 4),
            };

            quitButton.Click += QuitButton_Click;

            components = new List<Component>()
            {
                newGameButton,
                quitButton,
            };

            background = _contentManager.Load<Texture2D>("UI Stuff/UI Tech Effect");
            mainFrame = new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, mainFrame, Color.White);
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //reminder, delete sprites when not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            game.ChangeStates(new GameState(game, graphicsDevice, contentManager));
        }
        private void QuitButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
}

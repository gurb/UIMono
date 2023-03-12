using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Security.Principal;
using UIMono.Core.Caretaker;

namespace UIMono.Test
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private UILoader _uILoader;

        public Game1()
        {
            //_graphics = new GraphicsDeviceManager(this);
            //_graphics.PreferredBackBufferWidth = 1280;
            //_graphics.PreferredBackBufferHeight = 720;
            //_graphics.ApplyChanges();

            int initial_screen_width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int initial_screen_height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = initial_screen_width,
                PreferredBackBufferHeight = initial_screen_height,
                IsFullScreen = false,
                PreferredDepthStencilFormat = DepthFormat.None
            };
            _graphics.PreferMultiSampling = false;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";

                
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _uILoader = new UILoader("ui.json", _spriteBatch, GraphicsDevice, Window, GraphicsDevice.Viewport, GraphicsAdapter.DefaultAdapter, _graphics);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _uILoader.UIUpdate();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _uILoader.UIRender();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
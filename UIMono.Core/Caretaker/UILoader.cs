using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIMono.Core.Managers;

namespace UIMono.Core.Caretaker
{
    public class UILoader
    {
        private UICaretaker caretaker { get; set; }
        private SpriteBatch batch { get; set; }
        private GraphicsDevice device { get; set; }
        private GameWindow window { get; set; }

        public UILoader(string path, SpriteBatch batch, GraphicsDevice device, GameWindow window, Viewport viewport, GraphicsAdapter adapter, GraphicsDeviceManager graphics)
        {
            GraphicsManager.GraphicsDevice = device;
            WindowManager.Window = window;
            GraphicsManager.DefaultViewport = viewport;
            GraphicsManager.GraphicsAdapter = adapter;
            GraphicsManager.GraphicsDeviceManager = graphics;
            this.batch = batch;
            this.device = device;

            Init(path);
        }

        private void Init(string path)
        {
            caretaker = new UICaretaker(path);
        }

        public void UIUpdate()
        {
            caretaker.Update();
        }

        public void UIRender()
        {
            caretaker.Render(batch);
        }

    }
}

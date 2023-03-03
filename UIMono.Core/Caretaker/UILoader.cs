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

        public UILoader(string path, SpriteBatch batch, GraphicsDevice device)
        {
            TextureManager.GraphicsDevice = device;
            this.batch = batch;
            this.device = device;

            Init(path);
        }

        private void Init(string path)
        {
            caretaker = new UICaretaker(path);
        }

        public void UIRender()
        {
            caretaker.Render(batch);
        }

    }
}

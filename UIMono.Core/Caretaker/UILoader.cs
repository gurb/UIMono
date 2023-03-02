using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMono.Core.Caretaker
{
    public class UILoader
    {
        private UICaretaker caretaker { get; set; }
        private SpriteBatch batch { get; set; }

        public UILoader(string path, SpriteBatch batch)
        {
            caretaker = new UICaretaker(path);
            this.batch = batch;
        }

        public void UIRender()
        {
            caretaker.Render(batch);
        }

    }
}

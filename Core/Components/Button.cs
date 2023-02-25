using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIMono.Core.Managers;

namespace UIMono.Core.Components
{
    public class Button : Component
    {
       
        public bool Pressed { get; set; }
        public string Label { get; set; } = string.Empty;
        public Color LabelColor { get; set; }
        public Color? HoverBackgroundColor { get; set; }
        public Color? HoverLabelColor { get; set; }
        public int BorderThickness { get; set; } = 0;
        public Color? BorderColor { get; set; }
        public int BorderRadius { get; set; }

        public Button(Texture2D Texture2D, string Label)
        {
            this.Texture2D = Texture2D;
            this.Size = new Vector2(Texture2D.Width, Texture2D.Height);
            this.Label = Label;
        }

        public Button(string Label, int width, int height)
        {
            Texture2D = TextureManager.GenerateTexture(Color.White, width, height);
            this.Label = Label;
            this.Size = new Vector2(width, height);
        }


    }
}

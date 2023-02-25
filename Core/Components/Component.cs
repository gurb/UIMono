using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMono.Core.Components
{
    public class Component: IComponent
    {
        public string ParentTag { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Size { get; set; } = Vector2.Zero;
        public int ZIndex { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public Color BackgroundColor { get; set; }
        public List<IComponent> Children { get; set; } = new List<IComponent>();
        public Texture2D? Texture2D { get; set; }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2D, Position, BackgroundColor);
        }

        public virtual void Update()
        {
           
        }

    }
}

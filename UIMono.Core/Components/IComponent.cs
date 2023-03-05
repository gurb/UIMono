using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMono.Core.Components
{
    public interface IComponent
    {
        string ParentTag { get; set; }
        string Tag { get; set; } 
        Vector2 Position { get; set; } 
        Vector2 Size { get; set; } 
        int ZIndex { get; set; }
        bool Enabled { get; set; }
        bool Visible { get; set; }
        Color BackgroundColor { get; set; }
        List<IComponent> Children { get; set; }
        Texture2D? Texture2D { get; set; }
        RenderTarget2D? RenderTarget2D { get; set; }
        bool HasParent { get; set; }
        float Opacity { get; set; }


        public void Draw(SpriteBatch spriteBatch);

        public void Update();
    }
}

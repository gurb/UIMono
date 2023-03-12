using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIMono.Core.Enums;

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
        IComponent Parent { get; set; }
        List<IComponent> Children { get; set; }
        Texture2D? Texture2D { get; set; }
        RenderTarget2D? RenderTarget2D { get; set; }

        SpriteBatch Batch { get; set; }
        Viewport Viewport { get; set; }
        SizeType SizeType { get; set; }
        bool HasParent { get; set; }
        float Opacity { get; set; }
        bool IsDrawable { get; set; }
        float WR { get; set; }
        float HR { get; set; }
        Vector4 Padding { get; set; }
        Vector4 Margin { get; set; }

        int OriginalWidth { get; set; }
        int OriginalHeight { get; set; }

        int CalcWidth { get; set; }
        int CalcHeight { get; set; }


        public void Draw(SpriteBatch spriteBatch);

        public void Update(bool resize);
    }
}

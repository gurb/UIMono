using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIMono.Core.Enums;
using UIMono.Core.Managers;

namespace UIMono.Core.Components
{
    public class Panel: Component
    {
        public PanelType PanelType { get; set; } = PanelType.Vertical;
        public SizeType SizeType { get; set; } = SizeType.Pixel;

        public bool StretchChildren { get; set; } = false;
        public bool IsResponsive { get; set; } = true;
        public Vector2 MinSize { get; set; }
        public Vector2 MaxSize { get; set; }
        
        public Panel(Texture2D Texture2D)
        {
            this.Texture2D = Texture2D;
            this.Size = new Vector2(Texture2D.Width, Texture2D.Height);

            this.RenderTarget2D = new RenderTarget2D(GraphicsManager.GraphicsDevice, (int)this.Size.X, (int)this.Size.Y);
        }

        public Panel(int width, int height)
        {
            Texture2D = TextureManager.GenerateTexture(Color.White, width, height);
            this.Size = new Vector2(width, height);

            this.RenderTarget2D = new RenderTarget2D(GraphicsManager.GraphicsDevice, (int)this.Size.X, (int)this.Size.Y);
        }
    }
}

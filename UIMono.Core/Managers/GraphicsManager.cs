using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMono.Core.Managers
{
    public static class GraphicsManager
    {
        public static GraphicsDevice? GraphicsDevice { get; set; }
        public static SpriteSortMode SpriteSortMode { get; set; } = SpriteSortMode.Deferred;
        public static BlendState BlendState { get; set; } = BlendState.AlphaBlend;
        public static BlendState AlphaBlendState { get; set; } = new BlendState
        {
            AlphaBlendFunction = BlendFunction.Add,
            AlphaSourceBlend = Blend.SourceAlpha,
            AlphaDestinationBlend = Blend.One
        };
    }
}

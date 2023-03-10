using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMono.Core.Managers
{
    public static class TextureManager
    {
        public static Texture2D GenerateTexture(Color color, int width=0, int height=0, float alpha=1f)
        {
            if (GraphicsManager.GraphicsDevice != null)
            {
                Texture2D texture = new Texture2D(GraphicsManager.GraphicsDevice, width, height);

                Color[] pixels = new Color[width * height];
                for (int index = 0; index < pixels.Count(); index++)
                {
                    pixels[index] = new Color(color, alpha);
                }

                texture.SetData(pixels);

                return texture;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}

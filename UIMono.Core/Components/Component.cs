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
        public RenderTarget2D? RenderTarget2D { get; set; }
        public bool HasParent { get; set; }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
            if(Children.Count > 0 && GraphicsManager.GraphicsDevice != null)
            {

                spriteBatch.End();

                GraphicsManager.GraphicsDevice.SetRenderTarget(RenderTarget2D);

                spriteBatch.Begin();


                spriteBatch.Draw(Texture2D, new Vector2(0,0), Color.White);

                foreach (var child in Children)
                {
                    if (child.Texture2D != null)
                    {
                        spriteBatch.Draw(child.Texture2D, child.Position, child.BackgroundColor);
                    }
                }

                spriteBatch.End();


                if (HasParent == false)
                {
                    GraphicsManager.GraphicsDevice.SetRenderTarget(null);
                    spriteBatch.Begin();


                    spriteBatch.Draw(RenderTarget2D, Position, Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(Texture2D, Position, Color.White);
            }
        }

        public virtual void Update()
        {
           
        }

    }
}

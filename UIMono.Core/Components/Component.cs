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


                spriteBatch.Draw(Texture2D, new Vector2(0,0), BackgroundColor);

                foreach (var child in Children)
                {
                    ReDraw(spriteBatch, child, this);
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

        private void ReDraw(SpriteBatch spriteBatch, IComponent childComponent, IComponent parent)
        {
            if (childComponent.Children.Count > 0 && GraphicsManager.GraphicsDevice != null)
            {
                spriteBatch.End();

                GraphicsManager.GraphicsDevice.SetRenderTarget(childComponent.RenderTarget2D);

                spriteBatch.Begin();

                spriteBatch.Draw(childComponent.Texture2D, new Vector2(0, 0), childComponent.BackgroundColor);


                foreach (var child in childComponent.Children)
                {
                    ReDraw(spriteBatch, child, childComponent);
                }

                spriteBatch.End();

                GraphicsManager.GraphicsDevice.SetRenderTarget(parent.RenderTarget2D);
                GraphicsManager.GraphicsDevice.Clear(parent.BackgroundColor);
                spriteBatch.Begin();
                spriteBatch.Draw(childComponent.RenderTarget2D, childComponent.Position, Color.White);

            }
            else
            {
                spriteBatch.Draw(childComponent.Texture2D, childComponent.Position, childComponent.BackgroundColor);
            }
        }

        public virtual void Update()
        {
           
        }

    }
}

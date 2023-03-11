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
        public bool IsDrawable { get; set; }
        public SizeType SizeType { get; set; } = SizeType.Pixel;
        public float Opacity { get; set; } = 1.0f;
        /// <summary>
        /// WR (Width Ratio, 1.0f == 100%, 0.1f == 10%)
        /// </summary>
        public float WR { get; set; } = 1.0f;
        /// <summary>
        /// HR (Height Ratio, 1.0f == 100%, 0.1f == 10%)
        /// </summary>
        public float HR { get; set; } = 1.0f;
        /// <summary>
        /// Padding (x:Top, y:Left, z:Bottom, w:Right)
        /// </summary>
        public Vector4 Padding { get; set; } = new Vector4(0, 0, 0, 0);
        /// <summary>
        /// Margin (x:Top, y:Left, z:Bottom, w:Right)
        /// </summary>
        public Vector4 Margin { get; set; } = new Vector4(0, 0, 0, 0);


        public virtual void GenerateSurface(Texture2D Texture2D)
        {
            this.Texture2D = Texture2D;
            this.Size = new Vector2(Texture2D.Width, Texture2D.Height);

            SetRenderTarget();
        }

        public virtual void GenerateSurface(int width, int height)
        {
            Texture2D = TextureManager.GenerateTexture(Color.White, width, height);
            this.Size = new Vector2(width, height);

            SetRenderTarget();
        }

        public virtual void GenerateSurface(float wr, float hr)
        {
            this.WR = wr;
            this.HR = hr;

            if(wr > 2f || hr > 2f)
            {
                throw new Exception("Avoid over 2 ratio usage");
            } 
            else if(wr < 0f || hr < 0f)
            {
                throw new Exception("The ratio cannot be negative");
            }

            int width = (int)(wr * 800);
            int height = (int)(hr * 480);

            Texture2D = TextureManager.GenerateTexture(Color.White, width, height);
            this.Size = new Vector2(width, height);

            SetRenderTarget();
        }


        public virtual void SetRenderTarget()
        {
            if (IsDrawable)
            {
                this.RenderTarget2D = new RenderTarget2D(GraphicsManager.GraphicsDevice, (int)this.Size.X, (int)this.Size.Y, false, SurfaceFormat.Color, DepthFormat.None);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
            if(Children.Count > 0 && GraphicsManager.GraphicsDevice != null)
            {

                spriteBatch.End();

                GraphicsManager.GraphicsDevice.SetRenderTarget(RenderTarget2D);

                spriteBatch.Begin(GraphicsManager.SpriteSortMode, GraphicsManager.BlendState);


                spriteBatch.Draw(Texture2D, new Vector2(0, 0), BackgroundColor);


                foreach (var child in Children)
                {
                    ReDraw(spriteBatch, child, this);
                }

                spriteBatch.End();


                if (HasParent == false)
                {
                    GraphicsManager.GraphicsDevice.SetRenderTarget(null);
                   
                    spriteBatch.Begin(GraphicsManager.SpriteSortMode, GraphicsManager.BlendState);


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
                spriteBatch.Begin(GraphicsManager.SpriteSortMode, GraphicsManager.BlendState);

                spriteBatch.Draw(childComponent.Texture2D, new Vector2(0, 0), childComponent.BackgroundColor);


                foreach (var child in childComponent.Children)
                {
                    ReDraw(spriteBatch, child, childComponent);
                }

                spriteBatch.End();

                GraphicsManager.GraphicsDevice.SetRenderTarget(parent.RenderTarget2D);
                GraphicsManager.GraphicsDevice.Clear(parent.BackgroundColor * parent.Opacity);
                spriteBatch.Begin(GraphicsManager.SpriteSortMode, GraphicsManager.BlendState);
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

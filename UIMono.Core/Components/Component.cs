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
        public IComponent Parent { get; set; }
        public SpriteBatch Batch { get; set; }
        public List<IComponent> Children { get; set; } = new List<IComponent>();
        public Texture2D? Texture2D { get; set; }
        public RenderTarget2D? RenderTarget2D { get; set; }
        public Viewport Viewport { get; set; }

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



        public int OriginalWidth { get; set; }
        public int OriginalHeight { get; set; }

        public int CalcWidth { get; set; }
        public int CalcHeight { get; set; }

        public virtual void GenerateSurface(Texture2D Texture2D)
        {
            this.Texture2D = Texture2D;
            this.Size = new Vector2(Texture2D.Width, Texture2D.Height);

            SetDimension();
            SetRenderTarget();
        }

        public virtual void GenerateSurface(int width, int height)
        {
            Texture2D = TextureManager.GenerateTexture(Color.White, width, height);
            this.Size = new Vector2(width, height);

            SetDimension();
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

            int width = (int)(wr * GraphicsManager.GraphicsDevice.DisplayMode.Width);
            int height = (int)(hr * GraphicsManager.GraphicsDevice.DisplayMode.Height);

            Texture2D = TextureManager.GenerateTexture(Color.White, width, height);

            

            this.Size = new Vector2(width, height);

            SetDimension();
            SetRenderTarget();
        }

        private void SetDimension() 
        {
            this.OriginalWidth = Texture2D.Width;
            this.OriginalHeight = Texture2D.Height;

            this.CalcWidth = Texture2D.Width;
            this.CalcHeight = Texture2D.Height;
        }

        public virtual void SetRenderTarget()
        {
            if (IsDrawable)
            {

                this.Size = new Vector2(
                    GraphicsManager.GraphicsDeviceManager.PreferredBackBufferWidth * WR, 
                    GraphicsManager.GraphicsDeviceManager.PreferredBackBufferHeight * HR
                );

                this.Viewport = new Viewport((int)Position.X, (int)Position.Y,(int)this.Size.X, (int)this.Size.Y);
                this.Batch = new SpriteBatch(GraphicsManager.GraphicsDevice);
                this.RenderTarget2D = new RenderTarget2D(GraphicsManager.GraphicsDevice, (int)this.Size.X, (int)this.Size.Y, false, SurfaceFormat.Color, DepthFormat.None);
            }
        }

        public virtual void Update(bool resize)
        {
            if (resize && SizeType == SizeType.Percentage)
            {

                this.Size = new Vector2(
                    GraphicsManager.GraphicsDeviceManager.PreferredBackBufferWidth * WR,
                    GraphicsManager.GraphicsDeviceManager.PreferredBackBufferHeight * HR
                );

                CalcWidth = (int) (GraphicsManager.GraphicsDevice.PresentationParameters.BackBufferWidth * WR);
                CalcHeight = (int) (GraphicsManager.GraphicsDevice.PresentationParameters.BackBufferHeight * HR);

                

                this.Viewport = new Viewport((int)Position.X, (int)Position.Y, CalcWidth, CalcHeight);
            }
            else if (resize && SizeType == SizeType.Pixel && HasParent)
            {
                if (Parent.SizeType == SizeType.Percentage)
                {

                    //CalcWidth = 
                }
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            GraphicsManager.GraphicsDevice.Viewport = this.Viewport;


            Batch.Begin(GraphicsManager.SpriteSortMode, GraphicsManager.BlendState);

            Batch.Draw(Texture2D, new Rectangle((int)Position.X, (int)Position.Y, CalcWidth, CalcHeight),
                       new Rectangle(0, 0, OriginalWidth, OriginalHeight), BackgroundColor * Opacity);

            Batch.End();

            GraphicsManager.GraphicsDevice.Viewport = GraphicsManager.DefaultViewport;


            //if (HasParent == false)
            //{
            //    spriteBatch.Begin(GraphicsManager.SpriteSortMode, GraphicsManager.BlendState);



            //}


            //    if (Children.Count > 0 && GraphicsManager.GraphicsDevice != null)
            //{
            //    spriteBatch.End();

            //    GraphicsManager.GraphicsDevice.SetRenderTarget(RenderTarget2D);

            //    spriteBatch.Begin(GraphicsManager.SpriteSortMode, GraphicsManager.BlendState);


            //    //spriteBatch.Draw(Texture2D, new Vector2(0, 0), BackgroundColor);


            //    foreach (var child in Children)
            //    {
            //        ReDraw(spriteBatch, child, this);
            //    }

            //    spriteBatch.End();


            //    if (HasParent == false)
            //    {
            //        GraphicsManager.GraphicsDevice.SetRenderTarget(null);
            //        GraphicsManager.GraphicsDevice.Clear(Color.CornflowerBlue);


            //        spriteBatch.Begin(GraphicsManager.SpriteSortMode, GraphicsManager.BlendState);


            //        //spriteBatch.Draw(RenderTarget2D, Position, Color.White);
            //        spriteBatch.Draw(
            //            RenderTarget2D,
            //            new Rectangle((int)Position.X, (int)Position.Y, CalcWidth, CalcHeight),
            //            new Rectangle(0, 0, OriginalWidth, OriginalHeight),
            //            Color.White
            //        );
            //    }
            //}
            //else
            //{
            //    spriteBatch.Draw(Texture2D, Position, Color.White);
            //}
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

                Vector2 currentPosition = new Vector2(childComponent.Position.X + childComponent.Margin.X, childComponent.Position.Y + childComponent.Margin.Y);
                currentPosition = new Vector2(currentPosition.X + parent.Padding.X, currentPosition.Y + parent.Padding.Y);

                //spriteBatch.Draw(childComponent.RenderTarget2D, currentPosition, Color.White);
                spriteBatch.Draw(
                    childComponent.RenderTarget2D,
                    new Rectangle((int)currentPosition.X, (int)currentPosition.Y, childComponent.CalcWidth, childComponent.CalcHeight),
                    new Rectangle(0,0, childComponent.OriginalWidth, childComponent.OriginalHeight),
                    Color.White
                );

            }
            else
            {
                spriteBatch.Draw(childComponent.Texture2D, childComponent.Position, childComponent.BackgroundColor);
            }
        }
    }
}

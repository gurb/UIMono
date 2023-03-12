using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIMono.Core.Components;
using UIMono.Core.Enums;
using UIMono.Core.Managers;
using UIMono.Core.Models;

namespace UIMono.Core.Caretaker
{
    public class UICaretaker
    {
        string path;

        List<ComponentJson>? jsonComponents;
        List<IComponent> components;
        Dictionary<string, IComponent> componentsWithTag;

        public static string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string projectPath = appDirectory.Substring(0, appDirectory.IndexOf("bin"));

        private int OLD_WINDOW_WIDTH;
        private int OLD_WINDOW_HEIGHT;

        private string DISPLAY_MODE;

        private bool RESIZE;

        public UICaretaker(string path)
        {
            this.path = path;

            this.Init();
        }

        private void Init()
        {
            components = new List<IComponent>();
            componentsWithTag = new Dictionary<string, IComponent>();

            OLD_WINDOW_WIDTH = GraphicsManager.GraphicsDevice.PresentationParameters.BackBufferWidth;
            OLD_WINDOW_HEIGHT = GraphicsManager.GraphicsDevice.PresentationParameters.BackBufferHeight;


            DISPLAY_MODE = GraphicsManager.GraphicsAdapter.CurrentDisplayMode.ToString();
            
            this.ReadFile();
            this.SetUI();
        }

        private void ReadFile()
        {
            var text = File.ReadAllText(projectPath + path);
            jsonComponents = JsonConvert.DeserializeObject<List<ComponentJson>>(text);
        }

        private void SetUI()
        {
            if (jsonComponents == null)
                return;

            foreach (var componentJson in jsonComponents)
            {
                SetComponent(componentJson);
            }
        }

        private void SetChildrenUI(IComponent parent, List<ComponentJson> jsonComponents)
        {
            if (jsonComponents == null)
                return;

            foreach (var componentJson in jsonComponents)
            {
                SetComponent(componentJson, parent);
            }
        }

        private void SetComponent(ComponentJson componentJson, IComponent? parent = null)
        {
            if (componentJson.type == "panel")
            {
                SetPanel(componentJson, parent);
            }
        }

        private void SetPanel(ComponentJson componentJson, IComponent? parent)
        {
            Panel panel = new Panel();

            if(componentJson.sizeType == (int)SizeType.Percentage)
            {
                panel.GenerateSurface(componentJson.size[0], componentJson.size[1]);
                panel.SizeType = SizeType.Percentage;
            } 
            else
            {
                panel.GenerateSurface((int)componentJson.size[0], (int)componentJson.size[1]);
            }

            if(parent != null)
            {
                panel.Parent = parent;
            }

            if(componentJson.padding != null)
            {
                panel.Padding = new Vector4(componentJson.padding[0], componentJson.padding[1], componentJson.padding[2], componentJson.padding[3]); 
            }

            if(componentJson.margin != null)
            {
                panel.Margin = new Vector4(componentJson.margin[0], componentJson.margin[1], componentJson.margin[2], componentJson.margin[3]);
            }

            panel.Position = new Vector2(componentJson.position[0], componentJson.position[1]);

            if (componentJson.backgroundColor != null)
            {
                panel.BackgroundColor = ColorManager.GetColor(componentJson.backgroundColor);
            }
            if (componentJson.opacity != null)
            {
                panel.Opacity = componentJson.opacity.Value;
            }

            if (componentJson.tag != null)
            {
                if(!componentsWithTag.ContainsKey(componentJson.tag))
                {
                    componentsWithTag.Add(componentJson.tag, panel);
                    panel.Tag = componentJson.tag;
                } 
                else
                {
                    throw new Exception("The tag of component must be unique");
                }
            }

            if(parent == null)
            {
                components.Add(panel);
            } 
            else
            {
                panel.HasParent = true;
                parent.Children.Add(panel);
            }


            if (componentJson.children != null)
            {
                SetChildrenUI(panel, componentJson.children);
            }
        }

        public void Update()
        {
            CheckDisplayName();
            CheckDimension();

            foreach (var component in components)
            {
                component.Update(RESIZE);
            } 

            RESIZE = false;
        }


        private void CheckDimension()
        {
            if (OLD_WINDOW_WIDTH != GraphicsManager.GraphicsDevice.PresentationParameters.BackBufferWidth)
            {
                OLD_WINDOW_WIDTH = GraphicsManager.GraphicsDevice.PresentationParameters.BackBufferWidth;

                RESIZE = true;
            }
            if (OLD_WINDOW_HEIGHT != GraphicsManager.GraphicsDevice.PresentationParameters.BackBufferHeight)
            {
                OLD_WINDOW_HEIGHT = GraphicsManager.GraphicsDevice.PresentationParameters.BackBufferHeight;

                RESIZE = true;
            }
        }

        private void CheckDisplayName()
        {
            if (DISPLAY_MODE != GraphicsManager.GraphicsAdapter.CurrentDisplayMode.ToString())
            {
                DISPLAY_MODE = GraphicsManager.GraphicsAdapter.CurrentDisplayMode.ToString();

                DisplayMode displayMode = GraphicsManager.GraphicsAdapter.CurrentDisplayMode;

                GraphicsManager.GraphicsDeviceManager.PreferredBackBufferWidth = displayMode.Width;
                GraphicsManager.GraphicsDeviceManager.PreferredBackBufferHeight = displayMode.Height;
                GraphicsManager.GraphicsDeviceManager.ApplyChanges();
            }
        }


        public void Render(SpriteBatch batch)
        {

            foreach (var component in components)
            {
                if(!component.HasParent)
                {
                    component.Draw(batch);
                }
            }
        }
    }
}

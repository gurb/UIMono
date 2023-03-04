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

        public UICaretaker(string path)
        {
            this.path = path;

            this.Init();
        }

        private void Init()
        {
            components = new List<IComponent>();
            componentsWithTag = new Dictionary<string, IComponent>(); 

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
            Panel panel = new Panel(componentJson.size[0], componentJson.size[1]);

            panel.Position = new Vector2(componentJson.position[0], componentJson.position[1]);

            if (componentJson.backgroundColor != null)
            {
                panel.BackgroundColor = ColorManager.GetColor(componentJson.backgroundColor);
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

        public void Render(SpriteBatch batch)
        {
            batch.Begin();

            foreach(var component in components)
            {
                if(!component.HasParent)
                {
                    component.Draw(batch);
                }
            }

            batch.End();
        }
    }
}

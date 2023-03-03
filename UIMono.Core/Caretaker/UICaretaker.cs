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

        private void SetChildrenUI(List<ComponentJson> jsonComponents)
        {
            if (jsonComponents == null)
                return;

            foreach (var componentJson in jsonComponents)
            {
                SetComponent(componentJson);
            }
        }

        private void SetComponent(ComponentJson componentJson)
        {
            if (componentJson.type == "panel")
            {
                SetPanel(componentJson);
            }
        }

        private void SetPanel(ComponentJson componentJson)
        {
            Panel panel = new Panel(componentJson.width, componentJson.height);

            if (componentJson.backgroundColor != null)
            {
                panel.BackgroundColor = ColorManager.GetColor(componentJson.backgroundColor);
            }

            components.Add(panel);

            if (componentJson.children != null)
            {
                SetChildrenUI(componentJson.children);
            }
        }

        public void Render(SpriteBatch batch)
        {
            batch.Begin();

            foreach(var component in components)
            {
                component.Draw(batch);
            }

            batch.End();
        }


    }
}

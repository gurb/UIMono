using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIMono.Core.Models;

namespace UIMono.Core.Caretaker
{
    public class UICaretaker
    {
        string path;

        object? ui;

        public static string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string projectPath = appDirectory.Substring(0, appDirectory.IndexOf("bin"));

        public UICaretaker(string path)
        {
            this.path = path;

            this.ReadFile();
            this.SetUI();
        }

        private void ReadFile()
        {
            var text = File.ReadAllText(projectPath + path);
            JObject jObject = JObject.Parse(text);
            var test = jObject["component"];
        }

        private void SetUI()
        {

        }

        public void Render(SpriteBatch batch)
        {

        }


    }
}

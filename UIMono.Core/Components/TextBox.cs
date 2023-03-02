using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMono.Core.Components
{
    public class TextBox: Component
    {
        public string Text { get; set; } = string.Empty;
        public Color TextColor { get; set; } = Color.Black;
    }
}

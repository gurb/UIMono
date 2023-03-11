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
    public class Panel: Component
    {
        public PanelType PanelType { get; set; } = PanelType.Vertical;

        public bool StretchChildren { get; set; } = false;
        public bool IsResponsive { get; set; } = true;
        public Vector2 MinSize { get; set; }
        public Vector2 MaxSize { get; set; }
        
        public Panel()
        {
            IsDrawable = true;
        }

    }
}

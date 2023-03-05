using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMono.Core.Models
{

    public class ComponentJson
    {
        public string? type { get; set; }
        public string? tag { get; set; }
        public int? sizeType { get; set; }
        public int? panelType { get; set; }
        public string? backgroundColor { get; set; }
        public float? opacity { get; set; }
        public List<int> position { get; set; }
        public List<int> size { get; set; }
        public List<ComponentJson>? children { get; set; }

    }
}

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
        public int? sizeType { get; set; }
        public int? panelType { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public List<ComponentJson>? children { get; set; }

    }
}

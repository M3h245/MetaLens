using System;
using System.Collections.Generic;
using System.Text;

namespace MetaLens.Models
{
    public class MetadataItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }

        public bool IsRemovable { get; set; } = true;
    }
}

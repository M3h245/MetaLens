using System;
using System.Collections.Generic;
using System.Text;

namespace MetaLens.Models;

public class MetadataCategory
{
    public string Name { get; set; }
    public List<MetadataItem> Items { get; set; } = new();
}
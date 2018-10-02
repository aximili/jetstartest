using System;
using System.Collections.Generic;
using System.Text;

namespace JetStarTest
{
    public class ConsoleAppConfig
    {
        public FieldConfig Field { get; set; }
        public bool Verbose { get; set; }
    }

    public class FieldConfig {
        public string Type { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
    }
}

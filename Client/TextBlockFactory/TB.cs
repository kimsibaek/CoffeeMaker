﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoffeeMaker_Client.TextBlockFactory
{
    public interface Test
    {
        TextBlock textBlock { get; set; }
    }

    public class TB : TextBlock
    {
        public string Menu { get; set; }
        public int Price { get; set; }
    }
}

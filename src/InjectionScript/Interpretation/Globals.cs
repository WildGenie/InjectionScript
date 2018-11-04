﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionScript.Interpretation
{
    public class Globals
    {
        private readonly Dictionary<string, string> globals = new Dictionary<string, string>();

        public void SetGlobal(string name, string value)
        {
            globals.Add(name, value);
        }

        public string GetGlobal(string name)
        {
            if (globals.TryGetValue(name, out string value))
                return value;
            else
                return "N/A";
        }
    }
}
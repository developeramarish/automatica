﻿using System;
using System.Collections.Generic;
using System.Text;

namespace P3.Knx.Core.DPT.Dpt9
{
    internal class Dpt9028TypeTranslator : Dpt9Translator
    {
        public override string[] Ids => new[] { "9.028" };

        public override decimal ValidateMinMax(decimal value)
        {
            if (value < 0)
            {
                return 0;
            }
            if (value > 670760.96m)
            {
                return 670760.96m;
            }
            return value;
        }
    }
}

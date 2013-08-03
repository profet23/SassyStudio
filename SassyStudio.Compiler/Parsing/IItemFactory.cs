﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassyStudio.Compiler.Parsing
{
    public interface IItemFactory
    {
        ParseItem Create<T>(ComplexItem parent, ITextProvider text, ITokenStream stream) where T : ParseItem;
        T CreateSpecific<T>(ComplexItem parent, ITextProvider text, ITokenStream stream) where T : ParseItem;
        bool TryCreate(ComplexItem parent, ITextProvider text, ITokenStream stream, out ParseItem item);
    }
}

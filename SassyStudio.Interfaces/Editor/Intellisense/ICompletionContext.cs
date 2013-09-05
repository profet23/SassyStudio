﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SassyStudio.Compiler.Parsing;

namespace SassyStudio.Editor.Intellisense
{
    public interface ICompletionContext
    {
        IIntellisenseCache Cache { get; }
        ParseItem Current { get; }
        int Position { get; }
    }
}
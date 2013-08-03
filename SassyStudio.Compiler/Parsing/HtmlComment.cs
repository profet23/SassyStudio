﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassyStudio.Compiler.Parsing
{
    class HtmlComment : Comment
    {
        public override bool Parse(IItemFactory itemFactory, ITextProvider text, ITokenStream stream)
        {
            if (stream.Current.Type == TokenType.OpenHtmlComment)
            {
                OpenComment = Children.AddCurrentAndAdvance(stream, ClassifierType);

                if (stream.Current.Type == TokenType.CommentText)
                    CommentText = Children.AddCurrentAndAdvance(stream, ClassifierType);

                if (stream.Current.Type == TokenType.CloseHtmlComment)
                    CloseComment = Children.AddCurrentAndAdvance(stream, ClassifierType);
            }

            return Children.Count > 0;
        }
    }
}

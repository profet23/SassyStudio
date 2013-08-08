﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassyStudio.Compiler.Parsing
{
    public class ForLoopDirective : ControlDirective
    {
        public VariableName Variable { get; protected set; }
        public TokenItem FromKeyword { get; protected set; }
        public TokenItem ToKeyword { get; protected set; }
        public TokenItem ThroughKeyword { get; protected set; }

        public override bool Parse(IItemFactory itemFactory, ITextProvider text, ITokenStream stream)
        {
            if (AtRule.IsRule(text, stream, "for"))
            {
                ParseRule(itemFactory, text, stream);

                var variable = new VariableName(SassClassifierType.VariableDefinition);
                if (variable.Parse(itemFactory, text, stream))
                {
                    Variable = variable;
                    Children.Add(Variable);
                }

                while (!IsForStatementTerminator(stream.Current.Type))
                {
                    if (stream.Current.Type == TokenType.Identifier)
                    {
                        if (text.CompareOrdinal(stream.Current.Start, "to"))
                        {
                            ToKeyword = Children.AddCurrentAndAdvance(stream, SassClassifierType.Keyword);
                            continue;
                        }

                        if (text.CompareOrdinal(stream.Current.Start, "from"))
                        {
                            FromKeyword = Children.AddCurrentAndAdvance(stream, SassClassifierType.Keyword);
                            continue;
                        }

                        if (text.CompareOrdinal(stream.Current.Start, "through"))
                        {
                            ThroughKeyword = Children.AddCurrentAndAdvance(stream, SassClassifierType.Keyword);
                            continue;
                        }
                    }

                    ParseItem item;
                    if (itemFactory.TryCreateParsedOrDefault(this, text, stream, out item))
                        Children.Add(item);
                }

                ParseBody(itemFactory, text, stream);
            }

            return Children.Count > 0;
        }

        public override IEnumerable<VariableName> GetDefinedVariables(int position)
        {
            var variables = base.GetDefinedVariables(position);
            if (Body != null && position > Body.Start)
                variables = variables.Concat(new[] { Variable });

            return variables;
        }

        static bool IsForStatementTerminator(TokenType type)
        {
            switch (type)
            {
                case TokenType.EndOfFile:
                case TokenType.OpenCurlyBrace:
                    return true;
                default:
                    return false;
            }
        }
    }
}

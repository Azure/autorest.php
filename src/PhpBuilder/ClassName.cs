﻿using AutoRest.Php.PhpBuilder.Expressions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AutoRest.Php.PhpBuilder
{
    public sealed class ClassName : Name
    {
        public ImmutableList<string> Names { get; }

        public ImmutableList<string> PhpNames { get; }

        public string PhpNamespace { get; }

        public string PhpLocalName { get; }

        public string AbsoluteName { get; }

        public string FileName { get; }

        public ClassName(string value) : base(value)
        {
            Names = Original.Split(new[] { '.', '\\' }).ToImmutableList();
            PhpNames = Names.Select(Extensions.GetPhpName).ToImmutableList();
            PhpNamespace = string.Join("\\", PhpNames.Take(PhpNames.Count - 1));
            PhpLocalName = PhpNames[PhpNames.Count - 1];
            AbsoluteName = "\\" + string.Join("\\", PhpNames);
            FileName = string.Join("/", PhpNames) + ".php";
        }

        public New New(IEnumerable<Expression> parameters) 
            => new New(this, parameters);

        public New New(params Expression[] parameters)
            => new New(this, parameters);

        public StaticCall StaticCall(
            FunctionName function, params Expression[] parameters)
            => new StaticCall(this, function, parameters);

        public StaticCall StaticCall(
            string function, params Expression[] parameters)
            => new StaticCall(this, new FunctionName(function), parameters);
    }
}

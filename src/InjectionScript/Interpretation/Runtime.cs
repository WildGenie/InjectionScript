﻿using InjectionScript.Parsing;
using InjectionScript.Parsing.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionScript.Interpretation
{
    public class Runtime
    {
        public Metadata Metadata { get; } = new Metadata();
        public Interpreter Interpreter { get; }
        public Globals Globals { get; } = new Globals();
        public Objects Objects { get; } = new Objects();
        public string CurrentFileName { get; private set; }

        public Runtime()
        {
            Interpreter = new Interpreter(Metadata);
            RegisterNatives();
        }

        private void RegisterNatives()
        {
            Metadata.Add(new NativeSubrutineDefinition("UO", "SetGlobal", (Action<string, string>)Globals.SetGlobal));
            Metadata.Add(new NativeSubrutineDefinition("UO", "GetGlobal", (Func<string, string>)Globals.GetGlobal));
        }

        public void Load(string fileName)
        {
            var parser = new Parser();
            var errorListener = new MemorySyntaxErrorListener();
            parser.AddErrorListener(errorListener);
            var file = parser.ParseFile(File.ReadAllText(fileName));
            CurrentFileName = fileName;

            if (errorListener.Errors.Any())
            {
                throw new SyntaxErrorException(fileName, errorListener.Errors);
            }

            Metadata.ResetSubrutines();
            var collector = new DefinitionCollector(Metadata);
            collector.Visit(file);
        }

        public void Load(injectionParser.FileContext file)
        {
            var collector = new DefinitionCollector(Metadata);
            collector.Visit(file);
        }

        public InjectionValue CallSubrutine(string name, params string[] arguments)
        {
            if (Metadata.TryGetSubrutine(name, arguments.Length, out var subrutine))
            {
                return Interpreter.CallSubrutine(subrutine.Subrutine, 
                    arguments.Select(x => new InjectionValue(x)).ToArray());
            }
            else
                throw new NotImplementedException();
        }

        public int GetObject(string id)
        {
            if (Objects.TryGet(id, out int value))
                return value;

            return NumberConversions.Str2Int(id);
        }

        public void SetObject(string name, int value) => Objects.Set(name, value);
    }
}

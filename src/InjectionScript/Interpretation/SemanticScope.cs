﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectionScript.Interpretation
{
    public class SemanticScope
    {
        private class Scope
        {
            public Dictionary<string, InjectionValue> vars = new Dictionary<string, InjectionValue>();
            public Dictionary<string, InjectionValue[]> dims = new Dictionary<string, InjectionValue[]>();
        }

        private readonly Stack<Scope> scopes = new Stack<Scope>();

        public void Start() => scopes.Push(new Scope());
        public void End() => scopes.Pop();

        public void SetVar(string name, InjectionValue value)
        {
            var vars = scopes.Peek().vars;
            if (vars.ContainsKey(name))
                vars[name] = value;
            else if (scopes.Peek().dims.ContainsKey(name))
            {
                scopes.Peek().dims.Remove(name);
                vars[name] = value;
            }
            else
                throw new NotImplementedException();
        }
        public InjectionValue GetVar(string name) => scopes.Peek().vars[name];

        internal void DefineVar(string name)
        {
            var vars = scopes.Peek().vars;
            vars[name] = InjectionValue.Unit;
        }

        internal void DefineVar(string name, InjectionValue value)
        {
            var vars = scopes.Peek().vars;
            vars[name] = value;
        }

        public void SetDim(string name, int index, InjectionValue value)
        {
            var dims = scopes.Peek().dims;
            if (dims.ContainsKey(name))
                dims[name][index] = value;
            else
                throw new NotImplementedException();
        }
        public InjectionValue GetDim(string name, int index) => scopes.Peek().dims[name][index];

        internal void DefineDim(string name, int limit)
        {
            var dims = scopes.Peek().dims;
            dims[name] = new InjectionValue[limit + 1];
        }
    }
}

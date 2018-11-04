﻿using System.Collections.Generic;

namespace InjectionScript.Interpretation
{
    public class Metadata
    {
        private readonly Dictionary<string, SubrutineDefinition> subrutines
            = new Dictionary<string, SubrutineDefinition>();
        private readonly Dictionary<string, NativeSubrutineDefinition> nativeSubrutines
            = new Dictionary<string, NativeSubrutineDefinition>();

        public void Add(SubrutineDefinition subrutineDef) => subrutines.Add(GetSubrutineKey(subrutineDef), subrutineDef);
        public void Add(NativeSubrutineDefinition subrutineDef)
            => nativeSubrutines.Add(GetNativeSubrutineKey(subrutineDef), subrutineDef);

        public bool TryGetSubrutine(string name, int argumentCount, out SubrutineDefinition definition)
            => subrutines.TryGetValue(GetSubrutineKey(name, argumentCount), out definition);
        public SubrutineDefinition GetSubrutine(string name, int argumentCount)
            => subrutines[GetSubrutineKey(name, argumentCount)];

        private string GetSubrutineKey(SubrutineDefinition definition)
        {
            var paramCount = definition.Subrutine.parameters()?.parameterName()?.Length ?? 0;

            return GetSubrutineKey(definition.Name, paramCount);
        }

        private string GetSubrutineKey(string name, int paramCount)
            => $"{name}'`'{paramCount}";

        public NativeSubrutineDefinition GetNativeSubrutine(string ns, string name)
        {
            var key = string.IsNullOrEmpty(ns) ? name : $"{ns}.{name}";
            if (nativeSubrutines.TryGetValue(key, out var value))
                return value;

            return null;
        }

        private string GetNativeSubrutineKey(NativeSubrutineDefinition subrutineDef)
            => string.IsNullOrEmpty(subrutineDef.NameSpace)
                ? subrutineDef.Name
                : $"{subrutineDef.NameSpace}.{subrutineDef.Name}";
    }
}

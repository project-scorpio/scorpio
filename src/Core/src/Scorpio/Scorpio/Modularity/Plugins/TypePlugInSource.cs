﻿using System;

namespace Scorpio.Modularity.Plugins
{
    internal class TypePlugInSource : IPlugInSource
    {
        private readonly Type[] _moduleTypes;

        public TypePlugInSource(params Type[] moduleTypes) => _moduleTypes = moduleTypes ?? new Type[0];

        public Type[] GetModules() => _moduleTypes;
    }
}

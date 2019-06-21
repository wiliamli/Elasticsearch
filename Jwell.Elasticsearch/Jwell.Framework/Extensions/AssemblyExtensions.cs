﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jwell.Framework.Extensions
{
    public static class AssemblyExtensions
    {
        public static List<Type> GetInheritedTypes(this Assembly assembly, Type baseType)
        {
            return assembly.GetTypes()
                .Where(x => x.BaseType != null && x.BaseType.GenericEq(baseType))
                .ToList();
        }
    }
}

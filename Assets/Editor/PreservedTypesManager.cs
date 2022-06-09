//using System;
//using System.Linq;
//using System.Reflection;

//class PreserveTypeInfo
//{
//    public Type Type { get; set; }
//    public bool IncludeWhenUsed { get; set; }
//}

//interface IPreserveTypeProvider
//{
//    PreserveTypeInfo[] GetPreservedTypes();
//}

//class IncludeAssemblyTypes : IPreserveTypeProvider
//{
//    public Type[] IncludeTypes { get; set; }
//    public Type[] IncludeWhenUsedTypes { get; set; }

//    public PreserveTypeInfo[] GetPreservedTypes()
//    {
//        return IncludeTypes.Select(t => new PreserveTypeInfo() { Type = t })
//            .Concat(IncludeWhenUseTypes.Select(t => new PreserveTypeInfo() { Type = t, IncludeWhenUse = true })).ToList();
//    }
//}

//class ExcludeAssemblyTypes : IPreserveTypeProvider
//{
//    public Assembly Assembly { get; set; }
//    public Type[] ExcludeTypes { get; set; }

//    public PreserveTypeInfo[] GetPreservedTypes()
//    {
//        var excludeTypes = ExcludeTypes.ToHashSet();
//        return Assembly.Types.Where(t => !excludeTypes.Contains(t)).Select(t => new PreserveTypeInfo() { Type = t }).ToList();
//    }
//}

//[PreserveTypeProviderManager]
//class PreservedTypesManager
//{
//    public IEnumerable<PreserveTypeInfo> GetPreserveTypes()
//    {
//        foreach (var provider in GetPreserveTypesProviders())
//        {
//            foreach (var type in provider.GetPreserveTypes())
//            {
//                yield return type;
//            }
//        }
//    }

//    public IEnumerable<IPreserveTypeProvider> GetPreserveTypesProviders()
//    {
//        yield return new IncludeAssemblyTypes()
//        {
//            IncludeTypes = new Types[]
//            {
//                // ...
//            },
//        };
//        yield return new IncludeAssemblyTypes()
//        {
//            IncludeTypes = new Types[]
//            {
//                // ...
//            },
//        };
//        yield return new ExcludeAssemblyTypes()
//        {
//            Assembly = typeof(HotfixType1).Assembly,
//            ExcludeTypes = new Types[]
//            {
//                // ...
//            },
//        };
//    }
//}
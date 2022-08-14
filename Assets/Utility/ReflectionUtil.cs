using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NoodleEater.Utility 
{
    public static class ReflectionUtil
    {
		public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
		{
			List<T> objects = new List<T>();
			foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes()
				.Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
				{
					objects.Add((T)Activator.CreateInstance(type, constructorArgs));
				}
			return objects;
		}

        public static IEnumerable<FieldInfo> GetFieldWithAttribute<T>() where T : Attribute
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsClass)
                    {
                        foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (fieldInfo.GetCustomAttributes(typeof(T), false).Length > 0)
                            {
                                yield return fieldInfo;
                            }
                        }
                    }
                }
            }
        }
    }
}

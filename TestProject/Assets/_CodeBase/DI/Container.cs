using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace DI
{
    public static class Container
    {
        private static readonly Dictionary<Type, Object> RegDictionary = new ();

        public static T Get<T>()
        {
            var type = typeof(T);
            try
            {
                return (T)RegDictionary[type];
            }
            catch
            {
                throw new Exception($"{type} type is not registered");
            }
        }

        public static void Register<T>(T instance)
        {
            var t = typeof(T);
            if (RegDictionary.ContainsKey(t))
                RegDictionary[t] = instance;
            else
                RegDictionary.Add(t, instance);
        }
    }
}
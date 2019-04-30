using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;

namespace Base
{
    public abstract class BaseSingleton<T> where T:BaseSingleton<T>
    {
        private static T g_Singleton;
        public static T Singleton
        {
            get
            {
                if(g_Singleton == null)
                {
                    Type type = typeof(T);
                    g_Singleton = (T)Activator.CreateInstance(type);
                }
                return g_Singleton;
            }
        }
        public BaseSingleton()
        {
            g_Singleton = this as T;
        }
    }
}



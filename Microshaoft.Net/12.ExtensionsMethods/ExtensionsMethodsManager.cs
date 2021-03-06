﻿namespace Microshaoft
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    

    public static partial class ExtensionsMethodsManager
    {
        public static void ForEach<TKey, TValue>
                                (
                                    this ConcurrentDictionary<TKey, TValue> concurrentDictionary
                                    , Func<TKey, TValue, bool> processFunc
                                )
        {
            foreach (KeyValuePair<TKey, TValue> kvp in concurrentDictionary)
            {
                bool r = processFunc(kvp.Key, kvp.Value);
                if (r)
                {
                    break;
                }
            }
        }


      
    }
}

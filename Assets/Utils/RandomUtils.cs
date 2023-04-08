using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class RandomUtils
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.ToList();
            return list[Random.Range(0, list.Count)];
        }
    }
}
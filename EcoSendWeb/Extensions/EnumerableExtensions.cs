using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoSendWeb.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Nz<T>(this IEnumerable<T> enumeration)
        {
            return enumeration ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumeration, T firstElement)
        {
            yield return firstElement;
            foreach (T e in enumeration)
            {
                yield return e;
            }
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumeration, T lastElement)
        {
            foreach (T e in enumeration)
            {
                yield return e;
            }

            yield return lastElement;
        }

        public static int IndexOf<T>(this IEnumerable<T> enumeration, Func<T, bool> predicate, int fromIndex = 0)
        {
            if (fromIndex > 0)
            {
                enumeration = enumeration.Skip(fromIndex);
            }

            foreach (T element in enumeration)
            {
                if (predicate(element))
                {
                    return fromIndex;
                }
                ++fromIndex;
            }

            return -1;
        }

        public static IEnumerable<T> FlattenBreadthFirst<T>(this IEnumerable<T> enumeration, Func<T, IEnumerable<T>> children)
        {
            LinkedList<T> lstToGo = new LinkedList<T>(enumeration);
            while (lstToGo.Count > 0)
            {
                T current = lstToGo.First.Value;
                yield return current;
                foreach (T ch in children(current))
                {
                    lstToGo.AddLast(ch);
                }
                lstToGo.RemoveFirst();
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(this T obj)
        {
            yield return obj;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static bool IsOneOf<T>(this T self, params T[] items)
        {
            return items.Contains(self);
        }

        public static bool IsNotOneOf<T>(this T self, params T[] items)
        {
            return !items.Contains(self);
        }

        public static T AddTo<T>(this T self, ICollection<T> collection)
        {
            collection.Add(self);
            return self;
        }
    }
}
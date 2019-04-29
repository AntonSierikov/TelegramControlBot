using System;
using System.Collections.Generic;
using System.Text;

namespace ControlBot.BL.Extensions
{
    public static class IEnumeratorExtension
    {
        public delegate bool Try<T, TResult>(T item, out TResult result);

        //----------------------------------------------------------------//

        public static T GetNext<T>(this IEnumerator<T> enumerator) where T : class
        {
            return enumerator.MoveNext() ? enumerator.Current : null;
        }

        //----------------------------------------------------------------//
    
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) where T: class
        {
            foreach(T item in enumerable)
            {
                action(item);
            }
        }

        //----------------------------------------------------------------//

        public static List<TResult> AddRange<T, TResult>(this IEnumerable<T> enumerable, Try<T, TResult> @try, out List<T> notAdded)
        {
            List<TResult> resultList = new List<TResult>();
            notAdded = new List<T>();

            foreach(T item in enumerable)
            {
                if(@try(item, out TResult result))
                {
                    resultList.Add(result);
                }
                else
                {
                    notAdded.Add(item);
                }
            }

            return resultList;
        }
    }
}

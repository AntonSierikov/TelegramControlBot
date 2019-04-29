using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlBot.Core.Extensions
{
    public static class CollectionExtension
    {

        //----------------------------------------------------------------//

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        //----------------------------------------------------------------//

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
        {
            foreach (T item in enumerable)
            {
                await action(item);
            }
        }
    }
}

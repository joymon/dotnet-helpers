using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Helpers.Linq
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Chunk the sequence based on chunkSize and returns sequence of chunks.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<TResult>> Chunk<TResult>(this IEnumerable<TResult> source, int chunkSize)
        {
            return source.
                Select((thread, index) => new { Index = index, Value = thread }).
                GroupBy(tuple => tuple.Index / chunkSize).
                Select(group => group.Select(tuple => tuple.Value));
        }
        /// <summary>
        /// Randomize the sequence
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> Randomize<TResult>(this IEnumerable<TResult> source)
        {
            return source.
                Select((sourceItem, index) => new
                {
                    Item = sourceItem,
                    Id = Guid.NewGuid()
                }).
            OrderBy(t1 => t1.Id).Select(t1 => t1.Item);
        }
    }
}

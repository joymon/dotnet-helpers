using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Helpers.Linq
{
    /// <summary>
    /// Contains extension methods for <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Chunk the sequence based on chunkSize and returns sequence of chunks.
        /// </summary>
        /// <typeparam name="TResult">Type of source sequence.</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="chunkSize">Size of chunk</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// IEnumerable<int> input = new List<int>() { 1, 2, 3, 4 };
        /// int sumofFirstChunk = input.Chunk(2).First().Sum();
        /// ]]>
        /// </code>
        /// </example>
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
        /// <param name="source">The source of sequence</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// IEnumerable<int> input = new List<int>() { 1, 2, 3, 4 };
        /// IEnumerable<int> ramdomizedseQuence = input.Randomize();
        /// ]]>
        /// </code>
        /// </example>
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

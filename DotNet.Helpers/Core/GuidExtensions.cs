using System;
using System.Text;

namespace DotNet.Helpers.Core
{
    /// <summary>
    /// Extensions on <see cref="Guid"/> class.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Converts to shortstring having 22 characters
        /// </summary>
        /// <param name="id">The Guid to convert.</param>
        /// <returns>Short <see cref="string"/> id.</returns>
        public static string ToShortString(this Guid id)
        {
            return Convert.ToBase64String(id.ToByteArray())
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .Replace("=", "");
        }
    }
}

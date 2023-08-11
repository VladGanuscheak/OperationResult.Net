using System;

namespace OperationResult.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     Verifies if the specified type is included in the collection of types.
        /// </summary>
        /// <param name="type">The current type.</param>
        /// <param name="types">The collection of types.</param>
        /// <returns></returns>
        public static bool IsIn(this Type type, params Type[] types)
        {
            var isContained = false;

            for (var index = 0; index < types.Length; index++)
            {
                isContained |= types[index].Name == type.Name;
                if (isContained)
                {
                    break;
                }
            }

            return isContained;
        }
    }
}

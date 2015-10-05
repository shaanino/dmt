using System.Reflection;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace NeXT.DMT.Utilities
{
    public static class CommonFunction
    {
        /// <summary>
        /// Return string with all parameters values.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in obj.GetType().GetMembers())
            {
                if (item.MemberType == MemberTypes.Property)
                {
                    sb.AppendFormat("{0}: {1}, ", item.Name, obj.GetType().InvokeMember
                        (item.Name, BindingFlags.GetProperty, null, obj, null));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert to collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inCol">The in col.</param>
        /// <returns></returns>
        public static Collection<T> ToCollection<T>(this IEnumerable<T> inCol)
        {
            if (inCol == null)
                return null;
            Collection<T> outCol = new Collection<T>();
            inCol.AsEnumerable().ToList().ForEach(outCol.Add);
            return outCol;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Absentee.WebApi.Tools
{
    public static class Extensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
            {
                list.Add(item);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookmarks.Domain.Entities;

namespace Bookmarks.Domain.Services
{
    public static class BookmarkHelpers
    {
        public static string ToCommaSeparatedString(this List<Tag> tags)
        {
            string result = string.Empty;

            foreach (var tag in tags)
            {
                result += tag.Name + ",";
            }

            result = result.TrimEnd(' ', ',');
            return result;
        }

        public static List<string> ToTagList(this string commaSeparatedList)
        {
            return commaSeparatedList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).Distinct().ToList();
        }
    }
}

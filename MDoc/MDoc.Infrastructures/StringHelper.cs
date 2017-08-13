using System.Text.RegularExpressions;

namespace MDoc.Infrastructures
{
    public static class StringHelper
    {
        public static bool IsEmail(this string s)
        {
            const string expression = "^[a-zA-Z]+(([\\'\\,\\.\\- ][a-zA-Z ])?[a-zA-Z]*)*\\s+<(\\w[-._\\w]*\\w@\\w[-._\\w]*\\w\\.\\w{2,3})>$|^(\\w[-._\\w]*\\w@\\w[-._\\w]*\\w\\.\\w{2,3})";
            // ^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$
            return Regex.IsMatch(s, expression);
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderRenamer
{

    public static class MyExtensions
    {
        public static string Right(this string sValue, int iMaxLength)
        {
            //Check if the value is valid
            if (string.IsNullOrEmpty(sValue))
            {
                //Set valid empty string as string could be null
                sValue = string.Empty;
            }
            else if (sValue.Length > iMaxLength)
            {
                //Make the string no longer than the max length
                sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
            }

            //Return the string
            return sValue;
        }
        public static string FileNameClean(this string sValue)
        {

            sValue = sValue.Replace(":", " - ");
            sValue = sValue.Replace("|", " - ");
            sValue = sValue.Replace("+", " - ");
            sValue = sValue.Replace("\"", "");

            //Return the string
            return sValue;
        }

    }

    public static class PropGridExExtensions
    {
        public static void SetLabelColumnWidth(this PropertyGridEx.PropertyGridEx grid, int width)
        {
            FieldInfo fi = grid?.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic);
            Control view = fi?.GetValue(grid) as Control;
            MethodInfo mi = view?.GetType().GetMethod("MoveSplitterTo", BindingFlags.Instance | BindingFlags.NonPublic);
            mi?.Invoke(view, new object[] { width });
        }
    }

    public class MyContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(p => base.CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                   .Select(f => base.CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }
    }


        //Extension methods must be defined in a static class
    public static class StringExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static string TrimAndReduce(this string str)
        {
            return ConvertWhitespacesToSingleSpaces(str).Trim();
        }

        public static string ConvertWhitespacesToSingleSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        public static List<string> GetYearsFromString(this string Value)
        {

            List<string> result = new List<string>();
            string temp = Value;
            // remove yars from string
            for (int i = 1800; i < DateTime.Now.Year + 3; i++)
            {
                string temp1 = temp.Replace(i.ToString(), " ");
                if (temp != temp1)
                {
                    result.Add(i.ToString());
                }
            }

            return result;
        }

        public static string RemoveStartEndChar(this string Word, String Char)
        {

            String CleanedWord = Word;

            bool Do = true;
            while (Do)
            {
                Do = false;
                CleanedWord = CleanedWord.TrimStart().TrimEnd();
                if (CleanedWord.StartsWith(Char))
                {
                    CleanedWord = CleanedWord.Substring(1, CleanedWord.Length - 1);
                    Do = true;
                }
            }

            Do = true;
            while (Do)
            {
                Do = false;
                CleanedWord = CleanedWord.TrimStart().TrimEnd();
                if (CleanedWord.EndsWith(Char))
                {
                    CleanedWord = CleanedWord.Substring(0, CleanedWord.Length - 1);
                    Do = true;
                }
            }

            CleanedWord = CleanedWord.TrimStart().TrimEnd();

            return CleanedWord;

        }

        public static string RemoveWildcharFromItem(this string Word)
        {
            string result = Word;

            result = Word.RemoveStartEndChar("*"); // RemoveStartEndChar(DictItem, "*");

            return result;
        }

        public static string RemoveWords(this string ToClean, string DictItem)
        {
            String result = ToClean;

            String tempResult = "";
            while (tempResult != result)
            {
                tempResult = result;
                String cItem = DictItem.RemoveWildcharFromItem();
                if (cItem != null && cItem != "")
                {
                    result = " " + result + " ";
                    result = result.Replace(" " + cItem + " ", " ");
                    String ItemAux = cItem.Replace(".", " ");
                    result = " " + result.TrimStart().TrimEnd() + " ";
                    result = result.Replace(" " + ItemAux + " ", " ");

                    // if has wildchar
                    if (DictItem != cItem)
                    {
                        String lDelimiter = " ";
                        String rDelimiter = " ";
                        if (DictItem.StartsWith("*"))
                        {
                            lDelimiter = "";
                        }
                        if (DictItem.EndsWith("*"))
                        {
                            rDelimiter = "";
                        }
                        result = result.TrimStart().TrimEnd();
                        result = " " + result + " ";
                        result = result.Replace(lDelimiter + cItem + rDelimiter, " ");
                        ItemAux = cItem.Replace(".", " ");
                        result = " " + result.TrimStart().TrimEnd() + " ";
                        result = result.Replace(lDelimiter + ItemAux + rDelimiter, " ");
                    }

                }
            }
            return result;
        }

        public static string RemoveSpecialChars(this string ToClean)
        {
            string CleanedString = ToClean;
            CleanedString = CleanedString.Replace(".", " ")
                                    .Replace(".", " ")
                                    .Replace("_", " ")
                                    .Replace("[", " ")
                                    .Replace("]", " ")
                                    .Replace("  ", " ")
                                    .TrimStart()
                                    .TrimEnd();

            CleanedString = CleanedString.RemoveStartEndChar("-");
            CleanedString = CleanedString.RemoveStartEndChar("_");
            CleanedString = CleanedString.RemoveStartEndChar(".");
            CleanedString = CleanedString.RemoveStartEndChar("[");
            CleanedString = CleanedString.RemoveStartEndChar("]");
            CleanedString = CleanedString.RemoveStartEndChar("^");
            CleanedString = CleanedString.RemoveStartEndChar(".");

            CleanedString = CleanedString.Replace("--", "-")
                           .Replace("_", " ")
                           .Replace("  ", " ")
                           .Replace("..", ".")
                           .TrimStart()
                           .TrimEnd();

            CleanedString = CleanedString.Replace(".", " ")
                           .Replace(".", " ")
                           .Replace("_", " ")
                           .Replace("[", " ")
                           .Replace("]", " ")
                           .Replace("  ", " ")
                           .TrimStart()
                           .TrimEnd();

            CleanedString = CleanedString.Replace("--", "-")
                           .Replace("_", " ")
                           .Replace("  ", " ")
                           .Replace("..", ".")
                           .TrimStart()
                           .TrimEnd();

            return CleanedString;
        }

        public static string RemoveFromList(this string ToClean, List<string> data, bool RemoveWildchars = false)
        {
            string CleanedString = ToClean;

            foreach (var DictItem in data)
            {
                if (RemoveWildchars)
                {
                    var Dict = DictItem.RemoveWildcharFromItem();
                    if (Dict != null && Dict != "")
                    {
                        CleanedString = CleanedString.RemoveWords(Dict);
                    }
                }
                else
                {
                    CleanedString = CleanedString.RemoveWords(DictItem);
                }
            }

            return CleanedString;
        }

    }
}

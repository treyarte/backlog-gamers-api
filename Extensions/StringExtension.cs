using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using backlog_gamers_api.Helpers;

namespace backlog_gamers_api.Extensions;

/// <summary>
/// Helper methods for strings
/// </summary>
public static class StringExtension
{
     /// <summary>Parses a string to the specified enum value</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static T EnumByDescription<T>(this string desc) where T : Enum
        {
            Type type = typeof(T);

            try
            {
                foreach(string field in Enum.GetNames(type))
                {
                    MemberInfo[] infos = type.GetMember(field);

                    foreach (MemberInfo info in infos)
                    {
                        DescriptionAttribute attr = info.GetCustomAttribute<DescriptionAttribute>(false);

                        if(attr.Description.Equals(desc, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return field.ToEnum<T>(true);
                        }
                    }
                }

            }
             
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return default(T);
        }

        public static string AddCommasToNumberString(this string numberText)
        {
            string pattern = @"\B(?<!\.\d*)(?=(\d{3})+(?!\d))";
            string replacement = @"$0,";
            
            //TODO check if number if not throw error

            try
            {
                string results = Regex.Replace(numberText, pattern, replacement);
                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw ;
            }
        }

        /// <summary>Tries to parse a string to the specified enum value</summary>
        public static bool TryParseToEnum<T>(this string name, out T value, bool ignoreCase = true)
        {
            try
            {
                value = name.ToEnum<T>(ignoreCase);
                return true;
            }
            catch (ArgumentException)
            {
                value = default(T);
                return false;
            }
        }
        
        /// <summary>
        /// Capitalize the first letter in text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isMultiWord"></param>
        /// <returns></returns>
        public static string CapitalizeText(this string text, bool isMultiWord = false) {
            if (String.IsNullOrWhiteSpace(text))
            {
                return "";
            }
            var textArr = text.ToCharArray();
        
            textArr[0] = char.ToUpper(textArr[0]);

            if (!isMultiWord)
            {
                return string.Join("", textArr);
            }

            var words = text.Split(" ");

            var capitalizeWords = new List<string>();
        
            foreach (var word in words)
            {
                capitalizeWords.Add(word.CapitalizeText(false));
            }

            return string.Join(" ", capitalizeWords);
        }


        /// <summary>
        /// Uppercase a letter in the passed in text at a specific spot
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string ToLowerAt(this string text, int index)
        {
            if (index > text.Length - 1)
            {
                return text;
            }

            var letter = text[index];
            var newText =text.Replace(letter, Char.ToLower(letter));

            return newText;
        }

        /// <summary>
        /// Converts a string into a slug format
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToSlug(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "";
            }

            text = text.Replace(" ", "-");
            
            string slug = StringHelper.SanitizeSlug(text);
            
            return slug;
        }
}
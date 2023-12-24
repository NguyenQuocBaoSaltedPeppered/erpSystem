using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ERP.Databases
{
    public partial class Helpers
    {
        /// <summary>
        /// Chuyển từ tiếng việt có dấu thành tiếng việt không dấu.
        /// </summary>
        /// <param name="s">Chuỗi tiếng việt cần chuyển</param>
        /// <param name="isReplaceSpecialChracter">Có thay thế các ký tự đặc biệt hay không</param>
        /// <returns>Kết quả sau khi chuyển</returns>
        public static string ConvertToUnSign(string s, bool isReplaceSpecialChracter = false)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            Regex regex = MyRegex();
            string temp = s.Normalize(NormalizationForm.FormD);
            temp = regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            if (isReplaceSpecialChracter)
            {
                temp = temp.Replace(" ", "_");
                temp = temp.Replace(":", "");
                temp = temp.Replace("\\", "");
                temp = temp.Replace("/", "");
                temp = temp.Replace("\"", "");
                temp = temp.Replace("*", "");
                temp = temp.Replace("?", "");
                temp = temp.Replace(">", "");
                temp = temp.Replace("<", "");
                temp = temp.Replace("||", "");
            }
            return temp;
        }
        [GeneratedRegex("\\p{IsCombiningDiacriticalMarks}+")]
        private static partial Regex MyRegex();
    }
}
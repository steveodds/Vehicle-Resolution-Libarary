using System;
using System.IO;

namespace Vehicle_Resolution_Libarary
{
    public class Start
    {
        private string _message;
        public string ResolveSingleWord(string original, string refExcelFile)
        {
            if (string.IsNullOrWhiteSpace(original))
                return "ERROR: The argument was empty.";
            if (original.Length < 3)
                return "ERROR: No reliable matches can be made for arguments with less than 3 characters.";
            if (string.IsNullOrWhiteSpace(refExcelFile))
                return "ERROR: No reference file was given.";
            if (!File.Exists(refExcelFile))
                return "ERROR: Could not find file";

            if (original.Contains("xlsx") || original.Contains("xls"))
            {
                var excelResult = ProcessFullExcel(original);
                return excelResult.ToString();
            }
            else
            {
                var check = new NearestCorrectSpelling(null, refExcelFile);
                var result = check.GetWord(original, out int lev);
                if (lev == 1000 || lev < 0)
                    return "ERROR: No match was found.";
                if (original.Length == 3 && lev > 1)
                    return "ERROR: No match was found.";
                return result;
            }
            //return "error";
        }

        public int ProcessFullExcel(string excelFile)
        {
            throw new NotImplementedException("This feature is currently disabled.");
        }
    }
}

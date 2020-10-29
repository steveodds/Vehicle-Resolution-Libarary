using System;

namespace Vehicle_Resolution_Libarary
{
    public class Start
    {
        private string _message;
        public string ResolveSingleWord(string original, string refExcelFile)
        {
            if (string.IsNullOrWhiteSpace(original))
                throw new ArgumentException("The argument was empty.");
            if (original.Length < 3)
                throw new ArgumentException("No reliable matches can be made for arguments with less than 3 characters.");
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
                    throw new Exception("No match was found.");
                if (original.Length == 3 && lev > 1)
                    throw new Exception("No match was found.");
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

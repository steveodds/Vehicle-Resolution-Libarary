using System;
using System.IO;

namespace Vehicle_Resolution_Libarary
{
    public class Start
    {
        private string _message;
        public string ResolveSingleWord(string original, string refExcelFile)
        {
            var logger = Logger.GetInstance();
            if (string.IsNullOrWhiteSpace(original))
            {
                _message = "ERROR: The argument was empty.";
                logger.Log($"{_message} - No word was given.");
                logger.MarkAsEnd();
                return _message;
            }
            if (original.Length < 3)
            {
                _message = "ERROR: No reliable matches can be made for arguments with less than 3 characters.";
                logger.Log(_message);
                logger.MarkAsEnd();
                return _message;
            }
            if (string.IsNullOrWhiteSpace(refExcelFile))
            {
                _message = "ERROR: No reference file was given.";
                logger.Log($"{_message} - Given path: |{refExcelFile}|");
                logger.MarkAsEnd();
                return _message;
            }
            if (!File.Exists(refExcelFile))
            {
                _message = "ERROR: Could not find file";
                logger.Log($"{_message} - No such file exists at |{refExcelFile}|");
                logger.MarkAsEnd();
                return _message;
            }

            if (original.Contains("xlsx") || original.Contains("xls"))
            {
                _message = "ERROR: You provided a file as an argument in the wrong parameter.";
                logger.Log($"{_message} - Wrong parameter |{original}|.");
                logger.MarkAsEnd();
                return _message;
            }
            else
            {
                logger.Log("Starting process...");
                var check = new NearestCorrectSpelling(null, refExcelFile);
                var result = check.GetWord(original, out int lev);
                logger.Log("Checking result...");
                if (lev == 1000 || lev < 0)
                    return "ERROR: No match was found.";
                if (original.Length == 3 && lev > 1)
                    return "ERROR: No match was found.";
                logger.Log("Result found. Submitting...");
                logger.MarkAsEnd();
                return result;
            }
            //return "error";
        }

        ////DEPRECIATED
        //public int ProcessFullExcel(string excelFile)
        //{
        //    throw new NotImplementedException("This feature is currently disabled.");
        //}
    }
}

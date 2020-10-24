using System;

namespace Vehicle_Resolution_Libarary
{
    public class Start
    {
        private string _message;
        public string ResolveSingleWord(string original)
        {
            try
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
                    var check = new NearestCorrectSpelling(null, string.Empty);
                    var result = check.GetWord(original, out int lev);
                    if (lev == 1000 || lev < 0)
                        throw new Exception("No match was found.");
                    if (original.Length == 3 && lev > 1)
                        throw new Exception("No match was found.");
                    return result;
                }
            }
            catch (System.ArgumentNullException ex)
            {
                _message = "No file or argument was given: " + ex.Message;
            }
            catch (System.IO.IOException ex)
            {
                _message = "Cannot access file. Check if it's open in Excel: " + ex.Message;
            }
            catch (Exception ex)
            {
                _message = "There was an error with the provided argument: " + ex.Message;
            }
            //TODO: Log messages and implement a proper finally
            finally
            {
                //LOG
            }

            return "error";
        }

        public int ProcessFullExcel(string excelFile)
        {
            throw new NotImplementedException("This feature is currently disabled.");
        }
    }
}

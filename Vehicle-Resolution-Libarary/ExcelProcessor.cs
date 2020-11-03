using System;
using System.Collections.Generic;
using System.IO;

internal class ExcelProcessor
{
    //THIS CLASS HAS BEEN CONVERTED TO WORK WITH CSVs INSTEAD OF EXCELS, THE NAME REMAINS FOR "LEGACY REASONS"
    private readonly string _excelFile;
    private readonly Vehicle_Resolution_Libarary.Logger _logger;
    public List<MotorModel> RawMotorData {get; set;}
    public ExcelProcessor(string filepath)
    {
        _excelFile = filepath;
        _logger = Vehicle_Resolution_Libarary.Logger.GetInstance();
    }

    public List<MakeModel> GetRefData()
    {
        //var refFile = $@"{Directory.GetCurrentDirectory()}\Agilis LIVE Make & Models.xlsx";
        var refFile = _excelFile;
        if(refFile is null)
        {
            _logger.Log("FileNotFoundException ==> No file was provided - The parameter was null.");
            _logger.MarkAsEnd();
            throw new FileNotFoundException("No file was provided.");
        }
        if(!File.Exists(_excelFile))
        {
            _logger.Log("FileNotFoundException ==> No file was provided - File does not exist.");
            _logger.MarkAsEnd();
            throw new FileNotFoundException("No file was provided.");
        }

        try
        {
            var vehicles = new List<MakeModel>();

            var lines = File.ReadAllLines(refFile);
            foreach (var line in lines)
            {
                var segments = line.Split(',');
                vehicles.Add(new MakeModel { 
                    Make = segments[0],
                    Model = segments[1],
                    Code = int.TryParse(segments[2], out int result) ? result : 0,
                    Type = segments[3]
                });
            }


            //// NO LONGER READS EXCEL FILES
            //IWorkbook workbook;
            //using (FileStream file = new FileStream(refFile, FileMode.Open, FileAccess.Read))
            //{
            //    workbook = WorkbookFactory.Create(file);
            //}

            //var importer = new Mapper(workbook);
            //var items = importer.Take<MakeModel>(0);
            //foreach (var item in items)
            //{
            //    var row = item.Value;
            //    if (string.IsNullOrEmpty(row.Make))
            //        continue;

            //    vehicles.Add(row);
            //}
            return vehicles;
        }
        catch (Exception ex)
        {
            _logger.Log("Exception on processing reference file:");
            _logger.Log(ex.Message);
            _logger.Log(ex.ToString());
            _logger.Log(ex.InnerException is null ? "No inner exception." : ex.InnerException.ToString());
            _logger.MarkAsEnd();
        }
        return null;
    }

    //DEPRECIATED
    //public void GetGenesysData()
    //{
    //    IWorkbook workbook;
    //    var names = new List<MotorModel>();
    //    using (var file = new FileStream(_excelFile, FileMode.Open, FileAccess.Read))
    //    {
    //        workbook = WorkbookFactory.Create(file);
    //    }

    //    var sheet = workbook.GetSheetAt(0);
    //    var row = 1;
    //    while (sheet.GetRow(row) != null)
    //    {
    //        var makeCell = sheet.GetRow(row).GetCell(19);
    //        names.Add(new MotorModel{
    //            Make = makeCell.StringCellValue,
    //            ExcelLocation = makeCell.Address.ToString()
    //        });
    //        row++;
    //    }
    //    RawMotorData = names.OrderBy(x => x.Make).ToList();
    //}

    public void WriteFixedData(List<MotorModel> fixedModels)
    {
        //TODO write successful word completions back to the excel file
        throw new NotImplementedException("This feature is currently disabled.");
    }
}
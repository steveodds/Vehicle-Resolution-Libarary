using System;
using System.Collections.Generic;
using Fastenshtein;

internal class NearestCorrectSpelling
{
    private readonly string _excelFile;
    // public List<MotorModel> FinalList { get; private set; }
    private readonly List<MotorModel> _misspeltModels;
    private readonly List<MotorModel> _filteredModels;
    public NearestCorrectSpelling(List<MotorModel> misspeltModels, string excelFile)
    {
        _misspeltModels = misspeltModels;
        _excelFile = excelFile;
        _filteredModels = new List<MotorModel>();
    }

    private string ProcessRawModels(string model)
    {
        if(model.Contains(@"/"))
            model = model.Replace('/', ' ');

        //TODO Add more constraints
        
        return model;
    }

    private string GetClosestWord(string original, out bool exactmatch)
    {
        if(original is null)
            throw new ArgumentNullException("Cannot compare null values");
        //string word = String.Empty;
        exactmatch = false;
        var match = GetWord(original, out int levDistance);
        if(match is null || levDistance == 1000)
            throw new Exception("No match was found");
        if(match == original && levDistance == 0)
            exactmatch = true;
        return match;
    }

    public string GetWord(string original, out int lev)
    {
        string word = String.Empty;
        var levenshtein = new Levenshtein(original.ToLowerInvariant().Trim());
        var levDistance = 1000;
        var models = GetList();
        foreach (var model in models)
        {
            int distanceTemp = levenshtein.DistanceFrom(model.Model.ToLowerInvariant().Trim());
            if(distanceTemp < levDistance)
            {
                levDistance = distanceTemp;
                word = model.Model;
            }
            if(levDistance == 0)
                break;
        }
        lev = levDistance;
        return word;
    }

    private List<MakeModel> GetList()
    {
        var processor = new ExcelProcessor(_excelFile);
        var models = processor.GetRefData();
        return models;
    }

    public List<MotorModel> ProcessModels()
    {
        foreach (var model in _misspeltModels)
        {
            var fixedModel = ProcessRawModels(model.Make);
            var modelMatch = GetClosestWord(fixedModel, out bool exactmatch);
            if (exactmatch is false || !String.IsNullOrEmpty(modelMatch))
            {
                _filteredModels.Add(new MotorModel{
                    Make = modelMatch,
                    ExcelLocation = model.ExcelLocation
                });
            }
        }

        return _filteredModels;
    }

}
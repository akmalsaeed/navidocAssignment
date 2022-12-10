using navidocAssignment.DataModels;
using System.Numerics;
using System.Text.Json;

namespace navidocAssignment.Helpers
{
    public class TxtFileHelpers
    {
        public static string getContentForBarChart(string fileName)
        {
            string line;
            var filename = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            BarchartDataModel barcharDataModel = new BarchartDataModel();
            while ((line = file.ReadLine()) != null)
            {
                string[] strArray  = line.Split(':');
                barcharDataModel.labels.Add(strArray[0]);
                barcharDataModel.colors.Add(strArray[1]);
                barcharDataModel.data.Add(Int32.Parse(strArray[2]));
            }
            string jsonString = JsonSerializer.Serialize(barcharDataModel);
            return jsonString;
        }
    }
}

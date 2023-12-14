using LB3.Models;
using LumenWorks.Framework.IO.Csv;
using Microsoft.ML.Data;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace LB3.Services
{
    public class WineService
    {

        private readonly string _filePath;
        private List<Wine> _wines = new List<Wine>();

        public WineService(string filePath) {
            _filePath = filePath;

            // Parse CSV file using LumenWorks
            using (var csv = new CsvReader(new StreamReader(_filePath), true))
            {
                while (csv.ReadNextRecord())
                {
                    _wines.Add(new Wine
                    {
                        No = Int32.Parse(csv[0]),
                        Country = csv[1],
                        Description = csv[2],
                        Designation = csv[3],
                        Points = csv[4] == ""? null: Int32.Parse(csv[4]),
                        Price = csv[5] == "" ? null : Decimal.Parse(csv[5]),
                        Province = csv[6],
                        Region_1 = csv[7],
                        Region_2 = csv[8],
                        Variety = csv[9],
                        Winery = csv[10]
                    });
                }
            }

        }

        public Wine? get(string name)
        {
            return _wines.FirstOrDefault(x => x.Designation == name);
        }

        public Wine? get(string country, float price)
        {
            var sampleData = new MLModel.ModelInput()
            {
                Country = country,
                Price = price,
                Points = 95,
            };

            var result = MLModel.Predict(sampleData);
            return _wines.FirstOrDefault(x => x.Designation == result.PredictedLabel);
        }

        public List<string?> countries()
        {
            return _wines.Select(x => x.Country).Distinct().ToList();
        }
    }
}

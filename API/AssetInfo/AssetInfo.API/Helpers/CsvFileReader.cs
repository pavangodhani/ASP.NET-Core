using AssetInfo.API.Entities;
using System.Collections.Generic;
using System.IO;

namespace AssetInfo.API.Services
{
    public static class CsvFileReader
    {
        private static ICollection<Asset> _assets { get; set; }

        private static int _count { get; set; }

        private static string _csvFilePath = @"E:\KLing\ASP.NET Core\Project\AssetInfo\matrix.txt";

        public static ICollection<Asset> GetAssetsFromCsvFile()
        {
            using (var sr = new StreamReader(_csvFilePath))
            {
                string csvLine;
                _assets = new List<Asset>();
                while ((csvLine = sr.ReadLine()) != null)
                {
                    _assets.Add(ReadAssetFromCsvLine(csvLine));
                }
            }

            return _assets;
        }

        private static Asset ReadAssetFromCsvLine(string csvLine)
        {
            _count++;
            string[] parts = csvLine.Split(",");

            return new Asset() { 
                Id = _count, 
                MachineType = parts[0], 
                AssetName = parts[1], 
                AssetSeriesNo = parts[2] 
            };
        }

    }
}

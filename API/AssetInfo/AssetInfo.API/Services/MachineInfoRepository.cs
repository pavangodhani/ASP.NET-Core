using AssetInfo.API.DbContexts;
using AssetInfo.API.Entities;
using AssetInfo.API.Helpers;
using AssetInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Services
{
    public class MachineInfoRepository : IMachineInfoRepository
    {
        public AssetInfoContext _context;

        public MachineInfoRepository(AssetInfoContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public IEnumerable<string> GetMachineTypes()
        {
            return _context.Assets.Select(
                a => a.MachineType
                ).ToHashSet();
        }

        public IEnumerable<Asset> GetMachineTypesForAsset(string assetName)
        {
            return _context.Assets.Where
                (
                    a => a.AssetName.ToLower().Replace(" ", String.Empty) ==
                    assetName.ToLower().Replace(" ", String.Empty)
                ).ToList<Asset>();
        }


        public IEnumerable<string> GetMachineWhichUseLatestAssets()
        {
            var groupByMachineType = 
                _context.Assets.ToList()
                .GroupBy(a => a.MachineType)
                .ToDictionary(a => a.Key, a => a.ToList());

            var assetWithLatestSeries = GetAssetsWithLatestSeriesNo();

            var machineWhichUseLatestAssets = new List<string>();

            AssetComparer assetComparer = new AssetComparer();

            foreach (var machineType in groupByMachineType)
            {
                if(machineType.Value.All(a => assetWithLatestSeries.Contains<Asset>(a, assetComparer)))
                {
                    machineWhichUseLatestAssets.Add(machineType.Key);
                }
            }
            return machineWhichUseLatestAssets;
        }

        public IEnumerable<Asset> GetAssetsWithLatestSeriesNo()
        {
            AssetNameComparer assetNameComparer = new AssetNameComparer();

            return _context.Assets.OrderByDescending(
                a => a.AssetSeriesNo
                ).ToHashSet<Asset>(assetNameComparer);
        }
    }
}

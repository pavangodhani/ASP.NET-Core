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

        public IEnumerable<string> GetMachineTypesWhichUseLatestAssets()
        {
            var groupsByMachineTypes =
                _context.Assets.ToList()
                .GroupBy(a => a.MachineType);
                //.ToDictionary(g => g.Key, g => g.ToList());

            var assetsWithLatestSeries = GetAssetsWithLatestSeriesNumber();

            ICollection<string> machineTypesWhichUseLatestAssets = new List<string>();

            AssetComparer assetComparer = new AssetComparer();

            foreach (var machineType in groupsByMachineTypes)
            {
                if (machineType.All(
                    a => assetsWithLatestSeries.Contains<AssetNameAndSeriesNo>(
                        new AssetNameAndSeriesNo
                        { 
                            AssetName = a.AssetName, 
                            AssetSeriesNo = a.AssetSeriesNo
                        }, assetComparer)))
                {
                    machineTypesWhichUseLatestAssets.Add(machineType.Key);
                }
            }
            return machineTypesWhichUseLatestAssets;
        }

        IEnumerable<AssetNameAndSeriesNo> GetAssetsWithLatestSeriesNumber()
        {
            AssetNameComparer assetNameComparer = new AssetNameComparer();

            //return _context.Assets.OrderByDescending(
            //    a => a.AssetSeriesNo
            //    ).ToHashSet<Asset>(assetNameComparer);

            return _context.Assets.GroupBy(a => a.AssetName).Select(
                g => new AssetNameAndSeriesNo
                {
                    AssetName = g.Key,
                    AssetSeriesNo = g.Max(a => a.AssetSeriesNo)
                }).ToList();
        }
    }

    public class Number
    {
        int num;
        public Number(int n)
        {
            num = n;
        }
    }
}

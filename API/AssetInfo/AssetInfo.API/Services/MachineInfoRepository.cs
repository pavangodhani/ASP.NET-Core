using AssetInfo.API.DbContexts;
using AssetInfo.API.Entities;
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

        public IEnumerable<string> GetMachineTypesWhichUseLatestAssetSeriesForAllAssetThatItUses()
        {
            List<string> result = new List<string>();
            //var machineTypes = GetMachineTypes();
            //var assetsWithLatestSeriesNo = GetAssetsWithLatestSeriesNo();

            //foreach (var machineType in machineTypes)
            //{
            //    var assetsForSpecificMachineType = GetAssetsForMachineType(machineType);
            //    int count = 0;

            //    foreach (var assetForSpecificMachineType in assetsForSpecificMachineType)
            //    {
            //        foreach (var assetWithLatestSeriesNo in assetsWithLatestSeriesNo)
            //        {
            //            if (assetForSpecificMachineType.AssetName.ToLowerAndRemoveSpaces() ==
            //                assetWithLatestSeriesNo.AssetName.ToLowerAndRemoveSpaces() &&
            //                assetForSpecificMachineType.AssetSeriesNo.ToLowerAndRemoveSpacesAnds() ==
            //                assetWithLatestSeriesNo.AssetSeriesNo.ToLowerAndRemoveSpacesAnds())
            //            {
            //                count++;
            //            }
            //        }
            //    }

            //    if (count == assetsForSpecificMachineType.Count() && machineType != null)
            //    {
            //        result.Add(machineType);
            //    }
            //}

            return result;
        }

        public void GetMachineWhichUseLatestAssets()
        {
            var mDict = _context.Assets.GroupBy(x => x.MachineType).ToDictionary(x => x.Key);
        }
    }

}

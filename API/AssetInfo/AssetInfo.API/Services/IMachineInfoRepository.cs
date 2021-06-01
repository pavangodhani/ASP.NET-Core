using AssetInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Services
{
    public interface IMachineInfoRepository
    {
        public IEnumerable<string> GetMachineTypes();
        public IEnumerable<Asset> GetMachineTypesForAsset(string assetName);
        public IEnumerable<string> GetMachineTypesWhichUseLatestAssetSeriesForAllAssetThatItUses();
    }
}
